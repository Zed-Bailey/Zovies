using Zovies.Backend.Context;
using Zovies.Backend.Models;
using Zovies.Backend.Services;

// https://docs.microsoft.com/en-us/dotnet/iot/deployment
// https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-6.0


// read appsettings.json and get data we need
IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
// configure application data class
try
{
    // The folder to save downloaded movies to on this device
    ApplicationData.SaveFolderPath = configuration["SaveFolderPath"];
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

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy  =>
        {
            policy.WithOrigins(configuration["CorsUrl"]);
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();

app.MapControllers();

app.Run();