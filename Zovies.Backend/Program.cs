using Zovies.Backend.Context;
using Zovies.Backend.Models;
using Zovies.Backend.Services;

// http://cagewebdev.com/raspberry-pi-connecting-to-a-network-drive/
// https://docs.microsoft.com/en-us/dotnet/iot/deployment

// read appsettings.json and get data we need
IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
// configure application data class
// this should be the url of the save folder on your network
// eg. http://192.1.x.x/save/movie/folder/
ApplicationData.NetworkDriveUrl = configuration["NetworkDriveUrl"];
// and this should be the same folder path as what's in the network drive url
// eg. /save/movie/folder

ApplicationData.SaveFolderPath = new Uri(ApplicationData.NetworkDriveUrl).AbsolutePath;
ApplicationData.OmdbKey = configuration["OMDBApiKey"];


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MovieContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();