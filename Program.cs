using NewWebApplication2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NewWebApplication2.Middleware;
using System.Threading;
using System.Threading.Channels;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Rejesracja background serwisu
        var logChannel = Channel.CreateUnbounded<string>();
        builder.Services.AddSingleton(logChannel);
        builder.Services.AddHostedService<BackgroundLoggerService>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
        });         
        
        var app = builder.Build();        

        app.UseRouting();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "EquipmentHostingService V1");
        });
        
        // Podłączanie middleware w celu sprawdzenia klucza API
        //app.UseMiddleware<ApiKeyMiddleware>();

        app.UseEndpoints(endpoints => {endpoints.MapControllers();});

        app.Run();
    }
}