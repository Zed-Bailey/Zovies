using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Zovies.Backend.Models;
using Zovies.Backend.Services;

namespace Zovies.UnitTest;

public class OmdbUnitTest
{
    [OneTimeSetUp]
    public void Setup()
    {
        // set the omdb api key here
        ApplicationData.OmdbKey = "";
    }
    
    
    [Test]
    public async Task TestGetMovie()
    {
        var service = new OMDBService();
        var response = await service.FetchMovieDetails("indiana jones", 1981);
        
        Assert.NotNull(response);
        Assert.AreEqual(1981, int.Parse(response.Year));
        Assert.AreEqual("Indiana Jones and the Raiders of the Lost Ark", response.Title);
        Assert.AreEqual(8.4f, float.Parse(response.imdbRating));
    }
}