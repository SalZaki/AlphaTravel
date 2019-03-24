namespace Alpha.Travel.WebApi.IntegrationTests
{
    using Persistence;
    using WebApi.Host;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.EntityFrameworkCore;

    public abstract class WebApplicationFactory : WebApplicationFactory<Startup>
    {
        public WebApplicationFactory() { }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                services.AddDbContext<AlphaTravelDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryAppDb");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var appDb = scopedServices.GetRequiredService<AlphaTravelDbContext>();
                    appDb.Database.EnsureCreated();
                    try
                    {
                        SeedData.PopulateTestData(appDb);
                    }
                    catch { }
                }
            });
        }
    }
}