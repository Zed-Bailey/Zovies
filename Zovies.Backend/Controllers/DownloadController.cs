using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    public IActionResult PostDownloadURL([FromForm] string downloadUrl)
    {
        return Ok(downloadUrl);
    }
}

