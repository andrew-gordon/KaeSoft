using System;
using Andy.Lib.Interfaces;
using Andy.Lib.Properties;

namespace Andy.Lib.Classes
{
    [UsedImplicitly]
// ReSharper disable once ClassNeverInstantiated.Global
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow
        {
            get { return DateTime.UtcNow; }
        }
    }
}