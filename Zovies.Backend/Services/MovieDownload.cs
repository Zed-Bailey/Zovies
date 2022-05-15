using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
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
        var (year, name, m3U8File) = _automation.GetEverything();
        // close browser after getting everything we need
        _automation.Close();

        if (m3U8File == null)
        {
            return (false, "No HD video could be found");
        }
        
        var service = new OMDBService();
        var movie = await service.FetchMovieDetails(name, year);
        if (movie == null) return
            (false, "failed to find the movie information");
        
        await using var context = new MovieContext();
        var createdMovie = context.Add(new Movie
        {
            MovieName = movie.Title,
            MovieCast = movie.Actors,
        });
        await context.SaveChangesAsync();
        
        Console.WriteLine("imdb rating: " + movie.imdbRating);
        var rating = float.Parse(movie.imdbRating);
        year = int.Parse(movie.Year);
        var moviesDetails = new Details
        {
            Description = movie.Plot,
            Rating = rating,
            Year = year,
            MovieGenres = movie.Genre,
            MovieCoverPath = movie.Poster,
            MovieFilePath = "",
            Movie = createdMovie.Entity,
        };
        
        createdMovie.Entity.MovieDetails = moviesDetails;
        await context.SaveChangesAsync();
        
        // runs in background
        Task.Run(() =>
        {
            var saveLocation = $"{ApplicationData.SaveFolderPath}{movie.Title}-{movie.Year}";
            var id = createdMovie.Entity.MovieId;
            // execute command to download movie
            // command: 
            //  youtube-dl --output "save/path/{movie name}-{year}.%(ext)s" {m3u8 file url}
            //.... long running process here
            var process = new Process();
            var startInfo = new ProcessStartInfo {
                FileName = "/bin/bash",
                Arguments = $"-c \"youtube-dl --output '{saveLocation}.%(ext)s' {m3U8File} \"",
                WindowStyle = ProcessWindowStyle.Hidden,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false
            };


            process.StartInfo = startInfo;
            process.Start();
            Console.WriteLine("Downloading: " + m3U8File);
            process.WaitForExit();
            // update movie variable with the file download location
            // update db context
            var updateContext = new MovieContext();
            var movieToUpdate = updateContext.Movies
                .Include(m => m.MovieDetails)
                .AsTracking()
                .First(x => x.MovieId == id);

            movieToUpdate.MovieDetails.MovieFilePath = saveLocation + ".mp4";
            
            updateContext.SaveChanges();

            return true;
        });
        
        return (true, createdMovie.Entity.MovieId.ToString());
    
        
    }
}