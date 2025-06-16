using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kolos2Popr.Models;

[Table("Character")]
public class Character
{
    [Key]
    public int CharacterId { get; set; }
    [MaxLength(50)]
    public string FirstName { get; set; } = null!;
    [MaxLength(100)]
    public string LastName { get; set; } = null!;
    public int CurrentWeight { get; set; }
    public int MaxWeight { get; set; }

    public ICollection<Backpack> Backpacks { get; set; } = null!;
    public ICollection<CharacterTitle> CharacterTitles { get; set; } = null!;
}