using CarPark.Models;
using Microsoft.EntityFrameworkCore;

namespace CarPark.Persistence.SqlServer
{
    public class CarParkContext : DbContext
    {
        private const string NewId = "NEWID()", DecimalPrecision = "decimal(11,4)";

        public CarParkContext(DbContextOptions<CarParkContext> options) : base(options)
        {
        }

        public DbSet<DefaultFlatRate> DefaultFlatRates { get; set; }
        public DbSet<FlatRate> FlatRates { get; set; }
        public DbSet<HourlyRate> HourlyRates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FlatRate>().Property(x => x.Key).HasDefaultValueSql(NewId);
            modelBuilder.Entity<FlatRate>().Property(x => x.EntryHourOffsetFrom).HasColumnType(DecimalPrecision);
            modelBuilder.Entity<FlatRate>().Property(x => x.EntryHourOffsetTo).HasColumnType(DecimalPrecision);
            modelBuilder.Entity<FlatRate>().Property(x => x.ExitHourOffsetFrom).HasColumnType(DecimalPrecision);
            modelBuilder.Entity<FlatRate>().Property(x => x.ExitHourOffsetTo).HasColumnType(DecimalPrecision);
            modelBuilder.Entity<FlatRate>().Property(x => x.TotalPrice).HasColumnType(DecimalPrecision);
            modelBuilder.Entity<FlatRate>().HasIndex(x => new { x.EntryHourOffsetTo });

            modelBuilder.Entity<HourlyRate>().Property(x => x.Key).HasDefaultValueSql(NewId);
            modelBuilder.Entity<HourlyRate>().Property(x => x.FromHour).HasColumnType(DecimalPrecision);
            modelBuilder.Entity<HourlyRate>().Property(x => x.ToHour).HasColumnType(DecimalPrecision);
            modelBuilder.Entity<HourlyRate>().Property(x => x.Value).HasColumnType(DecimalPrecision);
            modelBuilder.Entity<HourlyRate>().HasIndex(x => new { x.FromHour, x.ToHour });

            modelBuilder.Entity<DefaultFlatRate>().Property(x => x.Key).HasDefaultValueSql(NewId);
            modelBuilder.Entity<DefaultFlatRate>().Property(x => x.Value).HasColumnType(DecimalPrecision);

            // Data seeding. Remove if needed.
            foreach (var rate in Seeds.FlatRates)
            {
                modelBuilder.Entity<FlatRate>().HasData(rate);
            }

            foreach (var rate in Seeds.HourlyRates)
            {
                modelBuilder.Entity<HourlyRate>().HasData(rate);
            }

            foreach (var rate in Seeds.DefaultFlatRates)
            {
                modelBuilder.Entity<DefaultFlatRate>().HasData(rate);
            }
        }
    }
}