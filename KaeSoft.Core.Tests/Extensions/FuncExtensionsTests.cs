using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using KaeSoft.Core.Extensions;
using NUnit.Framework;

namespace KaeSoft.Core.Tests.Extensions
{
    [TestFixture]
    public class FuncExtensionsTests
    {
        [Test]
        public void Test_Fibonnaci()
        {
            Func<int, BigInteger> fibonacci = null;
            fibonacci = x => x <= 0 ? 0 : (x == 1 ? 1 : BigInteger.Add(fibonacci(x - 1), fibonacci(x - 2)));

            Assert.AreEqual(new BigInteger(0), fibonacci(0));
            Assert.AreEqual(new BigInteger(1), fibonacci(1));
            Assert.AreEqual(new BigInteger(1), fibonacci(2));
            Assert.AreEqual(new BigInteger(2), fibonacci(3));
            Assert.AreEqual(new BigInteger(3), fibonacci(4));
            Assert.AreEqual(new BigInteger(5), fibonacci(5));
            Assert.AreEqual(new BigInteger(8), fibonacci(6));

            Func<int, BigInteger> fasterFibonnaci = n =>
            {
                Func<int, BigInteger> calculator = null;

                calculator = x =>
                {
                    if (x <= 0) return 0;
                    if (x == 1) return 1;

                    // ReSharper disable once PossibleNullReferenceException
                    // ReSharper disable AccessToModifiedClosure
                    return calculator(x - 1) + calculator(x - 2);
                    // ReSharper restore AccessToModifiedClosure
                };

                calculator = calculator.Memoize();

                return calculator(n);
            };

            Assert.AreEqual(new BigInteger(0), fasterFibonnaci(0));
            Assert.AreEqual(new BigInteger(1), fasterFibonnaci(1));
            Assert.AreEqual(new BigInteger(1), fasterFibonnaci(2));
            Assert.AreEqual(new BigInteger(2), fasterFibonnaci(3));
            Assert.AreEqual(new BigInteger(3), fasterFibonnaci(4));
            Assert.AreEqual(new BigInteger(5), fasterFibonnaci(5));
            Assert.AreEqual(new BigInteger(8), fasterFibonnaci(6));
            Assert.AreEqual(BigInteger.Parse("218922995834555169026"), fasterFibonnaci(99));
        }

        [Test]
        public void Test_Fibonnaci_With_Concurrency()
        {
            for (int a = 1; a < 100; a++)
            {
                Func<int, BigInteger> fasterFibonnaci = n =>
                {
                    Func<int, BigInteger> calculator = null;

                    calculator = x =>
                    {
                        if (x <= 0) return 0;
                        if (x == 1) return 1;

                        // ReSharper disable once PossibleNullReferenceException
                        // ReSharper disable AccessToModifiedClosure
                        return calculator(x - 1) + calculator(x - 2);
                        // ReSharper restore AccessToModifiedClosure
                    };

                    calculator = calculator.Memoize();

                    return calculator(n);
                };

                var taskList = new List<Task>();

                for (var i = 1; i < 100; i++)
                {
                    var task = new Task(() =>
                    {
                        Assert.AreEqual(BigInteger.Parse("218922995834555169026"), fasterFibonnaci(99));
                    }, TaskCreationOptions.LongRunning);
                    taskList.Add(task);
                }

                foreach (var task in taskList)
                    task.Start();
                Task.WaitAll(taskList.ToArray());
            }
        }
    }

}