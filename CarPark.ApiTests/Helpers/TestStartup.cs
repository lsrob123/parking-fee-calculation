using CarPark.Abstractions;
using CarPark.Api;
using CarPark.Persistence.InMemory;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace CarPark.ApiTests.Helpers
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
            : base(configuration, webHostEnvironment)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            services.AddScoped<IInMemoryStore, InMemoryStore>();

            services.Remove(services.FirstOrDefault(x => x.ServiceType == typeof(IParkingRateRepository)));
            services.AddScoped<IParkingRateRepository, InMemoryRepository>();
            // Can change to a different implementation of reporsitory when supported by test infrastructure
        }
    }
}