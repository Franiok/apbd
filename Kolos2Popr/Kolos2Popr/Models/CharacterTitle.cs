using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kolos2Popr.Models;

[Table("CharacterTitle")]
[PrimaryKey(nameof(CharacterId), nameof(TitleId))]
public class CharacterTitle
{
    [ForeignKey(nameof(Character))]
    public int CharacterId { get; set; }
    [ForeignKey(nameof(Title))]
    public int TitleId { get; set; }
    public DateTime AcquiredAt { get; set; }

    public Character Character { get; set; } = null!;
    public Title Title { get; set; } = null!;
}