using Zovies.Backend.Models;
using System.Web;
namespace Zovies.Backend.Services;

public class OMDBService
{
    private readonly string _apiKey;
    private HttpClient _client;
    public OMDBService()
    {
        _apiKey = ApplicationData.OMDB_Key;
        _client = new HttpClient();
    }

    /// <summary>
    /// Query omdb api for movie information
    /// </summary>
    /// <param name="movieName"></param>
    /// <param name="releaseYear"></param>
    /// <returns></returns>
    public async Task<Movie?> FetchMovieDetails(string movieName, int releaseYear)
    {
        var baseUrl = $"http://www.omdbapi.com/?apikey={_apiKey}&t={movieName}&y={releaseYear}";
        var omdb = await _client.GetFromJsonAsync<OMDBModel>(HttpUtility.UrlEncode(baseUrl));
        if (omdb != null || omdb?.Response == "True")
        {
            return new Movie
            {
                MovieName = omdb.Title,
                MovieCast = omdb.Actors,
                MovieDetails = new Details
                {
                    Description = omdb.Plot,
                    Rating = float.Parse(omdb.ImdbRating),
                    Year = int.Parse(omdb.Year),
                    MovieGenres = omdb.Genre,
                    MovieCoverPath = omdb.Poster
                }
            };
        }

        return null;
    }
}