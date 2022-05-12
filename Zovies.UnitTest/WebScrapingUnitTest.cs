using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using Zovies.Downloader;

namespace Zovies.UnitTest;

public class Tests
{
    private WebAutomater automation;
    
    [SetUp]
    public void Setup()
    {
        automation = new WebAutomater("");
    }

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
        var correctYear = 2022;
        var correctName = "The Lost City";
        automation.NavigateTo("https://lookmovie2.to/movies/view/13320622-le-secret-de-la-cite-perdue-2022");
        var (year, name) = automation.GetDetails();

        Assert.AreEqual(correctYear, year);
        Assert.AreEqual(correctName, name);
    }

    [TearDown]
    public void Teardown() {
        automation.Close();
    }
}