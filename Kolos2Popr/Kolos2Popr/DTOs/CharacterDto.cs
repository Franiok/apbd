namespace Kolos2Popr.DTOs;

public class CharacterDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public int CurrentWeight { get; set; }
    public int MaxWeight { get; set; }
    public List<BackpackItemDto> BackpackItems { get; set; } = null!;
    public List<TitleDto> Titles { get; set; } = null!;
}

public class BackpackItemDto
{
    public string ItemName { get; set; } = null!;
    public int ItemWeight { get; set; }
    public int Amount { get; set; }
}

public class TitleDto
{
    public string Title { get; set; } = null!;
    public DateTime AcquiredAt { get; set; }
}