using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using Zovies.Downloader;

namespace Zovies.UnitTest;

public class Tests
{
    private WebAutomater automation;

    private int CorrectYear;
    private string CorrectName;
    private string MovieUrl;
        
    
    
    [SetUp]
    public void Setup()
    {
        automation = new WebAutomater();
        MovieUrl = "https://lookmovie2.to/movies/view/13320622-le-secret-de-la-cite-perdue-2022";
        CorrectName = "The Lost City";
        CorrectYear = 2022;
    }

    // Basic test just to see if the webdriver is running correctly
    [Test]
    public void Test1()
    {
        automation.NavigateTo("https://google.com");
        Thread.Sleep(5000);
    }

  
    /// <summary>
    /// this will test fetching the movies name and release year
    /// make sure to update the correct year and correctName values, the url if needed as the
    /// sites url may have changed
    /// </summary>
    [Test]
    public void TestFetchingMovieNameAndYear()
    {
        
        automation.NavigateTo(MovieUrl);
        var (year, name) = automation.GetDetails();

        Assert.AreEqual(CorrectYear, year);
        Assert.AreEqual(CorrectName, name);
    }
    
    
    [Test]
    public void TestGettingM3U8File()
    {
        automation.NavigateTo(MovieUrl);
        var file = automation.GetHdMu38FileUrl();
        Console.WriteLine(file ?? "null :( no good resolution");
    }
    
    
    
    [TearDown]
    public void Teardown() {
        automation.Close();
    }
}