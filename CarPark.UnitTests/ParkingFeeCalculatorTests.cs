using CarPark.Models;
using CarPark.Persistence.InMemory;
using CarPark.Services;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CarPark.UnitTests
{
    public class ParkingFeeCalculatorTests
    {
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
        public async Task Should_Return_ExpectedFeeAmount_Given_TimeEntry_And_TimeExit(TestData testData)
        {
            var service = new ParkingFeeCalculator(new InMemoryRepository(new InMemoryStore()));
            var timeEntry = DateTime.Now.DayOfTheWeek(testData.DayOfWeek)
                .AddHours((double)testData.Duration.Min);
            var timeExit = DateTime.Now.DayOfTheWeek(testData.DayOfWeek)
                .AddHours((double)testData.Duration.Max);

            var feeAmount = await service.GetFeeAmount(timeEntry, timeExit);
            feeAmount.Should().Be(testData.ExpectedPrice);
        }

        [Fact]
        public async Task Should_Throw_Exception_Given_TimeEntry_Greater_Than_TimeExit()
        {
            var service = new ParkingFeeCalculator(new InMemoryRepository(new InMemoryStore()));
            var timeEntry = DateTime.Now;
            var timeExit = timeEntry.AddHours(-2);

            Func<Task> act = async () => await service.GetFeeAmount(timeEntry, timeExit);
            await act.Should()
                .ThrowAsync<ProcessException>()
                .WithMessage("Exit time is greater than entry time.");
        }

        [Fact]
        public async Task Should_Throw_Exception_Given_No_DefaultFlatRate()
        {
            var store = new InMemoryStore();
            store.DefaultFlatRates.Clear();
            var repository = new InMemoryRepository(store);
            var service = new ParkingFeeCalculator(repository);
            var timeEntry = DateTime.Parse("2020-07-07").AddHours(10);
            var timeExit = timeEntry.AddHours(4);

            Func<Task> act = async () => await service.GetFeeAmount(timeEntry, timeExit);
            await act.Should().ThrowAsync<ProcessException>().WithMessage("Internal data error.");
        }

        [Fact]
        public async Task Should_Throw_Exception_Given_MoreThanOne_DefaultFlatRates()
        {
            var store = new InMemoryStore();
            store.DefaultFlatRates.Add(new DefaultFlatRate(23));
            var repository = new InMemoryRepository(store);
            var service = new ParkingFeeCalculator(repository);
            var timeEntry = DateTime.Parse("2020-07-07").AddHours(10);
            var timeExit = timeEntry.AddHours(4);

            Func<Task> act = async () => await service.GetFeeAmount(timeEntry, timeExit);
            await act.Should().ThrowAsync<ProcessException>().WithMessage("Internal data error.");
        }
    }
}