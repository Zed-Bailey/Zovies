using NYoutubeDL;
using Zovies.Backend.Models;

namespace Zovies.Backend.Services;

public class DownloadService
{
    public static void Download(string m3U8File, string outputFile, bool consoleOutput = false)
    {
        var youtubeDl = new YoutubeDL();
        youtubeDl.Options.FilesystemOptions.Output = outputFile;
        youtubeDl.VideoUrl = m3U8File;

        if (consoleOutput)
        {
            youtubeDl.StandardOutputEvent += (sender, output) => Console.WriteLine(output);
            youtubeDl.StandardErrorEvent += (sender, errorOutput) => Console.WriteLine(errorOutput);    
        }
        
        youtubeDl.Download();
    }
}