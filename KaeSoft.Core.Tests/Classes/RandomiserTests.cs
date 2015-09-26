using System;
using System.Collections.Generic;
using System.Linq;
using KaeSoft.Core.Classes;
using NUnit.Framework;

namespace KaeSoft.Core.Tests.Classes
{
    [TestFixture]
    public class RandomiserTests
    {
        [Test]
        public void GenerateIntegerTest()
        {
            const int minValueToGenerate = 0;
            const int maxValueToGenerate = 10; 
            
            var randomiser = new Randomiser();
            var results = new List<int>();

            for (var i = 0; i < 100000; i++)
            {
                results.Add(randomiser.GenerateInteger(minValueToGenerate, maxValueToGenerate));
            }

            var distinctResults = results.Distinct().Count();
            Assert.That(distinctResults > 5); // assumption

            var minValueGenerated = results.Min();
            var maxValueGenerated = results.Max();
            Assert.AreNotEqual(minValueGenerated, maxValueGenerated);
            Assert.AreEqual(minValueToGenerate, minValueGenerated);
            Assert.AreEqual(maxValueToGenerate, maxValueGenerated);
        }

        [Test]
        public void GenerateDoubleTest()
        {
            const int minValueToGenerate = 0;
            const int maxValueToGenerate = 10;

            var randomiser = new Randomiser();
            var results = new List<double>();
            
            for (var i = 0; i < 100000; i++)
            {
                results.Add(randomiser.GenerateDouble(minValueToGenerate, maxValueToGenerate));
            }

            var distinctResults = results.Distinct().Count();
            Assert.That(distinctResults > 1000); // assumption

            const double tolerance = 0.001;
            var minValueGenerated = results.Min();
            var maxValueGenerated = results.Max();
            Assert.That(Math.Abs(minValueGenerated - maxValueGenerated) > tolerance); // check min and max aren't the same (or thereabouts as it's a double)
            Assert.That(Math.Abs(minValueGenerated - minValueToGenerate) < tolerance);
            Assert.That(Math.Abs(maxValueGenerated - maxValueToGenerate) < tolerance);
        }
    }
}
