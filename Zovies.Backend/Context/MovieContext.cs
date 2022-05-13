using Microsoft.EntityFrameworkCore;
using Zovies.Backend.Models;

namespace Zovies.Backend.Context;

// Docs
// https://docs.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli


public class MovieContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }
    // public DbSet<Cast> Actors { get; set; }
    public DbSet<Details> MovieDetails { get; set; }

    public string DbPath { get; }

    /*
     * .Include() will load the Detail model from the databse
     * https://docs.microsoft.com/en-us/aspnet/web-api/overview/data/using-web-api-with-entity-framework/part-4
     * this can be removed by changing the models relationship to 'virtual' so it will be loaded automatically by EF
     * 
     */
    public Movie? GetMovie(int withId)
    {
        
        return Movies
            .Include(d => d.MovieDetails)
            .AsNoTracking()
            .FirstOrDefault(x => x.MovieId == withId);
    }

    public async Task<IEnumerable<Movie>> GetAll()
    {
        return await Movies
            .Include(m => m.MovieDetails)
            .AsNoTracking()
            .ToListAsync();
    }
    


    public MovieContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "movies.db");
    }
    
    
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
    
}