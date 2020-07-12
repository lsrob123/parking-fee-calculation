using CarPark.Abstractions;
using System;
using System.ComponentModel.DataAnnotations;

namespace CarPark.Models
{
    public class FlatRate : EntityBase
    {
        public FlatRate() : base()
        {
        }

        public FlatRate(decimal totalPrice, int dayOffsetToMonday, HourRange timeEntry, HourRange timeExit,
            int priority = 0, bool canExtendToFollowingDay = false) : this()
        {
            TotalPrice = totalPrice;
            Priority = priority;
            CanExtendToFollowingDay = canExtendToFollowingDay;
            DayOffsetToMonday = dayOffsetToMonday;
            EntryHourOffsetFrom = 24 * DayOffsetToMonday + timeEntry.Min;
            EntryHourOffsetTo = 24 * DayOffsetToMonday + timeEntry.Max;
            ExitHourOffsetFrom = 24 * DayOffsetToMonday + timeExit.Min;
            ExitHourOffsetTo = 24 * DayOffsetToMonday + timeExit.Max;
        }

        public bool CanExtendToFollowingDay { get; set; }
        public int DayOffsetToMonday { get; set; }
        public string Description => $"{(DayOfWeek)(1 + (DayOffsetToMonday == 6 ? -1 : DayOffsetToMonday))} > {EntryHourOffsetFrom}-{EntryHourOffsetTo} > {ExitHourOffsetFrom}-{ExitHourOffsetTo}";

        public decimal EntryHourOffsetFrom { get; set; }
        public decimal EntryHourOffsetTo { get; set; }
        public decimal ExitHourOffsetFrom { get; set; }
        public decimal ExitHourOffsetTo { get; set; }

        [Key]
        public override Guid Key { get; set; }

        public int Priority { get; set; }
        public decimal TotalPrice { get; set; }
    }
}