using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zovies.Backend.Context;
using Zovies.Backend.Models;

namespace Zovies.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MovieController : ControllerBase
{
    private readonly MovieContext _context;
    private readonly ILogger<MovieController> _logger;
    
    public MovieController(MovieContext context, ILogger<MovieController> logger)
    {
        _logger = logger;
        _context = context;
    }

    // GET: api/Movie
    [HttpGet]
    public async Task<IActionResult> GetMovies()
    {
        var movies = await _context.Movies
            .Include(m => m.MovieDetails)
            .Select(x => new { MovieId = x.MovieId, Name = x.MovieName, Cover = x.MovieDetails.MovieCoverPath})
            .ToListAsync();
        
        return Ok(movies);
    }

    [HttpGet("random")]
    public async Task<int> GetRandomMovie()
    {
        var movies = await _context.GetAll();
        var movieArray = movies.ToArray();
        return movieArray[Random.Shared.Next(0, movieArray.Length)].MovieId;
    }

    // GET: api/Movie/5
    [HttpGet("{id}")]
    public async Task<ActionResult<MovieDto>> GetMovie(int id)
    {
        var movie = _context.GetMovie(id);

        if (movie == null)
        {
            return NotFound();
        }
        
        return new MovieDto
        {
            MovieId = movie.MovieId,
            Title = movie.MovieName,
            Cast = movie.MovieCast,
            Details = new DetailDto(movie.MovieDetails, movie.MovieId)
        }; 
    }

    [HttpGet("filter")]
    public async Task<IActionResult> GetMoviesFiltered(string? search, float? rating, string? genre)
    {
        var movies = await _context.GetAll();
        var list = movies.ToList();
        // check if there are any movies stored
        if (!list.Any()) return Ok(new List<Movie>());

        var matched = from x in list
            where x.MovieName.ToLower().Contains(search?.ToLower() ?? "") &&
             x.MovieDetails.Rating >= (rating ?? 0) &&
             x.MovieDetails.MovieGenres.ToLower().Contains(genre?.ToLower() ?? "")
            select x;

        return Ok(matched.Select(x => new { MovieId = x.MovieId, Name = x.MovieName, Cover = x.MovieDetails.MovieCoverPath}));
    
    }
    

    // DELETE: api/Movie/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        
        if (movie == null)
        {
            return NotFound();
        }

        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool MovieExists(int id)
    {
        return _context.Movies.Any(e => e.MovieId == id);
    }
}
