using CarPark.Abstractions;
using System;

namespace CarPark
{
    public static class Extensions
    {
        public static DateTime DayOfTheWeek(this DateTime time, DayOfWeek startOfWeek)
        {
            var diff = (7 + (time.DayOfWeek - startOfWeek)) % 7;
            return time.AddDays(-1 * diff).Date;
        }

        public static int MondayBasedValue(this DayOfWeek dayOfWeek)
        {
            return (dayOfWeek == DayOfWeek.Sunday ? 7 : (int)dayOfWeek) - 1;
        }

        public static DateTime MondayOfTheWeek(this DateTime time)
        {
            return time.DayOfTheWeek(DayOfWeek.Monday);
        }

        public static TimeSpan OffsetToWeeklyStart(this DateTime time)
        {
            return time.Subtract(time.WeeklyStart());
        }

        public static DateTime WeeklyStart(this DateTime time)
        {
            return time.Date.Subtract(TimeSpan.FromDays(time.DayOfWeek.MondayBasedValue()));
        }

        public static TEntity WithKey<TEntity>(this TEntity entity, string key) where TEntity : EntityBase
        {
            entity.Key = new Guid(key);
            return entity;
        }
    }
}