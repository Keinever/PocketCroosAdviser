namespace PocketCroosAdviser.Models;

public record BlFilm
{
    public int Id { get; set; }
    public string? Photo { get; set; }
    public string? Description { get; set; }
    public string? Name { get; set; }
    public string? Country { get; set; }
    public string? Date { get; set; }
    public string? Raiting { get; set; }
}