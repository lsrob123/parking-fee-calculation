using CarPark.Abstractions;

namespace CarPark.Models
{
    public class HourRange : Range<decimal>
    {
        public HourRange()
        {
        }

        public HourRange(decimal min, decimal max) : base(min, max)
        {
        }
    }
}