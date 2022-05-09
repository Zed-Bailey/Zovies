using NUnit.Framework;
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
        var a = new WebAutomater("https://lookmovie2.to");
        // teardown the browser
        a.Close();
    }
}