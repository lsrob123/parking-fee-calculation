using CarPark.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarPark.Abstractions
{
    public interface IParkingRateRepository
    {
        Task<decimal> GetDefaultFlatRate();
        Task<ICollection<FlatRate>> GetFlatRates(TimeSpan entryOffsetToMonday, TimeSpan exitOffsetToMonday);
        Task<HourlyRate> GetHourlyRate(decimal hours);
    }
}