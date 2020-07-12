using CarPark.Api.Models;
using CarPark.ApiTests.Helpers;
using FluentAssertions;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace CarPark.ApiTests
{
    public class FailedCallTests
    {
        [Fact]
        public async Task Should_Return_BadRequest_Given_ExitBeingEalierThanEntry()
        {
            var factory = new ApplicationFactory<TestStartup>().AsWebApplicationFactory();
            var httpClient = factory.CreateClient();

            var exit = DateTime.Now;
            var entry = exit.AddHours(3);
            var response = await httpClient.GetAsync($"/parking-fee/?from={entry:yyyy-MM-dd HH:mm}&&to={exit:yyyy-MM-dd HH:mm}");

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var json = await response.Content.ReadAsStringAsync();
            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            errorResponse.Title.Should().Be("Exit time is greater than entry time.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("?from=1&to=2")]
        [InlineData("?to=2")]
        [InlineData("?from=1")]
        public async Task Should_Return_BadRequest_Given_Invalid_QueryString(string queryString)
        {
            var factory = new ApplicationFactory<TestStartup>().AsWebApplicationFactory();
            var httpClient = factory.CreateClient();

            var response = await httpClient.GetAsync($"/parking-fee/{queryString}");

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var json = await response.Content.ReadAsStringAsync();
            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            errorResponse.Title.Should().Be("One or more validation errors occurred.");
        }

        [Fact]
        public async Task Should_Return_InternalServerError_Given_MoreThanOneDefaultFlatRateFound()
        {
            var factory = new ApplicationFactory<TestStartupMoreThanOneDefaultFlatRate>().AsWebApplicationFactory();
            var httpClient = factory.CreateClient();

            var response = await httpClient.GetAsync($"/parking-fee/?from=2020-07-07 10:00&to=2020-07-07 14:00");

            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
            var json = await response.Content.ReadAsStringAsync();
            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            errorResponse.Title.Should().Be("Internal data error.");
        }

        [Fact]
        public async Task Should_Return_InternalServerError_Given_No_DefaultFlatRateFound()
        {
            var factory = new ApplicationFactory<TestStartupNoDefaultFlatRate>().AsWebApplicationFactory();
            var httpClient = factory.CreateClient();

            var response = await httpClient.GetAsync($"/parking-fee/?from=2020-07-07 10:00&to=2020-07-07 14:00");

            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
            var json = await response.Content.ReadAsStringAsync();
            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            errorResponse.Title.Should().Be("Internal data error.");
        }
    }
}