using Microsoft.EntityFrameworkCore;
using Zovies.Backend.Models;

namespace Zovies.Backend.Context;

// Docs
// https://docs.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli


public class MovieContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Cast> Actors { get; set; }
    public DbSet<Details> MovieDetails { get; set; }

    public string DbPath { get; }

    public MovieContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "movies.db");
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
    
}