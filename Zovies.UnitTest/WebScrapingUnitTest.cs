using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using Zovies.Backend.Models;
using Zovies.Backend.Services;


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
        MovieUrl = "https://lmplayer24.xyz/movies/play/10872600-spider-man-no-way-home-2021?mid=17&sid=cinft0aepbbhmtlq2ta0epd1fq&sec=38dedd5347449be5def13318126a0eb101513e0b&t=1652746278";
        CorrectName = "Spider-Man: No Way Home";
        CorrectYear = 2021;
    }

    // Basic test just to see if the webdriver is running correctly
    // if it's not then an error will occur here
    [Test]
    public void TestNavigateToGoogle()
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


    [Test]
    public void TestDownload()
    {
        // fetch m3u8 file and movie name and year
        automation.NavigateTo(MovieUrl);
        var (year,name,file) = automation.GetEverything();
        automation.Close();
        
        Assert.NotNull(file);
        
        ApplicationData.SaveFolderPath = "~/zovies/";
        var outputFile = $"{name}-{year}.mp4";
        
        // download file
        DownloadService.Download(file, outputFile, true);
        
        // assert that the file exists
        Assert.True(File.Exists(ApplicationData.SaveFolderPath + outputFile));
    }
    
    
    [TearDown]
    public void Teardown() {
        automation.Close();
    }
}