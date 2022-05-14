namespace Zovies.Backend.Controllers;

public class FilterParams
{
    public string? Genre { get; } = null;
    public float? Rating { get; } = null;
    public string? SearchTerm { get; } = null;
}