using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using Zovies.Downloader;

namespace Zovies.UnitTest;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var a = new WebAutomater("https://google.com");
        // var driver = new ChromeDriver();
        // driver.Url = "Https://google.com";
        // driver.Close();
        // teardown the browser
        // a.GetLogs();
        Thread.Sleep(5000);
        a.Close();
    }
}