using CarPark.ApiTests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace CarPark.ApiTests
{
    public class OkCallTests : IClassFixture<ApplicationFactory<TestStartup>>
    {
        private readonly WebApplicationFactory<TestStartup> _factory;

        public OkCallTests(ApplicationFactory<TestStartup> factory)
        {
            _factory = factory.AsWebApplicationFactory();
        }

        public static IEnumerable<object[]> TestDataCollection(int? singleTestDataIndex)
        {
            var collection = new List<object[]>
            {
                // Weekends
                new object[] { new TestData().On(DayOfWeek.Friday).FromHour(23.59M).ToHour(30).Expect(6.5M) },
                new object[] { new TestData().On(DayOfWeek.Friday).FromHour(23.59M).ToHour(29).Expect(6.5M) },
                new object[] { new TestData().On(DayOfWeek.Friday).FromHour(23.59M).ToHour(31).Expect(16.5M) },
                new object[] { new TestData().On(DayOfWeek.Saturday).FromHour(0).ToHour(25).Expect(20) },
                new object[] { new TestData().On(DayOfWeek.Saturday).FromHour(0).ToHour(23).Expect(10) },
                new object[] { new TestData().On(DayOfWeek.Sunday).FromHour(0).ToHour(24).Expect(10) },
                new object[] { new TestData().On(DayOfWeek.Sunday).FromHour(2).ToHour(20).Expect(10) },
                new object[] { new TestData().On(DayOfWeek.Sunday).FromHour(6).ToHour(25).Expect(16.5M) },
                new object[] { new TestData().On(DayOfWeek.Sunday).FromHour(6).ToHour(30).Expect(16.5M) },
                new object[] { new TestData().On(DayOfWeek.Sunday).FromHour(6).ToHour(32).Expect(30M) },

                // Weekdays
                new object[] { new TestData().On(DayOfWeek.Monday).FromHour(0).ToHour(6).Expect(6.5M) },
                new object[] { new TestData().On(DayOfWeek.Monday).FromHour(6).ToHour(15.5M).Expect(13) },
                new object[] { new TestData().On(DayOfWeek.Monday).FromHour(6).ToHour(15.501M).Expect(13) },
                new object[] { new TestData().On(DayOfWeek.Monday).FromHour(7).ToHour(10).Expect(15) },
                new object[] { new TestData().On(DayOfWeek.Monday).FromHour(7).ToHour(23.5M).Expect(13) },
                new object[] { new TestData().On(DayOfWeek.Monday).FromHour(7).ToHour(23.6M).Expect(20) },
                new object[] { new TestData().On(DayOfWeek.Monday).FromHour(10).ToHour(15.5M).Expect(20) },
                new object[] { new TestData().On(DayOfWeek.Monday).FromHour(0).ToHour(8).Expect(20) },
                new object[] { new TestData().On(DayOfWeek.Monday).FromHour(5).ToHour(24.01M).Expect(40M) },

                new object[] { new TestData().On(DayOfWeek.Thursday).FromHour(0).ToHour(6).Expect(6.5M) },
                new object[] { new TestData().On(DayOfWeek.Thursday).FromHour(6).ToHour(15.5M).Expect(13) },
                new object[] { new TestData().On(DayOfWeek.Thursday).FromHour(6).ToHour(15.501M).Expect(13) },
                new object[] { new TestData().On(DayOfWeek.Thursday).FromHour(7).ToHour(10).Expect(15) },
                new object[] { new TestData().On(DayOfWeek.Thursday).FromHour(7).ToHour(23.5M).Expect(13) },
                new object[] { new TestData().On(DayOfWeek.Thursday).FromHour(7).ToHour(23.6M).Expect(20) },
                new object[] { new TestData().On(DayOfWeek.Thursday).FromHour(10).ToHour(15.5M).Expect(20) },
                new object[] { new TestData().On(DayOfWeek.Thursday).FromHour(0).ToHour(8).Expect(20) },
                new object[] { new TestData().On(DayOfWeek.Thursday).FromHour(5).ToHour(24.01M).Expect(40M) },
            };

            return singleTestDataIndex.HasValue
                ? new List<object[]>(collection.Skip(singleTestDataIndex.Value).Take(1))
                : collection;
        }

        [Theory]
        [MemberData(nameof(TestDataCollection), parameters: null)]
        public async Task Should_Return_ExpectedFeeAmount_Given_Valid_QueryParameters(TestData testData)
        {
            var entry = DateTime.Now.DayOfTheWeek(testData.DayOfWeek)
                .AddHours((double)testData.Duration.Min);
            var exit = DateTime.Now.DayOfTheWeek(testData.DayOfWeek)
                .AddHours((double)testData.Duration.Max);

            var httpClient = _factory.CreateClient();

            var response = await httpClient.GetAsync($"/parking-fee/?from={entry:yyyy-MM-dd HH:mm}&&to={exit:yyyy-MM-dd HH:mm}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var feeAmountString = await response.Content.ReadAsStringAsync();
            decimal.Parse(feeAmountString).Should().Be(testData.ExpectedPrice);
        }
    }
}