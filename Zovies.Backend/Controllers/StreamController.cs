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
        public FileResult Stream([FromQuery] int id)
        {
                // https://stackoverflow.com/questions/42771409/how-to-stream-with-asp-net-core
                var movie = _context.GetMovie(id);
                if (movie == null)
                {
                        _logger.LogError("Tried to stream a movie with id {Id} but it does not exist", id);
                        return new FileStreamResult(System.IO.Stream.Null, String.Empty);
                }
                var filepath = movie.MovieDetails.MovieFilePath; 
                
                _logger.LogDebug("Streaming movie with id {Id} and filepath={Filepath}", movie.MovieId, filepath);
                
                // Filestream result will force the client to download the entire video rather then streaming it in chunks
                // var stream = System.IO.File.OpenRead(filepath);
                // return new FileStreamResult(stream, new MediaTypeHeaderValue("video/mp4"));
                
                // Physical file will stream the file to the client in chunks on demand, and with range processing functionality so the
                // client video player can skip around the video
                return PhysicalFile(filepath, "video/mp4", enableRangeProcessing: true);

        }
        
}