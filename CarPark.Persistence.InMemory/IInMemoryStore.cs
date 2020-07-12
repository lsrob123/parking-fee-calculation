using CarPark.Models;
using System.Collections.Generic;

namespace CarPark.Persistence.InMemory
{
    public interface IInMemoryStore
    {
        ICollection<DefaultFlatRate> DefaultFlatRates { get; }
        ICollection<FlatRate> FlatRates { get; }
        ICollection<HourlyRate> HourlyRates { get; }
    }
}