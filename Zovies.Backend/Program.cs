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
try
{
    // the network url of the folder where you want to download and access the movies from on this device
    // eg. http://192.1.x.x/save/movie/folder/
    ApplicationData.NetworkDriveUrl = configuration["NetworkDriveUrl"];
    // extract the save folder path from the network url
    ApplicationData.SaveFolderPath = new Uri(ApplicationData.NetworkDriveUrl).AbsolutePath.TrimEnd('/');
    ApplicationData.OmdbKey = configuration["OMDBApiKey"];
}
catch (Exception e)
{
    Console.WriteLine("ERROR:\nSeems like you did not fill in the appsettings.json file.\nMake sure to fill in the api key and network drive fields!");
    return;
}




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