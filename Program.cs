using NewWebApplication2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NewWebApplication2.Middleware;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
        });         

        var app = builder.Build();

        // Podłączanie middleware w celu sprawdzenia klucza API
        app.UseMiddleware<ApiKeyMiddleware>();

        app.UseRouting();
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        });

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers(); 
        });

        app.Run();
    }
}