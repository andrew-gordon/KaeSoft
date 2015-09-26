using System.Collections.Generic;
using System.Linq;

namespace KaeSoft.Core.Extensions
{
    public static class ListExtensions
    {
        public static void RemoveLast<T>(this IList<T> list)
        {
            if (!list.Any()) return;

            var lastItemIndex = list.Count - 1;
            list.RemoveAt(lastItemIndex);
        }
    }
}
