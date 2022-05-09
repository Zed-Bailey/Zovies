namespace Zovies.Backend.Models;

public class Details
{
    public int Year { get; set; }
    public Genre MovieGenre { get; set; }
    public float Rating { get; set; }
    public string Description { get; set; }
    public string MovieFilePath { get; set; }
    public string MovieCoverPath { get; set; }
}