using System.ComponentModel.DataAnnotations.Schema;

namespace Zovies.Backend.Models;

public class Details
{
    // primary key
    public int DetailsId { get; set; }
 
    public int Year { get; set; }
    public string MovieGenres { get; set; }
    public float Rating { get; set; }
    public string Description { get; set; }
    public string MovieFilePath { get; set; }
    public string MovieCoverPath { get; set; }
    
    
    // foreign key
    public int MovieId { get; set; }
    
    // relationship to movie
    public Movie Movie { get; set; }
}

/// <summary>
/// Details DTO class
/// </summary>
public class DetailDto {
    public int Year { get; set; }
    public string Genres { get; set; }
    public float Rating { get; set; }
    public string Description { get; set; }
    public string MovieFileUrl { get; } = "";
    public string CoverUrl { get; set; }

    public DetailDto(Details detailModel)
    {
        Year = detailModel.Year;
        Genres = detailModel.MovieGenres;
        Rating = detailModel.Rating;
        Description = detailModel.Description;
        CoverUrl = detailModel.MovieCoverPath;
        // check if the movie has been downloaded
        if(detailModel.MovieFilePath != "")
            // will create : http://192.1.1.1/folder/on/drive/movie_name-year.mp4
            MovieFileUrl = ApplicationData.NetworkDriveUrl + detailModel.MovieFilePath;
    }
}