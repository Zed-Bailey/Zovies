using Microsoft.AspNetCore.Mvc;
using Zovies.Backend.Context;
using Microsoft.Net.Http.Headers;

namespace Zovies.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StreamController : ControllerBase
{
        private ILogger<StreamController> _logger;
        private MovieContext _context;
        
        public StreamController(MovieContext context, ILogger<StreamController> logger)
        {
                _context = context;
                _logger = logger;
        }
        
        
        [HttpGet]
        public FileStreamResult Stream([FromQuery] int id)
        {
                // https://stackoverflow.com/questions/42771409/how-to-stream-with-asp-net-core
                var movie = _context.GetMovie(id);
                if (movie == null)
                {
                        _logger.LogError("Tried to stream a movie with id {Id} but it does not exist", id);
                        return new FileStreamResult(System.IO.Stream.Null, String.Empty);
                }
                var filepath = movie.MovieDetails.MovieFilePath; 
                // var filepath = "/Users/zed/zovies/big-buck-bunny_trailer.webm";
                _logger.LogDebug("Streaming movie with id {Id} and filepath={Filepath}", movie.MovieId, filepath);
                var stream = System.IO.File.OpenRead(filepath);
                return new FileStreamResult(stream, new MediaTypeHeaderValue("video/mp4"));
                
        }
        
}