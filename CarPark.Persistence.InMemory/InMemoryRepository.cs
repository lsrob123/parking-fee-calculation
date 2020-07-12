using CarPark.Abstractions;
using CarPark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CarPark.Persistence.InMemory
{
    public class InMemoryRepository : IParkingRateRepository
    {
        private readonly IInMemoryStore _store;

        public InMemoryRepository(IInMemoryStore store)
        {
            _store = store;
        }

        public Task<decimal> GetDefaultFlatRate()
        {
            var rates = _store.DefaultFlatRates;
            if (rates.Count == 0)
            {
                throw new ProcessException("Internal data error.",
                   HttpStatusCode.InternalServerError,
                   new Error(nameof(InMemoryRepository), "No DefaultFlatRates found."));
            }

            if (rates.Count > 1)
            {
                throw new ProcessException("Internal data error.",
                   HttpStatusCode.InternalServerError,
                   new Error(nameof(InMemoryRepository), $"{rates.Count} DefaultFlatRates found."));
            }

            return Task.FromResult(rates.First().Value);
        }

        public Task<ICollection<FlatRate>> GetFlatRates(TimeSpan entryOffsetToMonday,
            TimeSpan exitOffsetToMonday)
        {
            var entryOffsetToMondayHours = (decimal)entryOffsetToMonday.TotalHours;
            ICollection<FlatRate> rates = _store.FlatRates
                .Where(x => entryOffsetToMondayHours < x.EntryHourOffsetTo)
                .OrderBy(x => x.Priority)
                .ToList();
            return Task.FromResult(rates);
        }

        public Task<HourlyRate> GetHourlyRate(decimal hours)
        {
            var rate = _store.HourlyRates
                .FirstOrDefault(x => hours > x.FromHour && hours <= x.ToHour);
            return Task.FromResult(rate);
        }
    }
}