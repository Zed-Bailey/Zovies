using System.Net;
using System.Text.Json;
using Zovies.Backend.Models;
using System.Web;
namespace Zovies.Backend.Services;

public class OMDBService
{
    private readonly string _apiKey;
    private HttpClient _client;
    public OMDBService()
    {
        _apiKey = ApplicationData.OmdbKey;
        _client = new HttpClient();
    }

    /// <summary>
    /// Query omdb api for movie information
    /// </summary>
    /// <param name="movieName"></param>
    /// <param name="releaseYear"></param>
    /// <returns></returns>
    public async Task<OMDBModel?> FetchMovieDetails(string movieName, int releaseYear)
    {
        var baseUrl = $"http://www.omdbapi.com/?apikey={_apiKey}&t={movieName}&y={releaseYear}";
        // var omdb = await _client.GetFromJsonAsync<OMDBModel>(HttpUtility.UrlEncode(baseUrl));
        var response = await _client.GetAsync(baseUrl);
        if (response.StatusCode != HttpStatusCode.OK)
        {
            Console.WriteLine(response.ReasonPhrase);
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            return null;
        }
        
        var omdb = JsonSerializer.Deserialize<OMDBModel>(await response.Content.ReadAsStringAsync());
        
        // check that something  was returned from the api
        if (omdb != null || omdb?.Response == "True")
        {
            return omdb;
        }

        return null;
    }
}