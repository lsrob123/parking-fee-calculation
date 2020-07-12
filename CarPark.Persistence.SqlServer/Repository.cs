using CarPark.Abstractions;
using CarPark.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CarPark.Persistence.SqlServer
{
    public class Repository : IParkingRateRepository
    {
        private readonly CarParkContext _carParkContext;

        public Repository(CarParkContext carParkContext)
        {
            _carParkContext = carParkContext;
        }

        public async Task<decimal> GetDefaultFlatRate()
        {
            var rates = await _carParkContext.DefaultFlatRates.ToListAsync();
            if (rates.Count == 0)
            {
                throw new ProcessException("Internal data error.",
                   HttpStatusCode.InternalServerError,
                   new Error(nameof(Repository), "No DefaultFlatRates found."));
            }

            if (rates.Count > 1)
            {
                throw new ProcessException("Internal data error.",
                   HttpStatusCode.InternalServerError,
                   new Error(nameof(Repository), $"{rates.Count} DefaultFlatRates found."));
            }

            return rates.First().Value;
        }

        public async Task<ICollection<FlatRate>> GetFlatRates(TimeSpan entryOffsetToMonday, TimeSpan exitOffsetToMonday)
        {
            var entryOffsetToMondayHours = (decimal)entryOffsetToMonday.TotalHours;
            ICollection<FlatRate> rates = await _carParkContext.FlatRates
                .Where(x => entryOffsetToMondayHours < x.EntryHourOffsetTo)
                .OrderBy(x => x.Priority)
                .ToListAsync();
            return rates;
        }

        public async Task<HourlyRate> GetHourlyRate(decimal hours)
        {
            var rate = await _carParkContext.HourlyRates
               .FirstOrDefaultAsync(x => hours > x.FromHour && hours <= x.ToHour);
            return rate;
        }
    }
}