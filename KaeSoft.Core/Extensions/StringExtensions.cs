using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Andy.Lib.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Truncates a string to a given number of characters.  If the string is longer than the maximum length 
        /// specified then an optional suffix may be added (within the permitted size of the string).
        /// </summary>
        /// <param name="text">The string to truncate</param>
        /// <param name="maxLength">The maximum length of the resulting string</param>
        /// <param name="suffix">An optional suffix if the input string exceeds the maximum length</param>
        /// <returns>Truncated string</returns>
        public static string Truncate(this string text, int maxLength, string suffix = "...")
        {
            string str = text;
            if (maxLength > 0)
            {
                int length = maxLength - suffix.Length;
                if (length <= 0)
                {
                    return str;
                }
                if ((text != null) && (text.Length > maxLength))
                {
                    return text.Substring(0, length) + suffix;
                }
            }
            return str;
        }

        /// <summary>
        /// Indicates whether the specified string contains Arabic characters.
        /// </summary>
        /// <param name="input">String to test</param>
        /// <returns>true if <paramref name="input"/> contains Arabic characters; otherwise, false.</returns>
        public static bool IsArabic(this string input)
        {
            var isArabic = Regex.IsMatch(input, @"\p{IsArabic}");
            return isArabic;
        }

        /// <summary>
        /// Indicates whether the specified string contains Thai characters.
        /// </summary>
        /// <param name="input">String to test</param>
        /// <returns>true if <paramref name="input"/> contains greek characters; otherwise, false.</returns>
        public static bool IsThai(this string input)
        {
            var isThai = Regex.IsMatch(input, @"\p{IsThai}");
            return isThai;
        }

        /// <summary>
        /// Indicates whether the specified string contains Greek characters.
        /// </summary>
        /// <param name="input">String to test</param>
        /// <returns>true if <paramref name="input"/> contains Greek characters; otherwise, false.</returns>
        public static bool IsGreek(this string input)
        {
            var isGreek = Regex.IsMatch(input, @"\p{IsGreek}");
            return isGreek;
        }

        /// <summary>
        /// Strips out all occurrences of a set of characters from a string
        /// </summary>
        /// <param name="input"></param>
        /// <param name="charactersToStripOut"></param>
        /// <returns>String with all occurrences of the specified characters removed</returns>
        public static string Strip(this string input, IEnumerable<char> charactersToStripOut)
        {
            if (charactersToStripOut == null) return input;

            return charactersToStripOut
                .Select(ch => ch.ToString(CultureInfo.InvariantCulture))
                .Aggregate(input, (current, replaceText) => current.Replace(replaceText, string.Empty));
        }

        /// <summary>
        /// Indicates whether the specified string contains a specified word - but only if the word matches a complete word. 
        /// </summary>
        /// <param name="input">String to test</param>
        /// <param name="word">Word to find</param>
        /// <param name="ignoreCase">Determines whether the check should be case-insensitive.</param>
        /// <returns>true if <paramref name="input"/> contains the specified word; otherwise, false.</returns>
        public static bool ContainsWord(this string input, string word, bool ignoreCase = true)
        {
            if (string.IsNullOrWhiteSpace(word))
                return false;

            return ContainsAnyWord(input, new[] {word}, ignoreCase);

            // Is the following more efficient than reusing ContainsAnyWord?

            //var regexPattern = @"\b" + word + @"\b";
            //var contains = Regex.IsMatch(input, regexPattern, ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.None);
            //return contains;
        }

        /// <summary>
        /// Indicates whether the specified string contains any word specified in a list - but only if the word matches a complete word. 
        /// </summary>
        /// <param name="input">String to test</param>
        /// <param name="words">List of words to find</param>
        /// <param name="ignoreCase">Determines whether the check should be case-insensitive.</param>
        /// <returns>true if <paramref name="input"/> contains the specified word; otherwise, false.</returns>
        public static bool ContainsAnyWord(this string input, IEnumerable<string> words, bool ignoreCase = true)
        {
            if (words == null)
                throw new ArgumentNullException("words");

            var refinedWordList = words.Where(w => !string.IsNullOrWhiteSpace(w)).ToList();
            
            if (refinedWordList.Count == 0)
                return false;

            var regexPattern = string.Join("|", refinedWordList.Select(word => @"\b" + word + @"\b")); // each word must be wrapped as \bWORD\b
            var contains = Regex.IsMatch(input, regexPattern, ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.None);
            return contains;
        }
    }
}
