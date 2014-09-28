using System;
using System.Collections.Generic;

namespace NewModel.Shared.ModelReflection
{
    public static class Hierarchy
    {
        public static HashSet<T> Flatten<T>(this T source, Func<T, IEnumerable<T>> selector, IEqualityComparer<T> equalityComparer)
        {
            var visitedItems = new HashSet<T>(equalityComparer);
            InternalFlatten(source, selector, visitedItems);
            return visitedItems;
        }
        public static HashSet<T> Flatten<T>(this T source, Func<T, IEnumerable<T>> selector)
        {
            var visitedItems = new HashSet<T>();
            InternalFlatten(source, selector, visitedItems);
            return visitedItems;
        }
        public static HashSet<T> Flatten<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> selector)
        {
            var visitedItems = new HashSet<T>();
            foreach (var type in source)
                InternalFlatten(type, selector, visitedItems);
            return visitedItems;
        }

        static void InternalFlatten<T>(T source,
            Func<T, IEnumerable<T>> selector, HashSet<T> visitedItems)
        {
            if (!visitedItems.Add(source)) return;
            foreach (var item in selector(source)) 
                InternalFlatten(item, selector, visitedItems);
        }
    }
}