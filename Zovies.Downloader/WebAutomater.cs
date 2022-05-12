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

    private IWebDriver _driver;


    public WebAutomater()
    {
        _driver = new ChromeDriver();
    }

    public void NavigateTo(string url)
    {
        _driver.Url = url;
    }
    
    /// <summary>
    /// Clicks on the video player play button, opening the video player
    /// </summary>
    public void OpenPlayer()
    {
        var exists = _driver.FindElement(By.ClassName("round-button"));
        if (exists != null)
        {
            exists.Click();
            // sleep after click on the player as there may be a thread defence page
            // between the movie detail and move player pages
            Thread.Sleep(5000);
        }
    }

    /// <summary>
    /// Executes a javascript script that will fetch the hd resolution mu38 file from the video players quality selector  
    /// </summary>
    /// <returns></returns>
    public string? GetHdMu38FileUrl()
    {
        // navigate to the player page so the videojs player is accessible
        OpenPlayer();
        /*
         * working javascript script that fetches the source from the quality inspector, also
         * source.src is the m3u8 file, now we dont need to watch the network logs!
         * created by playing around in the developer console
         * and discovering that the player uses this plugin: https://github.com/silvermine/videojs-quality-selector
         // creates a new videojs instance
         var player = videojs('video_player_html5_api');
         // get the items in the quality panel
         var items = player.controlBar.getChild("QualitySelector").items;
        // theres only 2 resolutions available for non-members 480p and HD
        // HD is the second array element 
        if(items.length == 2) {
            // this will return the m3u8 file url for the HD video 
              return items[1].source.src
        }
        
        //if(items[1].source.label == 'HD') {}
         */
        var execute = _driver as IJavaScriptExecutor;
        var response = execute?.ExecuteScript(@"
            var player = videojs('video_player_html5_api');
            var items = player.controlBar.getChild('QualitySelector').items;
            if(items.length == 2) {
                    return items[1].source.src
            }
        ");
        return response as string;
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


    /// <summary>
    /// Calls the GetDetails function to fetch the movie name and year
    /// then navigates to the player page and gets the m3u8 file
    /// </summary>
    /// <returns>(year, movie title, HD m3u8 file url) the m3u8 file will be null if no HD resolution was specified</returns>
    public (int, string, string?) GetEverything()
    {
        var (year, name) = GetDetails();
        var file = GetHdMu38FileUrl();
        return (year, name, file);
    }

    /// <summary>
    /// Close the browser
    /// </summary>
    public void Close()
    {
        // stop the network monitoring
        _driver.Manage().Network.StopMonitoring().Wait();
        // close the driver
        _driver.Close();
    }

}