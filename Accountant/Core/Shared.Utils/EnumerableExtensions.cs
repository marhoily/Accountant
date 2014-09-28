using System;
using System.Collections.Generic;

using NewModel.Shared.Annotations;

namespace NewModel.Shared.Utils
{
    [Draft]
    public static class EnumerableExtensions
    {
        public static void Apply<T>(this IEnumerable<T> source, Action<T> first, Action<T> others)
        {
            var counter = 0;
            foreach (var item in source)
            {
                if (++counter == 1) first(item);
                else others(item);
            }
        }
    }
}