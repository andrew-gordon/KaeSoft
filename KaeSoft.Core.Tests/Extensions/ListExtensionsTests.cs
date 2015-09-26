using System.Collections.Generic;
using System.Linq;
using Andy.Lib.Extensions;
using NUnit.Framework;

namespace Andy.Lib.Tests.Extensions
{
    [TestFixture]
    public class ListExtensionsTests
    {
        [Test]
        [TestCase(new int[] { }, new int[] { })]
        [TestCase(new[] { 1 }, new int[] { })]
        [TestCase(new[] {1, 2}, new[] {1})]
        [TestCase(new[] {1, 2, 3}, new[] {1, 2})]
        [TestCase(new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}, new[] {1, 2, 3, 4, 5, 6, 7, 8, 9})]
        public void RemoveLastTest(IEnumerable<int> input, IEnumerable<int> expected)
        {
            var inputList = input.ToList();
            inputList.RemoveLast();

            Assert.That(expected.SequenceEqual(inputList));
        }
    }
}
