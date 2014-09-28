using System;
using System.Collections.Generic;

namespace NewModel.Shared.Utils
{
    public interface IApplicator {
        void Apply<T>(IEnumerable<T> source, Action<T> first, Action<T, T> second);
    }
}