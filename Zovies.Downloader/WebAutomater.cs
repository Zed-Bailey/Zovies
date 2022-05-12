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
        
        _driver.Url = baseUrl;
        _baseUrl = baseUrl;
    }

    private static void LogNetworkRequest(object sender, NetworkRequestSentEventArgs args)
    {
        Console.WriteLine($"{args.RequestMethod} | {args.RequestUrl}");
    }
    
    public void NavigateHome() => _driver.Url = _baseUrl;

    public void Close()
    {
        // stop the network monitoring
        _driver.Manage().Network.StopMonitoring().Wait();
        // close the driver
        _driver.Close();
    }

}