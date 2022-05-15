using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Zovies.Backend.Context;
using Zovies.Backend.Models;

namespace Zovies.UnitTest;

public class UnitTest
{

    /// <summary>
    /// Run once before the test cases run to purge the db of all data
    /// </summary>
    [OneTimeSetUp]
    public void PurgeDb()
    {
        using var db = new MovieContext();
        
        
        if (!db.Movies.Any()) return;
        // remove movies
        db.Movies.RemoveRange(db.Movies.ToList());
        db.SaveChanges();
        
        
        // remove details
        if (!db.MovieDetails.Any()) return;
        db.MovieDetails.RemoveRange(db.MovieDetails.ToList());
        db.SaveChanges();
    }
    
    [Test]
    public void TestCreate()
    {
        using var db = new MovieContext();
        Console.WriteLine($"Database path: {db.DbPath}");
        
        // create new object
        var movie = new Movie {
                MovieCast = "Actor 1, Actor 2",
                MovieName = "The bestest movie"
        };
        // add to the set and save changes
        db.Movies.Add(movie);
        db.SaveChanges();

        // create new details model, referencing the movie id that was just created
        var details = new Details
        {
            Description = "Movie Description",
            Rating = 5,
            Year = 2022,
            MovieGenres = "action,adventure",
            MovieCoverPath = "file/path",
            MovieFilePath = "movie/path",
            MovieId = movie.MovieId,
        };
        // add to the movie details set and save.
        db.MovieDetails.Add(details);
        db.SaveChanges();
        
        // check that something was created
        Assert.AreEqual(1, db.Movies.Count());
        Assert.AreEqual(1, db.MovieDetails.Count());
    }

    [Test]
    public void TestRead()
    {
        using var db = new MovieContext();
        // get the first movie with matching ID
        var movie = db.GetMovie(db.Movies.First().MovieId);
        Assert.NotNull(movie);
        Assert.NotNull(movie?.MovieDetails);
        Assert.AreEqual("Movie Description", movie?.MovieDetails.Description);
        Assert.AreEqual(5, movie?.MovieDetails.Rating);
        Assert.AreEqual(2022, movie?.MovieDetails.Year);
        
    }
    
    // TODO add delete test
    

}