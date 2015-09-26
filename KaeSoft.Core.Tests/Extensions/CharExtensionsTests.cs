using KaeSoft.Core.Extensions;
using NUnit.Framework;

namespace KaeSoft.Core.Tests.Extensions
{
    [TestFixture]
    public class CharExtensionsTests
    {
        [Test]
        [TestCase('a', false)]
        [TestCase('z', false)]
        [TestCase(' ', false)]
        [TestCase('!', false)]
        [TestCase('z', false)]
        [TestCase('0', false)]
        [TestCase('1', false)]
        [TestCase('2', false)]
        [TestCase('3', false)]
        [TestCase('4', false)]
        [TestCase('5', false)]
        [TestCase('6', false)]
        [TestCase('7', false)]
        [TestCase('8', false)]
        [TestCase('9', false)]
        [TestCase('س', true)]
        [TestCase('ظ', true)]
        [TestCase('ج', true)]
        public void IsArabicTest(char input, bool expectedResult)
        {
            var isArabic = input.IsArabic();
            Assert.AreEqual(expectedResult, isArabic);
        }

        [TestCase('1', false)]
        [TestCase('a', false)]
        [TestCase('\x0638', false)] // Arabic
        [TestCase('\x0E02', true)] 
        [TestCase('\x0E32', true)] 
        public void IsThaiTest(char input, bool expectedResult)
        {
            var isThai = input.IsThai();
            Assert.AreEqual(expectedResult, isThai);
        }

        [TestCase('α', true)]
        [TestCase('β', true)]
        [TestCase('γ', true)]
        [TestCase('δ', true)]
        [TestCase('ε', true)]
        [TestCase('ζ', true)]
        [TestCase('η', true)]
        [TestCase('θ', true)]
        [TestCase('ι', true)]
        [TestCase('κ', true)]
        [TestCase('λ', true)]
        [TestCase('μ', true)]
        [TestCase('ν', true)]
        [TestCase('ξ', true)]
        [TestCase('ο', true)]
        [TestCase('π', true)]
        [TestCase('ρ', true)]
        [TestCase('ε', true)]
        [TestCase('ς', true)]
        [TestCase('σ', true)]
        [TestCase('τ', true)]
        [TestCase('υ', true)]
        [TestCase('φ', true)]
        [TestCase('χ', true)]
        [TestCase('ψ', true)]
        [TestCase('ω', true)]
        public void IsGreekTest(char input, bool expectedResult)
        {
            var isGreek = input.IsGreek();
            Assert.AreEqual(expectedResult, isGreek);
        }
    }
}
