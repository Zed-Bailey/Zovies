namespace Zovies.Backend.Models;

public class Movie
{
    // primary key
    public int MovieId { get; set; }
    public string MovieName { get; set; }
    // Comma seperated string of movie cast members
    public string MovieCast { get; set; }
    
    // relationship to details 
    public Details MovieDetails { get; set; }
}