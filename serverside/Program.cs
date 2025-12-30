global using Microsoft.AspNetCore.Builder;
global using Microsoft.Extensions.DependencyInjection;
global using serverside.Interfaces;
global using serverside.Repositories;
global using Microsoft.OpenApi;
using ServerSide.Extensions;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder => builder
            .WithOrigins("http://localhost:3000") 
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddHttpClient<IMovieRepository, MovieRepository>();

// Add Swagger (Bonus Point: API Documentation)
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Movie API", 
        Version = "v1",
        Description = "API for fetching movie data from TheMovieDB"
    });
});

var app = builder.Build();
app.UseCors("AllowReactApp");
app.UseDeveloperExceptionPage();
                
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie API V1");
    c.RoutePrefix = string.Empty; // Swagger at root URL
});


// app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("AllowReactApp");

app.UseAuthorization();
app.UseExceptionHandler(new ExceptionHandlerOptions
{
    SuppressDiagnosticsCallback = (ctx) => 
    {
        // Example: Only suppress default logging for "expected" exceptions 
        // that you already logged in your GlobalExceptionHandler
        if (ctx.Exception is ArgumentException)
        {
            return true;
        }
        
        return false;
    }
});
app.MapControllers();

app.Run();