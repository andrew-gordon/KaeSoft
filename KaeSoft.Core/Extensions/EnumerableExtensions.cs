using System;
using System.Collections.Generic;
using System.Linq;
using KaeSoft.Core.Classes;

namespace KaeSoft.Core.Extensions
{
    public static class EnumerableExtensions
    {
        public static T GetRandomValue<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");

            var randomiser = new Randomiser();
            var randomValue = randomiser.GetRandomValue(enumerable);
            return randomValue;
        }

        public static IEnumerable<T> RandomiseOrder<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");

            return enumerable.OrderBy(x => Guid.NewGuid());
        }

        public static IEnumerable<PositionalItem<T>> SelectWithPosition<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable==null)
                throw new ArgumentNullException("enumerable");

            //var isFirst = true;
            var index = 0;

            using (var enumerator = enumerable.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    bool isLast;
                    do
                    {
                        var current = enumerator.Current;
                        isLast = !enumerator.MoveNext();

                        yield return new PositionalItem<T>(current, index++, isLast);

                        //isFirst = false;
                    } while (!isLast);
                }
            }

        }

        public static IEnumerable<T> Do<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source==null)
                throw new ArgumentNullException("source");

            if (action == null)
                throw new ArgumentNullException("action");

            foreach (var item in source)
            {
                action(item);
                yield return item;
            }
        }

        public static IEnumerable<Tuple<T1, T2>> FullOuterJoin<T1, T2>
            (this IEnumerable<T1> one, IEnumerable<T2> two, Func<T1, T2, bool> match)
        {
            // http://stackoverflow.com/questions/11577876/synchronizing-two-enumerables
            // http://blogs.geniuscode.net/RyanDHatch/?p=116

            var left = from a in one
                from b in two.Where((b) => match(a, b)).DefaultIfEmpty()
                select new Tuple<T1, T2>(a, b);

            var right = from b in two
                from a in one.Where((a) => match(a, b)).DefaultIfEmpty()
                select new Tuple<T1, T2>(a, b);

            return left.Concat(right).Distinct();
        }

        public class PositionalItem<T>
        {
            public PositionalItem(T value, int index, bool isLast)
            {
                Value = value;
                Index = index;
                IsLast = isLast;
            }

            public T Value { get; private set; }
            public int Index { get; private set; }
            public bool IsLast { get; private set; }

            public bool IsFirst
            {
                get { return Index == 0; }
            }
        }
    }
}