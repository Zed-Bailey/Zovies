namespace Zovies.Backend.Models;

public class Movie
{
    public int MovieID { get; set; }
    public string MovieName { get; set; }
    public List<Cast> MovieCast { get; set; }
    public Details MovieDetails { get; set; }
}