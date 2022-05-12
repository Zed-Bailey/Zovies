using Zovies.Backend.Models;

namespace Zovies.Backend.Services;

public class OMDBService
{
    private readonly string _apiKey;
    public OMDBService(string apiKey)
    {
        _apiKey = apiKey;
    }

    // public async Task<Movie> FetchMovieDetails(string movieName, int releaseYear)
    // {
    // }
}