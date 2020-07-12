using CarPark.Abstractions;
using System;
using System.ComponentModel.DataAnnotations;

namespace CarPark.Models
{
    public class HourlyRate : EntityBase
    {
        public HourlyRate() : base()
        {
        }

        public HourlyRate(decimal value, decimal fromHours, decimal toHour) : this()
        {
            Value = value;
            FromHour = fromHours;
            ToHour = toHour;
        }

        public decimal FromHour { get; set; }
        public decimal ToHour { get; set; }

        [Key]
        public override Guid Key { get; set; }

        public decimal Value { get; set; }
    }
}