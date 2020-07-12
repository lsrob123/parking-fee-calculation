using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CarPark.Persistence.SqlServer
{
    public class CarParkContextFactory : IDesignTimeDbContextFactory<CarParkContext>
    {
        public CarParkContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CarParkContext>();
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=CarPark;User Id=sa;Password=123;");
            // Please change the above connection string if design time operations are needed.

            return new CarParkContext(optionsBuilder.Options);
        }
    }
}