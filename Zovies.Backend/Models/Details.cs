using System.ComponentModel.DataAnnotations.Schema;

namespace Zovies.Backend.Models;

public class Details
{
    // primary key
    public int DetailsId { get; set; }
    // foreign key
    public int MovieId { get; set; }
    public int Year { get; set; }
    public string MovieGenres { get; set; }
    public float Rating { get; set; }
    public string Description { get; set; }
    public string MovieFilePath { get; set; }
    public string MovieCoverPath { get; set; }
    
    // relationship to movie
    public Movie Movie { get; set; }
}