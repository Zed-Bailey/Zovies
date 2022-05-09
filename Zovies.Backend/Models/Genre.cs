namespace Zovies.Backend.Models;


    //TODO: add genres here, each genre should correspond to an int value
    // casting int to enum https://stackoverflow.com/questions/29482/how-can-i-cast-int-to-enum


public class Genre
{
    public int GenreID { get; set; }
    public string GenreName { get; set; }
}
