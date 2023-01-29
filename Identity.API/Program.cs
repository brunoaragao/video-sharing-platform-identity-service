using AluraChallenge.VideoSharingPlatform.Services.Identity.API.Data;
using Microsoft.EntityFrameworkCore;

namespace AluraChallenge.VideoSharingPlatform.Services.Identity.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ApplicationContext>();
            await context.Database.MigrateAsync();
        }

        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
