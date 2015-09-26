using System;

namespace KaeSoft.Core.Extensions
{
    public static class ArrayExtensions
    {
        public static void Shuffle<T>(this T[] source)
        {
            var random = new Random(unchecked(Environment.TickCount * 31));
            var n = source.Length;
            while (n > 1)
            {
                n--;
                var k = random.Next(n + 1);
                T value = source[k];
                source[k] = source[n];
                source[n] = value;
            }
        }
    }
}
