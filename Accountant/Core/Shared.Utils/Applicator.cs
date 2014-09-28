using System;
using System.Collections.Generic;

using NewModel.Shared.Annotations;

namespace NewModel.Shared.Utils
{
    [Draft]
    public sealed class Applicator : IApplicator
    {
        public void Apply<T>(IEnumerable<T> source, Action<T> first, Action<T, T> second)
        {
            var isFirst = true;
            var previous = default(T);
            foreach (var item in source)
            {
                if (isFirst) first(item);
                else second(item, previous);
                previous = item;
                isFirst = false;
            }
        }
    }
}