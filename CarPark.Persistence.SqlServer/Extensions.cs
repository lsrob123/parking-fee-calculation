using CarPark.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarPark.Persistence.SqlServer
{
    public static class Extensions
    {
        public static IServiceCollection UseSqlRepository(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<CarParkContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString(nameof(CarParkContext))));
            services.AddScoped<IParkingRateRepository, Repository>();
            return services;
        }
    }
}