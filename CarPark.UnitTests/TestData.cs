using CarPark.Models;
using System;

namespace CarPark.UnitTests
{
    public class TestData
    {
        public TestData()
        {
            Duration = new HourRange();
        }

        public DayOfWeek DayOfWeek { get; set; }
        public HourRange Duration { get; set; }
        public decimal ExpectedPrice { get; set; }

        public TestData Expect(decimal expectedPrice)
        {
            ExpectedPrice = expectedPrice;
            return this;
        }

        public TestData On(DayOfWeek dayOfWeek)
        {
            DayOfWeek = dayOfWeek;
            return this;
        }

        public TestData FromHour(decimal fromHour)
        {
            Duration.Min = fromHour;
            return this;
        }

        public TestData ToHour(decimal toHour)
        {
            Duration.Max = toHour;
            return this;
        }
    }
}