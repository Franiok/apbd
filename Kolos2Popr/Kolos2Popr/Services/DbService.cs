using Kolos2Popr.Data;
using Kolos2Popr.DTOs;
using Kolos2Popr.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Kolos2Popr.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;

    public DbService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<CharacterDto?> GetCharacterById(int characterId)
    {
        var ch = await _context.Characters
            .Include(c => c.Backpacks).ThenInclude(b => b.Item)
            .Include(c => c.CharacterTitles).ThenInclude(ct => ct.Title)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CharacterId == characterId);

        if (ch is null)
            throw new NotFoundException("Character not found.");

        return new CharacterDto
        {
            FirstName = ch.FirstName,
            LastName = ch.LastName,
            CurrentWeight = ch.CurrentWeight,
            MaxWeight = ch.MaxWeight,

            BackpackItems = ch.Backpacks
                .Select(b => new BackpackItemDto
                {
                    ItemName = b.Item.Name,
                    ItemWeight = b.Item.Weight,
                    Amount = b.Amount
                })
                .ToList(),

            Titles = ch.CharacterTitles
                .OrderByDescending(t => t.AcquiredAt)
                .Select(t => new TitleDto
                {
                    Title      = t.Title.Name,
                    AcquiredAt = t.AcquiredAt
                })
                .ToList()
        };
    }

    public async Task AddItemsToBackpackAsync(int characterId, List<int> itemIds)
    {
        if (itemIds is null || itemIds.Count == 0)
            throw new ConflictException("List of item ids is empty.");

        if (itemIds.Distinct().Count() != itemIds.Count)
            throw new ConflictException("Duplicate item ids in request.");

        await using var t = await _context.Database.BeginTransactionAsync();

        var character = await _context.Characters
                            .Include(c => c.Backpacks)
                            .FirstOrDefaultAsync(c => c.CharacterId == characterId)
                        ?? throw new NotFoundException("Character not found.");

        var items = await _context.Items
            .Where(i => itemIds.Contains(i.ItemId))
            .ToListAsync();

        if (items.Count != itemIds.Count)
            throw new NotFoundException("Some items do not exist.");

        var addedWeight = items.Sum(i => i.Weight);
        if (character.CurrentWeight + addedWeight > character.MaxWeight)
            throw new ConflictException("Character is over-encumbered.");
        
        foreach (var item in items)
        {
            var row = character.Backpacks.FirstOrDefault(b => b.ItemId == item.ItemId);
            if (row is null)
            {
                _context.Backpacks.Add(new Models.Backpack
                {
                    CharacterId = characterId,
                    ItemId      = item.ItemId,
                    Amount      = 1
                });
            }
            else
            {
                row.Amount += 1;
            }
        }

        character.CurrentWeight += addedWeight;
        await _context.SaveChangesAsync();
        await t.CommitAsync();
    }
}