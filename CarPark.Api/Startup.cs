using CarPark.Abstractions;
using CarPark.Api.Middleware;
using CarPark.Persistence.SqlServer;
using CarPark.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarPark.Api
{
    public class Startup
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }

        public void Configure(IApplicationBuilder app)
        {
            var hostingEnvironment = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

            app.UseRouting();
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.UseSqlRepository(Configuration);
            // In-memory repository can also be used. Replace the above line with the following 2 lines
            // services.AddScoped<IInMemoryStore, InMemoryStore>();
            // services.AddScoped<IParkingRateRepository, InMemoryRepository>();

            services.AddScoped<IParkingFeeCalculator, ParkingFeeCalculator>();
            services.AddControllers();
        }
    }
}