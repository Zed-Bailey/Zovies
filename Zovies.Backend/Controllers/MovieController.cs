#nullable disable
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
    public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
    {
        return await _context.Movies.ToListAsync();
    }

    // GET: api/Movie/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>> GetMovie(int id)
    {
        var movie = await _context.Movies.FindAsync(id);

        if (movie == null)
        {
            return NotFound();
        }

        return movie;
    }

    [HttpGet("filter")]
    public ActionResult<Movie> GetMoviesFiltered([FromQuery] FilterParams filterParams)
    {
        var movies = _context.Movies;
        if (!movies.Any())
            return Ok(new List<Movie>());
        
        
        IQueryable<Movie> matchingMovies = null;

        if (filterParams.Genre != null)
            matchingMovies = matchingMovies.Concat(movies.Where(x => x.MovieDetails.MovieGenres.Exists(g => g.GenreName == filterParams.Genre)));

        if (filterParams.Rating != null)
            matchingMovies = matchingMovies.Concat(movies.Where(x => x.MovieDetails.Rating >= filterParams.Rating));
        
        // call distinct to remove any duplicates
        // could also use union when concating the filtered list together as i think that will also remove duplicates
        return Ok(matchingMovies.Distinct().ToList());
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
        return _context.Movies.Any(e => e.MovieID == id);
    }
}