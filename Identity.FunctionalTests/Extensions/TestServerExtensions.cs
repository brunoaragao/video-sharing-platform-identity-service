namespace AluraChallenge.VideoSharingPlatform.Services.Identity.FunctionalTests.Extensions;

public static class TestServerExtensions
{
    public static TestServer SeedDatabaseToTest(this TestServer testServer)
    {
        using (var scope = testServer.Host.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var userManager = scopedServices.GetRequiredService<UserManager<IdentityUser>>();
            userManager.CreateAsync(
                new() { Email = "registered@email.com", UserName = "registered@email.com" },
                "P@ssw0rd"
            ).Wait();
        }

        return testServer;
    }
}
