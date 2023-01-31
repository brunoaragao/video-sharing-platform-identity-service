using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AluraChallenge.VideoSharingPlatform.Services.Identity.API.Data;

namespace AluraChallenge.VideoSharingPlatform.Services.Identity.API;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddDbContext<ApplicationContext>(options =>
        {
            options.UseNpgsql(Configuration.GetConnectionString("IdentityConnection"));
        });

        services.AddIdentityCore<IdentityUser>(
            options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<ApplicationContext>();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new()
            {
                Title = "AluraChallenge.VideoSharingPlatform.Services.Identity.API",
                Version = "v1"
            });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(
                    "/swagger/v1/swagger.json",
                    "AluraChallenge.VideoSharingPlatform.Services.Identity.API v1");
            });
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
