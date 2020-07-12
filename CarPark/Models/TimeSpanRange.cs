using CarPark.Abstractions;
using System;

namespace CarPark.Models
{
    public class TimeSpanRange : Range<TimeSpan>
    {
        public TimeSpanRange()
        {
        }

        public TimeSpanRange(TimeSpan min, TimeSpan max) : base(min, max)
        {
        }

        public TimeSpanRange(int dayOffset, HourRange hourRange)
            : this(dayOffset, hourRange.Min, hourRange.Max)
        {
        }

        public TimeSpanRange(int dayOffset, decimal from, decimal to)
        {
            Min = TimeSpan.FromHours((double)(24 * dayOffset + from));
            Max = TimeSpan.FromHours((double)(24 * dayOffset + to));
        }
    }
}