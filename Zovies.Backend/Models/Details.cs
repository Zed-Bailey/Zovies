namespace Zovies.Backend.Models;

public class Details
{
    public int DetailsID { get; set; }
    public int Year { get; set; }
    public List<Genre> MovieGenres { get; set; }
    public float Rating { get; set; }
    public string Description { get; set; }
    public string MovieFilePath { get; set; }
    public string MovieCoverPath { get; set; }
}