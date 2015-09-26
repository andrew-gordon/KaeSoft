using System;
using System.Threading;
using Andy.Lib.Classes;
using NUnit.Framework;

namespace Andy.Lib.Tests.Classes
{
    [TestFixture]
    public class DateTimeProviderTests
    {
        [Test]
        public void UtcNow_Test()
        {
            var dateTimeProvider = new DateTimeProvider();

            var d1 = dateTimeProvider.UtcNow;
            Thread.Sleep(TimeSpan.FromMilliseconds(10));
            var d2 = dateTimeProvider.UtcNow;
            Assert.AreNotEqual(d1, d2);
            Assert.Greater(d2, d1);
        }
    }
}
