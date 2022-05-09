using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Zovies.Downloader;

public class WebAutomater
{
    private IWebDriver _driver;
    private string _baseUrl;
    
    public WebAutomater(string baseUrl)
    {
        _driver = new ChromeDriver();
        _driver.Url = baseUrl;
        _baseUrl = baseUrl;
    }

    public void NavigateHome() => _driver.Url = _baseUrl;

    public void Close() => _driver.Close();

}