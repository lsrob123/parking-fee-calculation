using System;
using System.Threading.Tasks;

namespace CarPark.Abstractions
{
    public interface IParkingFeeCalculator
    {
        Task<decimal> GetFeeAmount(DateTime timeEntry, DateTime timeExit);
    }
}