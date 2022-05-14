using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zovies.Backend.Services;

namespace Zovies.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DownloadController : ControllerBase
{
    /// <summary>
    /// Post the url to download to this endpoint via a form 
    /// </summary>
    /// <param name="downloadUrl"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> PostDownloadUrl([FromForm] string downloadUrl)
    {
        if (downloadUrl == "") return NotFound();
        var downloader = new MovieDownload(downloadUrl);
        var (success, message) = await downloader.GetMovie();
        // the id can be used by the web app to poll for the download status
        // when it's complete the app can show a snack bar message showing that it was successfully downloaded
        if(success)
            return Ok(new {ID = int.Parse(message)});

        return NotFound(new {Error = message});

    }
}

