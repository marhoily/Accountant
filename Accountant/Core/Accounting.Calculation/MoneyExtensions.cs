using System;
using System.Collections.Generic;
using System.Linq;

using NewModel.Accounting.Core;

namespace NewModel.Accounting.Calculation
{
    public static class MoneyExtensions
    {
        public static MoneyBag Sum<T>(this IEnumerable<T> source, Func<T, Money> selector)
        {
            return source.Select(selector).Aggregate(new MoneyBag(), (a, b) => a + b);
        }
    }
}