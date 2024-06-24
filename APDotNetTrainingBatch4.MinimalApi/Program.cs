using APDotNetTrainingBatch4.MinimalApi.Db;
using APDotNetTrainingBatch4.MinimalApi.Features.Blog;
using APDotNetTrainingBatch4.MinimalApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
    //opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection2"));
},ServiceLifetime.Transient); //default => ServiceLifeTiem.Scoped

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapGet("/", () => "Hello World");
/* Code move to BlogService => using ExtensionMethod */
//BlogService.MapBlogs(app);
//How to invoke like  app.xxx() ... => use 'this' keywords in para 

app.MapBlogs();

//app.MapBlogs().mapget...; //return type=> IEndpointRouteBuilder =? consequence dot method calling (.().())

app.Run();






//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast = Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]
//        ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast")
//.WithOpenApi();

//internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
//{
//    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}
