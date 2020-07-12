using CarPark.Abstractions;
using System;
using System.ComponentModel.DataAnnotations;

namespace CarPark.Models
{
    public class DefaultFlatRate : EntityBase
    {
        public DefaultFlatRate() : base()
        {
        }

        public DefaultFlatRate(decimal value) : this()
        {
            Value = value;
        }

        [Key]
        public override Guid Key { get; set; }

        public decimal Value { get; set; }
    }
}