using CarPark.Models;
using System.Collections.Generic;

namespace CarPark.Persistence.InMemory
{
    public class InMemoryStore : IInMemoryStore
    {
        public ICollection<FlatRate> FlatRates { get; } = new List<FlatRate>
        {
            // Weekends
            new FlatRate(10M, 5, new HourRange(0,24), new HourRange(0,24), canExtendToFollowingDay: true),
            new FlatRate(10M, 6, new HourRange(0,24), new HourRange(0,24)),

            // Nights
            new FlatRate(6.5M, 0, new HourRange(0,6), new HourRange(0,6)),
            new FlatRate(6.5M, 0, new HourRange(18,24), new HourRange(18,30)),
            new FlatRate(6.5M, 1, new HourRange(0,6), new HourRange(0,6)),
            new FlatRate(6.5M, 1, new HourRange(18,24), new HourRange(18,30)),
            new FlatRate(6.5M, 2, new HourRange(0,6), new HourRange(0,6)),
            new FlatRate(6.5M, 2, new HourRange(18,24), new HourRange(18,30)),
            new FlatRate(6.5M, 3, new HourRange(0,6), new HourRange(0,6)),
            new FlatRate(6.5M, 3, new HourRange(18,24), new HourRange(18,30)),
            new FlatRate(6.5M, 4, new HourRange(0,6), new HourRange(0,6)),
            new FlatRate(6.5M, 4, new HourRange(18,24), new HourRange(18,30), canExtendToFollowingDay: true),

            // Early Birds
            new FlatRate(13M, 0, new HourRange(6,9), new HourRange(15.5M, 23.5M)),
            new FlatRate(13M, 1, new HourRange(6,9), new HourRange(15.5M, 23.5M)),
            new FlatRate(13M, 2, new HourRange(6,9), new HourRange(15.5M, 23.5M)),
            new FlatRate(13M, 3, new HourRange(6,9), new HourRange(15.5M, 23.5M)),
            new FlatRate(13M, 4, new HourRange(6,9), new HourRange(15.5M, 23.5M)),
        };

        public ICollection<HourlyRate> HourlyRates { get; } = new List<HourlyRate>
        {
            new HourlyRate(5M, 0, 1),
            new HourlyRate(10M, 1, 2),
            new HourlyRate(15M, 2, 3),
        };

        public ICollection<DefaultFlatRate> DefaultFlatRates { get; } = new List<DefaultFlatRate>
        {
            new DefaultFlatRate(20M)
        };
    }
}