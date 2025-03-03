using backend.DataBase;
using backend.Middleware;
using backend.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // CORS: Allow all origins
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins", policy => policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        });

        // Database connection
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection")));

        // Handling null values globally in JSON responses
        builder.Services.AddControllers().AddJsonOptions(option =>
        {
            option.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });

        // Register application services
        builder.Services.AddApplicationService();

        var app = builder.Build();

        // Apply error handling middleware
        app.UseMiddleware<ErrorHandlerMiddleware>();

        // 🔹 Enable CORS BEFORE anything else
        app.UseCors("AllowAllOrigins");

        // 🔹 Comment out HTTPS redirection if testing on localhost (optional)
        // app.UseHttpsRedirection();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
