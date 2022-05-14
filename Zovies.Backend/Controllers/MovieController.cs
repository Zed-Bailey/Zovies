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
    public async Task<IActionResult> GetMovies()
    {
        var movies = await _context.Movies
            .Include(m => m.MovieDetails)
            .Select(x => new { MovieId = x.MovieId, Name = x.MovieName, Cover = x.MovieDetails.MovieCoverPath})
            .ToListAsync();
        
        return Ok(movies);
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
            MovieName = movie.MovieName,
            MovieCast = movie.MovieCast,
            Details = new DetailDto(movie.MovieDetails)
        };
    }

    [HttpGet("filter")]
    public async Task<ActionResult<MovieDto>> GetMoviesFiltered([FromQuery] FilterParams filterParams)
    {
        var movies = await _context.GetAll();
        var list = movies.ToList();
        // check if there are any movies stored
        if (!list.Any()) return Ok(new List<Movie>());

        var matched = list
            // filter list
            .Where(x => x.MovieDetails.Rating >= (filterParams.Rating ?? 0))
            .Where(x => filterParams.Genre != null && x.MovieDetails.MovieGenres.Contains(filterParams.Genre))
            // convert matching models to the DTO object
            .Select(x => new MovieDto {
                MovieId = x.MovieId,
                MovieName = x.MovieName,
                MovieCast = x.MovieCast,
                Details = new DetailDto(x.MovieDetails)
            });
        
        return Ok(matched);
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