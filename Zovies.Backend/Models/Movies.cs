namespace Zovies.Backend.Models;

public class Movie
{
    public int MovieID { get; set; }
    public string MovieName { get; set; }
    // Comma seperated string of movie cast members
    public string MovieCast { get; set; }
    public Details MovieDetails { get; set; }
}