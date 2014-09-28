using System;

using NewModel.Shared.Annotations.ReSharper;

namespace NewModel.Shared.Utils
{
	public sealed class AutoDisposable : IDisposable
    {
        readonly Action mAction;

        public AutoDisposable([NotNull] Action action)
        {
            if (action == null) throw new ArgumentNullException("action");
            mAction = action;
        }

        public void Dispose()
        {
            mAction();
        }
    }
}