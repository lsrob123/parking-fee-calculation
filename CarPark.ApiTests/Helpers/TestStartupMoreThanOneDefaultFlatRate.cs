using CarPark.Abstractions;
using CarPark.Api;
using CarPark.Models;
using CarPark.Persistence.InMemory;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace CarPark.ApiTests.Helpers
{
    public class TestStartupMoreThanOneDefaultFlatRate : Startup
    {
        public TestStartupMoreThanOneDefaultFlatRate(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
            : base(configuration, webHostEnvironment)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            services.Remove(services.FirstOrDefault(x => x.ServiceType == typeof(IParkingRateRepository)));

            var store = new InMemoryStore();
            store.DefaultFlatRates.Add(new DefaultFlatRate(23));
            services.AddScoped<IInMemoryStore>(_ => store);
            services.AddScoped<IParkingRateRepository, InMemoryRepository>();
        }
    }
}