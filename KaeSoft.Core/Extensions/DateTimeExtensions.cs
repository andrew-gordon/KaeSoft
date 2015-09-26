using System;

namespace KaeSoft.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime ToNextWeekDay(this DateTime date)
        {
            var nextWeekDay = date.AddDays(1);

            while (nextWeekDay.DayOfWeek == DayOfWeek.Saturday || nextWeekDay.DayOfWeek == DayOfWeek.Sunday)
            {
                nextWeekDay = nextWeekDay.AddDays(1);
            }

            return nextWeekDay;
        }
    }
}
