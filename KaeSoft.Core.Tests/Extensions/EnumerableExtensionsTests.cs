using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Andy.Lib.Extensions;
using NUnit.Framework;

namespace Andy.Lib.Tests.Extensions
{
    [TestFixture]
    public class EnumerableExtensionsTests
    {
        [Test]
        [TestCase(new int[] {})]
        [TestCase(new[] {1})]
        [TestCase(new[] {1, 2})]
        [TestCase(new[] {1, 2, 3})]
        [TestCase(new[] {1, 2, 3, 4})]
        public void SelectWithPositionTest(int[] testList)
        {
            var listWithPosition = testList.SelectWithPosition().ToList();

            var i = 0;
            while (i < testList.Length)
            {
                var item = listWithPosition[i];
                var isFirstExpected = i == 0;
                var isLastExpected = i == testList.Length - 1;
                var valueExpected = testList[i];

                Assert.AreEqual(valueExpected, item.Value);
                Assert.AreEqual(isFirstExpected, item.IsFirst);
                Assert.AreEqual(isLastExpected, item.IsLast);
                Assert.AreEqual(i, item.Index);
                i++;
            }
        }


        [TestCase(new[] {1, 2, 3, 4})]
        [TestCase(new[] {1, 2})]
        public void GetRandomValue(int[] testList)
        {
            // call the function 100 times and check we don't have the same number each time

            var results = new List<int>();
            for (var i = 0; i < 100; i++)
            {
                results.Add(testList.GetRandomValue());
            }

            var isAlwaysSameValue = false;
            var eachInputListValueAppearsAtLeastOnce = true;

            foreach (var value in testList)
            {
                if (results.All(x => x != value))
                {
                    eachInputListValueAppearsAtLeastOnce = false;
                }

                if (results.All(x => x == value))
                {
                    isAlwaysSameValue = true;
                }
            }

            Assert.That(!isAlwaysSameValue);
            Assert.That(eachInputListValueAppearsAtLeastOnce);

        }

        [Test]
        public void RandomiseOrderTest()
        {
            const int itemCount = 100;

            var orderedList = Enumerable.Range(0, itemCount).ToList();
            var differentOrderCount = 0;

            for (var i = 0; i < 100; i++)
            {
                var randomOrderList = orderedList.RandomiseOrder().ToList();
                var sameOrder = randomOrderList.SequenceEqual(orderedList);

                if (!sameOrder) differentOrderCount++;
            }

            Assert.Greater(differentOrderCount, 0);
            Assert.GreaterOrEqual(differentOrderCount, (0.8*itemCount)); // Hoping that at least 80% will be different

            System.Diagnostics.Debug.WriteLine("Different order count = {0}/{1}", differentOrderCount, itemCount);
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void DoTest_With_Null_Action()
        {
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            Enumerable.Range(0, 10).Do(null).ToList();
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void DoTest_With_Null_Source()
        {
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            EnumerableExtensions.Do<int>(null, x => { }).ToList();
        }

        [Test]
        public void DoTest()
        {
            var sb = new StringBuilder();

            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            Enumerable.Range(0, 10).Do(x => { sb.Append("A"); }).ToList();
            Assert.AreEqual("AAAAAAAAAA", sb.ToString());
        }

        [Test]
        public void DoTest2()
        {
            var i = 0;

            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            Enumerable.Range(0, 10).Do(x => { i++; }).ToList();
            Assert.AreEqual(10, i);
        }

        [Test]
        public void FullOuterJoinTest()
        {
            var ryan = new Person {Name ="Ryan"};
            var jer = new Person {Name ="Jer"};
            var people = new List<Person> {ryan, jer};

            var camp = new Dog {Name ="Camp", Owner = ryan};
            var brody = new Dog {Name ="Brody", Owner = ryan};
            var homeless = new Dog {Name ="Homeless"};
            var dogs = new List<Dog> {camp, brody, homeless};

            Func<Person, Dog, bool> match =
                (person, dog) => dog != null && ReferenceEquals(dog.Owner, person);

            var join = people.FullOuterJoin(dogs, match).ToList();

            Assert.AreEqual(4, join.Count);
            Assert.IsTrue(join.Contains(new Tuple<Person, Dog>(null, homeless)));
            Assert.IsTrue(join.Contains(new Tuple<Person, Dog>(ryan, camp)));
            Assert.IsTrue(join.Contains(new Tuple<Person, Dog>(ryan, brody)));
            Assert.IsTrue(join.Contains(new Tuple<Person, Dog>(jer, null)));
        }

        private class Dog
        {
            public string Name { get; set; }
            public Person Owner { get; set; }
        }

        private class Person
        {
            public string Name { get; set; }
        }
    }
}
