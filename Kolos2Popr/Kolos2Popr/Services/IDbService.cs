using Kolos2Popr.DTOs;

namespace Kolos2Popr.Services;

public interface IDbService
{
    Task<CharacterDto?> GetCharacterById(int id);
    Task AddItemsToBackpackAsync(int id, List<int> itemIds);
}