namespace PocketCroosAdviser.Models;

public record Ganres
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool? Picked { get; set; }
}