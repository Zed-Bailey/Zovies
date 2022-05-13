using Zovies.Backend.Context;
using Zovies.Backend.Models;
using Zovies.Backend.Services;

namespace Zovies.Backend.Services;

public class MovieDownload
{
    private string _url;
    private WebAutomater _automation;
    
    public MovieDownload(string url)
    {
        _url = url;
        _automation = new WebAutomater();
    }

    /// <summary>
    /// Gets all the movie information
    /// downloads the movie and updates the database
    /// </summary>
    /// <returns>a tuple of success, message. When false is returned then the message will be why, when true is returned then the message will be the movies ID</returns>
    public async Task<(bool, string)> GetMovie()
    {
        _automation.NavigateTo(_url);
        Thread.Sleep(1000);
        var (year, name) = _automation.GetDetails();
        var service = new OMDBService();
        var movie = await service.FetchMovieDetails(name, year);
        if (movie == null) return (false, "failed to find the movie information");
        var context = new MovieContext();
        context.Add(movie);
        await context.SaveChangesAsync();
        // Task.Run(() =>
        // {
        //     var saveLocation = "";
        //     // execute command to download movie
        //     // command: 
        //     //  youtube-dl --output "save/path/{movie name}-{year}.%(ext)s" {m3u8 file url}
        //     
        //     // update movie variable with the file download location
        //     // update db context
        //     movie.MovieDetails.MovieFilePath = saveLocation;
        //     context.SaveChanges();
        // });
        return (true, movie.MovieID.ToString());
    }
}