namespace AluraChallenge.VideoSharingPlatform.Services.Identity.FunctionalTests.Fixtures;

public class TestServerFixture
{
    public TestServerFixture()
    {
        Client = CreateTestServer()
            .SeedDatabaseToTest()
            .CreateClient();
    }

    private static TestServer CreateTestServer()
    {
        var webHostBuilder = new WebHostBuilder()
            .ConfigureAppConfiguration(configuration =>
            {
                configuration.AddJsonFile("appsettings.json");
            })
            .ConfigureServices(services =>
            {
                services.AddDbContext<ApplicationContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });
            })
            .UseStartup<Startup>();

        return new TestServer(webHostBuilder);
    }

    public HttpClient Client { get; }
}
