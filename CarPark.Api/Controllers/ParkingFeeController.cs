using CarPark.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace CarPark.Api.Controllers
{
    [ApiController]
    [Route("parking-fee")]
    public class ParkingFeeController : ControllerBase
    {
        private readonly IParkingFeeCalculator _calculator;

        public ParkingFeeController(IParkingFeeCalculator calculator)
        {
            _calculator = calculator;
        }

        [HttpGet]
        public async Task<IActionResult> GetFeeAmount(
            [Required][FromQuery] DateTime from,
            [Required][FromQuery] DateTime to)
        {
            var fee = await _calculator.GetFeeAmount(from, to);
            return Ok(fee);
        }
    }
}