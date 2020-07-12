using CarPark.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace CarPark.ApiTests.Helpers
{
    public static class Extensions
    {
        public static WebApplicationFactory<TStartup> AsWebApplicationFactory<TStartup>
            (this ApplicationFactory<TStartup> factory) where TStartup : Startup
        {
            var webApplicationFactory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddMvc().AddApplicationPart(typeof(Startup).Assembly);
                });
            });

            return webApplicationFactory;
        }
    }
}