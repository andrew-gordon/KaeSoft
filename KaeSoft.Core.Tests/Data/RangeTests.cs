using System;
using KaeSoft.Core.Data;
using NUnit.Framework;

namespace KaeSoft.Core.Tests.Data
{
    [TestFixture]
    public class RangeTests
    {
        [Test]
        public void RangeTest_FromAndToSpecifiedCorrectly()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Assert.DoesNotThrow( () => new Range<int>(0, 10));
        }

        [Test]
        public void ToStringTest()
        {
            var range = new Range<int>(0, 10);
            var text = range.ToString();
            Assert.AreEqual("[0 - 10]", text);
        }

        [Test]
        [TestCase(1, 10, 2,6, true)]
        [TestCase(1, 10, 1,10, true)]
        [TestCase(1, 10, 1,9, true)]
        [TestCase(1, 10, 2,9, true)]
        public void IsInsideRangeTest(int outerRangeMin, int outerRangeMax, int innerRangeMin, int innerRangeMax, bool expected)
        {
            var outerRange = new Range<int>(outerRangeMin, outerRangeMax);
            var innerRange = new Range<int>(innerRangeMin, innerRangeMax);
            var isInsideRange = innerRange.IsInsideRange(outerRange);
            Assert.AreEqual(expected, isInsideRange);
        }

        [Test]
        [TestCase(1, 10, 2, 6, true)]
        [TestCase(1, 10, 1, 10, true)]
        [TestCase(1, 10, 1, 9, true)]
        [TestCase(1, 10, 2, 9, true)]
        public void ContainsRangeTest(int outerRangeMin, int outerRangeMax, int innerRangeMin, int innerRangeMax, bool expected)
        {
            var outerRange = new Range<int>(outerRangeMin, outerRangeMax);
            var innerRange = new Range<int>(innerRangeMin, innerRangeMax);
            var isInsideRange = outerRange.ContainsRange(innerRange);
            Assert.AreEqual(expected, isInsideRange);
        }

        [Test]
        [TestCase(1, 10, -1, false)]
        [TestCase(1, 10, 0, false)]
        [TestCase(1, 10, 1, true)]
        [TestCase(1, 10, 2, true)]
        [TestCase(1, 10, 3, true)]
        [TestCase(1, 10, 4, true)]
        [TestCase(1, 10, 5, true)]
        [TestCase(1, 10, 6, true)]
        [TestCase(1, 10, 7, true)]
        [TestCase(1, 10, 8, true)]
        [TestCase(1, 10, 9, true)]
        [TestCase(1, 10, 10, true)]
        [TestCase(1, 10, 11, false)]
        [TestCase(1, 10, 100, false)]
        public void ContainsValue(int rangeMin, int rangeMax, int value, bool expected)
        {
            var range = new Range<int>(rangeMin, rangeMax);
            var containsValue = range.ContainsValue(value);
            Assert.AreEqual(expected, containsValue);
        }

        [Test]
        public void EqualsObj_Test_Negative()
        {
            var t = "Hello";
            var range = new Range<int>(0, 10);
            var equals = range.Equals(t);
            Assert.IsFalse(equals);
        }

        [Test]
        public void Equals_Test_Null()
        {
            Range<int> t = null;
            var range = new Range<int>(0, 10);
            var equals = range.Equals(t);
            Assert.IsFalse(equals);
        }

        [Test]
        public void Equals_Test_Null_Object()
        {
            object t = null;
            var range = new Range<int>(0, 10);
            var equals = range.Equals(t);
            Assert.IsFalse(equals);
        }

        [Test]
        public void Equals_Test_Different_Range()
        {
            var range1 = new Range<int>(0, 10);
            var range2 = new Range<int>(0, 100);
            var equals = range1.Equals(range2);
            Assert.IsFalse(equals);
        }

        [Test]
        public void Equals_Test_Same_Range()
        {
            var range1 = new Range<int>(0, 10);
            var range2 = new Range<int>(0, 10);
            var equals = range1.Equals(range2);
            Assert.That(equals);
        }

        [Test]
        public void Equals_Test_Same_Reference()
        {
            var range = new Range<int>(0, 10);
            var equals = range.Equals(range);
            Assert.That(equals);
        }

        [Test]
        public void Base_Equals_Test_Same_Reference()
        {
            var range = new Range<int>(0, 10);
            var equals = ((object)range).Equals(range);
            Assert.That(equals);
        }


        [Test]
        public void Base_Equals_Test_Same_Range()
        {
            var range1 = new Range<int>(0, 10);
            var range2 = new Range<int>(0, 10);
            var equals = ((object)range1).Equals(range2);
            Assert.That(equals);
        }

        [Test]
        public void Equals_HashCodes_Differ_For_Two_Different_Ranges()
        {
            var range1 = new Range<int>(0, 10);
            var range2 = new Range<int>(0, 12);
            Assert.AreNotEqual(range1.GetHashCode(), range2.GetHashCode());
        }

        [Test]
        public void Equals_HashCodes_Equal_For_Same_Range()
        {
            var range1 = new Range<int>(0, 10);
            var range2 = new Range<int>(0, 10);
            Assert.AreEqual(range1.GetHashCode(), range2.GetHashCode());
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Invalid_Range()
        {
            new Range<int>(11, 10);
        }

    }
}
