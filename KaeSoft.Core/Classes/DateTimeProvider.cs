using System;
using KaeSoft.Core.Interfaces;

namespace KaeSoft.Core.Classes
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow
        {
            get { return DateTime.UtcNow; }
        }
    }
}