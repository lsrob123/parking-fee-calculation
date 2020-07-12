using CarPark.Abstractions;
using CarPark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CarPark.Services
{
    public class ParkingFeeCalculator : IParkingFeeCalculator
    {
        private readonly IParkingRateRepository _repository;

        public ParkingFeeCalculator(IParkingRateRepository repository)
        {
            _repository = repository;
        }

        public async Task<decimal> GetFeeAmount(DateTime timeEntry, DateTime timeExit)
        {
            if (timeExit < timeEntry)
            {
                throw new ProcessException("Exit time is greater than entry time.",
                    HttpStatusCode.BadRequest,
                    new Error(
                        nameof(ParkingFeeCalculator), 
                        $"Invalid arguments as timeExit {timeExit} < timeEntry {timeEntry}"));
            }

            if (timeExit == timeEntry)
            {
                return 0;
            }

            var monday = timeEntry.MondayOfTheWeek();
            var nextMonday = monday.AddDays(7);
            var exitOnSameDay = timeExit.Date == timeEntry.Date;

            var total = 0M;
            if (nextMonday >= timeExit)
            {
                total += await GetSubtotalOnFlatRates(
                    timeEntry.OffsetToWeeklyStart(),
                    timeExit.OffsetToWeeklyStart(),
                    exitOnSameDay);
            }
            else
            {
                var start = timeEntry;
                var end = nextMonday;

                while (end < timeExit)
                {
                    var subtotal = await GetSubtotalOnFlatRates(start.OffsetToWeeklyStart(),
                        start.OffsetToWeeklyStart().Add(end.Subtract(start)),
                        exitOnSameDay);
                    if (subtotal == 0)
                    {
                        subtotal = await GetSubtotalOnHourlyOrDefaultFlatRate(start, end);
                    }
                    total += subtotal;
                    start = end;
                    end = end.AddDays(7);
                    if (end > timeExit)
                    {
                        end = timeExit;
                        subtotal = await GetSubtotalOnFlatRates(start.OffsetToWeeklyStart(), end.OffsetToWeeklyStart(), exitOnSameDay);
                        if (subtotal == 0)
                        {
                            subtotal = await GetSubtotalOnHourlyOrDefaultFlatRate(start, end);
                        }
                        total += subtotal;
                    }
                }
            }

            if (total == 0M)
            {
                total = await GetSubtotalOnHourlyOrDefaultFlatRate(timeEntry, timeExit);
            }

            return total;
        }

        private ICollection<FlatRate> GetFlatRates(ICollection<FlatRate> rates, TimeSpan entryOffsetToMonday,
            TimeSpan exitOffsetToMonday, bool exitOnSameDay)
        {
            var entryOffsetToMondayHours = (decimal)entryOffsetToMonday.TotalHours;
            var exitOffsetToMondayHours = (decimal)exitOffsetToMonday.TotalHours;

            var rate = rates.FirstOrDefault(
               x => x.EntryHourOffsetFrom <= entryOffsetToMondayHours &&
                   x.ExitHourOffsetFrom <= exitOffsetToMondayHours &&
                   exitOffsetToMondayHours <= x.ExitHourOffsetTo);

            if (rate != null)
            {
                return new List<FlatRate> { rate };
            }

            if (exitOnSameDay)
            {
                return new List<FlatRate>();
            }

            var includedRates = rates
                .Where(x => x.ExitHourOffsetFrom <= exitOffsetToMondayHours)
                .ToList();

            if (!includedRates.Any(x => x.CanExtendToFollowingDay))
            {
                return new List<FlatRate>();
            }

            return includedRates;
        }

        private async Task<decimal> GetSubtotalOnFlatRates(TimeSpan entryOffsetToMonday, TimeSpan exitOffsetToMonday,
            bool exitOnSameDay)
        {
            var endOfWeek = TimeSpan.Zero.Add(TimeSpan.FromDays(7));
            if (exitOffsetToMonday < entryOffsetToMonday)
            {
                exitOffsetToMonday = endOfWeek.Add(exitOffsetToMonday);
            }

            var rates = GetFlatRates(
                await _repository.GetFlatRates(entryOffsetToMonday, exitOffsetToMonday),
                entryOffsetToMonday, exitOffsetToMonday, exitOnSameDay);

            var subTotal = rates
                .Aggregate(0M, (total, current) => total += current.TotalPrice);
            return subTotal;
        }

        private async Task<decimal> GetSubtotalOnHourlyOrDefaultFlatRate(
            DateTime timeEntry,
            DateTime timeExit)
        {
            var totalDays = 0;
            for (var date = timeEntry.Date; date <= timeExit.Date; date = date.AddDays(1))
            {
                totalDays++;
            }

            var defaultFlatRate = await _repository.GetDefaultFlatRate();
            var priceOnDefaultFlatRate = totalDays * defaultFlatRate;
            if (totalDays > 1)
            {
                return totalDays * defaultFlatRate;
            }

            var totalHours = (decimal)timeExit.Subtract(timeEntry).TotalHours;
            var rate = await _repository.GetHourlyRate(totalHours);
            var subtotal = rate?.Value ?? priceOnDefaultFlatRate;
            return subtotal;
        }
    }
}