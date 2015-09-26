using System;
using Andy.Lib.Extensions;
using NUnit.Framework;
using System.Collections.Generic;

namespace Andy.Lib.Tests.Extensions
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [Test]
        [TestCase("a", false)]
        [TestCase("z", false)]
        [TestCase(" ", false)]
        [TestCase("!", false)]
        [TestCase("z", false)]
        [TestCase("0", false)]
        [TestCase("1", false)]
        [TestCase("2", false)]
        [TestCase("3", false)]
        [TestCase("4", false)]
        [TestCase("5", false)]
        [TestCase("6", false)]
        [TestCase("7", false)]
        [TestCase("8", false)]
        [TestCase("9", false)]
        [TestCase("س", true)]
        [TestCase("ظ", true)]
        [TestCase("ساینبتسیکبدثصکبثحصخبدوزطئظضچج", true)]
        [TestCase("testساینبتسیکبدثصکبثحصخبدوزطئظضچج", true)]
        [TestCase("01234ساینبتسیکبدثصکبثحصخبدوزطئظضچج", true)]
        public void IsArabicTest(string input, bool expectedResult)
        {
            var isArabic = input.IsArabic();
            Assert.AreEqual(expectedResult, isArabic);
        }

        [TestCase("α", true)]
        [TestCase("β", true)]
        [TestCase("γ", true)]
        [TestCase("δ", true)]
        [TestCase("ε", true)]
        [TestCase("ζ", true)]
        [TestCase("η", true)]
        [TestCase("θ", true)]
        [TestCase("ι", true)]
        [TestCase("κ", true)]
        [TestCase("λ", true)]
        [TestCase("μ", true)]
        [TestCase("ν", true)]
        [TestCase("ξ", true)]
        [TestCase("ο", true)]
        [TestCase("π", true)]
        [TestCase("ρ", true)]
        [TestCase("ε", true)]
        [TestCase("ς", true)]
        [TestCase("σ", true)]
        [TestCase("τ", true)]
        [TestCase("υ", true)]
        [TestCase("φ", true)]
        [TestCase("χ", true)]
        [TestCase("ψ", true)]
        [TestCase("ω", true)]
        [TestCase("01234زطئظضچج", false)]
        [TestCase("abc", false)]
        [TestCase("123", false)]
        public void IsGreekTest(string input, bool expectedResult)
        {
            var isGreek = input.IsGreek();
            Assert.AreEqual(expectedResult, isGreek);
        }

        [TestCase("123", false)]
        [TestCase("abc", false)]
        [TestCase("ก ไก่", true)] // chicken
        [TestCase("ข ไข่", true)] // egg
        [TestCase("ช ช้าง", true)] // elephant
        public void IsThaiTest(string input, bool expectedResult)
        {
            var isThai = input.IsThai();
            Assert.AreEqual(expectedResult, isThai);
        }

        [Test]
        [TestCase("", 10, "")]
        [TestCase("", 0, "")]
        [TestCase("", -1, "")]
        [TestCase("a", 1, "a")]
        [TestCase("a", 2, "a")]
        [TestCase("a", 7, "a")]
        [TestCase("my", 1, "my")]
        [TestCase("my", 2, "my")]
        [TestCase("cat", 1, "cat")]
        [TestCase("cat", 2, "cat")]
        [TestCase("cat", 3, "cat")]
        [TestCase("computer", 1, "computer")]
        [TestCase("computer", 2, "computer")]
        [TestCase("computer", 3, "computer")]
        [TestCase("computer", 4, "c...")]
        [TestCase("computer", 5, "co...")]
        [TestCase("computer", 6, "com...")]
        [TestCase("computer", 7, "comp...")]
        [TestCase("computer", 8, "computer")]
        [TestCase("this is a test", 6, "thi...")]
        [TestCase("my name is Fred", 7, "my n...")]
        public void TruncateTestsWithDefaultSuffix(string input, int maxLength, string expected)
        {
            var actual = input.Truncate(maxLength);
            Assert.AreEqual(expected, actual);
        }


        [TestCase("a", new[] { 'a' }, "")]
        [TestCase("aa", new[] { 'a' }, "")]
        [TestCase("aaa", new[] { 'a' }, "")]
        [TestCase("ab", new[] { 'a' }, "b")]
        [TestCase("abc", new[] { 'a' }, "bc")]
        [TestCase("a", new char[] { }, "a")]
        [TestCase("", null, "")]
        [TestCase("a", null, "a")]
        [TestCase("", new char[] { }, "")]
        public void StripTests(string input, char[] stripChars, string expected)
        {
            var actual = input.Strip(stripChars);
            Assert.AreEqual(expected, actual);
        }


        private const bool MatchCase = false;
        private const bool IgnoreCase = true;

        [TestCase("Alex", null, IgnoreCase, false)] // returns false because word parameter is null or whitespace
        [TestCase("Alex", null, MatchCase, false)] // returns false because word parameter is null or whitespace
        [TestCase("Alex", "", IgnoreCase, false)] // returns false because word parameter is null or whitespace
        [TestCase("Alex", "", MatchCase, false)] // returns false because word parameter is null or whitespace
        [TestCase("Alex", " ", IgnoreCase, false)] // returns false because word parameter is null or whitespace
        [TestCase("Alex", " ", MatchCase, false)] // returns false because word parameter is null or whitespace
        [TestCase("Alex and Emily", " ", IgnoreCase, false)] // returns false because word parameter is null or whitespace
        [TestCase("Alex and Emily", " ", MatchCase, false)] // returns false because word parameter is null or whitespace
        [TestCase("Alex", "\t", IgnoreCase, false)] // returns false because word parameter is null or whitespace
        [TestCase("Alex", "\t", MatchCase, false)] // returns false because word parameter is null or whitespace
        [TestCase("Alex", "Alex", IgnoreCase, true)]
        [TestCase("Alex", "Alex", MatchCase, true)]
        [TestCase("Alex", "alex", IgnoreCase, true)]
        [TestCase("Alex", ".", IgnoreCase, false)] // test with a word containing '.'
        [TestCase("Alex.", ".", IgnoreCase, false)] // test with a word containing '.'
        [TestCase("Alex.", "Alex.", IgnoreCase, false)] // test with a word containing '.'
        [TestCase("Last night we went to a theatre", "the", IgnoreCase, false)]
        [TestCase("Last night we went to the theatre", "the", IgnoreCase, true)]
        [TestCase("Last night we went to a theatre", "theatre", IgnoreCase, true)]
        [TestCase("Last night we went to a theatre.", "theatre", IgnoreCase, true)]    // with punctuation immediately after word
        public void ContainsWord(string input, string word, bool ignoreCase, bool expected)
        {
            var actual = input.ContainsWord(word);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("Alex", new[] { "Alex" }, IgnoreCase, true)]
        [TestCase("Alex", new[] { "alex" }, MatchCase, true)]
        [TestCase("Last night we went to a theatre", new[] { "the" }, IgnoreCase, false)]
        [TestCase("Last night we went to a theatre", new[] { "went", "cat" }, IgnoreCase, true)] // test with one word not in string and another word in the string
        [TestCase("Last night we went to a theatre", new[] { "went", "" }, IgnoreCase, true)] // test with one word not in string and another word in the string
        [TestCase("Last night we went to a theatre", new[] { "went", null }, IgnoreCase, true)] // test with one word not in string and another word in the string
        [TestCase("Last night we went to a theatre", new[] { " " }, IgnoreCase, false)] // test with word list containing a whitespace word
        [TestCase("Last night we went to a theatre", new[] { "\t" }, IgnoreCase, false)] // test with word list containing a whitespace word
        [TestCase("Last night we went to a theatre", new[] { "night", " " }, IgnoreCase, true)] // test with word list containing a whitespace word and one valid word
        [TestCase("Last night we went to a theatre", new[] { "night", "\t" }, IgnoreCase, true)] // test with word list containing a whitespace word and one valid word
        [TestCase("Last night we went to the theatre", new[] { "the" }, IgnoreCase, true)]
        [TestCase("Last night we went to a theatre", new[] { "theatre" }, IgnoreCase, true)]
        [TestCase("Last night we went to a theatre.", new[] { "theatre" }, IgnoreCase, true)] // with punctuation immediately after word
        [TestCase("Last night we went to a theatre,", new[] { "theatre" }, IgnoreCase, true)] // with punctuation immediately after word
        [TestCase("Last night we went to a theatre!", new[] { "theatre" }, IgnoreCase, true)] // with punctuation immediately after word
        [TestCase("Last night we went to a theatre;", new[] { "theatre" }, IgnoreCase, true)] // with punctuation immediately after word
        [TestCase("Do you go to the theatre?", new[] { "theatre" }, IgnoreCase, true)] // with punctuation immediately after word
        [TestCase("THIS GROUP IS FOR PEOPLE WHO ARE INTERESTED IN THE MUSIC THAT WAS PLAYED IN GOA BEFORE THE ADVENT OF PSYTRANCE. A LOT OF THE TRACKS HERE WERE PLAYED BY THE FRENCH DJ LAURENT AT A LOT OF LEGENDARY PARTY'S DURING THIS ERA. A LOT OF DJ'S WOULD HAVE USED ALL OR PART OF THESE TRACKS TO CREATE A TRUE GOA PARTY ATMOSPHERE. MOSTLY MIXING ON TAPE DECKS AND THEN WHEN DAT TAPES CAME ALONG THESE WERE USED. VINYL WOULDNT HOLD UP UNDER THE DUSTY AND HOT CONDITIONS.", new[] { "da" }, IgnoreCase, false)]
        [TestCase("THIS GROUP IS FOR PEOPLE WHO ARE INTERESTED IN THE MUSIC THAT WAS PLAYED IN GOA BEFORE THE ADVENT OF PSYTRANCE. A LOT OF THE TRACKS HERE WERE PLAYED BY THE FRENCH DJ LAURENT AT A LOT OF LEGENDARY PARTY'S DURING THIS ERA. A LOT OF DJ'S WOULD HAVE USED ALL OR PART OF THESE TRACKS TO CREATE A TRUE GOA PARTY ATMOSPHERE. MOSTLY MIXING ON TAPE DECKS AND THEN WHEN DAT TAPES CAME ALONG THESE WERE USED. VINYL WOULDNT HOLD UP UNDER THE DUSTY AND HOT CONDITIONS.", new[] { "cat", "da", "dog" }, IgnoreCase, false)]
        public void ContainsAnyWord(string input, IEnumerable<string> words, bool ignoreCase, bool expected)
        {
            var actual = input.ContainsAnyWord(words);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void ContainsAnyWord_Called_With_Null_WordList()
        {
            var t = "The cat sat on the mat";
            t.ContainsAnyWord(null);
        }
    }

}