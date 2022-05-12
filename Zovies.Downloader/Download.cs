using Zovies.Backend.Models;
using Zovies.Backend.Services;

namespace Zovies.Downloader;

public class Download
{
    private string _url;
    private WebAutomater _automation;
    
    public Download(string url)
    {
        _url = url;
        _automation = new WebAutomater();

    }

    // public async Movie GetMovie()
    // {
    //     _automation.NavigateTo(_url);
    //     Thread.Sleep(1000);
    //     var (year, name) = _automation.GetDetails();
    //     var service = new OMDBService();
    //     await service.FetchMovieDetails(name, year);
    //
    //     // command: 
    //     //
    //     
    // }
}