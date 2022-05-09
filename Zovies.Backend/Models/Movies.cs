namespace Zovies.Backend.Models;

public class Movies
{
    public Guid ID { get; set; }
    public string MovieName { get; set; }
    public List<Cast> MovieCast { get; set; }
    public Details MovieDetails { get; set; }
}