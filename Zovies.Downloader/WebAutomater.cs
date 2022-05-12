using System.Reflection.Metadata.Ecma335;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Chromium;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.DevTools.V85.Network;
using OpenQA.Selenium.Firefox;

namespace Zovies.Downloader;

public class WebAutomater
{
    /*
     * Search for video element on page
     * trigger play (.play()?)
     * watch network tab for m2u8 file to be downloaded
     * 
     */
    private IWebDriver _driver;
    private string _baseUrl;
    
    public WebAutomater(string baseUrl)
    {
        _driver = new ChromeDriver();
        // start monitoring network requests
        _driver.Manage().Network.StartMonitoring().Wait();
        // add event handler, triggered for every request sent
        _driver.Manage().Network.NetworkRequestSent += LogNetworkRequest;
        
        // _driver.Url = baseUrl;
        _baseUrl = baseUrl;
    }

    public void NavigateTo(string url)
    {
        _driver.Url = url;
    }
    public void OpenPlayer()
    {
        var exists = _driver.FindElement(By.ClassName("round-button"));
        exists?.Click();
    }

    public void PlayMovie()
    {
        var player = _driver.FindElement(By.Id("video_player_html5_api"));
        player?.Click();
    }

    /// <summary>
    /// Get the movies release year and movies name from the page
    /// </summary>
    /// <returns>a tuple, the first value being the year and the second the name of the movie</returns>
    public (int, string) GetDetails()
    {
        var name = _driver.FindElement(By.ClassName("bd-hd")).Text;
        var year = _driver.FindElement(By.CssSelector(".bd-hd > span:nth-child(1)")).Text;
        name = name.Replace(year, "").Trim();
        return (int.Parse(year), name);
    }
    
    private static void LogNetworkRequest(object? sender, NetworkRequestSentEventArgs args)
    {
        if(args.RequestUrl.EndsWith(".m3u8"))
            Console.WriteLine($"{args.RequestMethod} | {args.RequestUrl}");
    }
    

    public void Close()
    {
        // stop the network monitoring
        _driver.Manage().Network.StopMonitoring().Wait();
        // close the driver
        _driver.Close();
    }

}