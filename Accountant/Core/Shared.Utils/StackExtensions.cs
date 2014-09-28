using System;
using System.Collections.Generic;

namespace NewModel.Shared.Utils
{
	public static class StackExtensions
	{
		public static IDisposable AutoPush<T>(this Stack<T> destination, T item)
		{
			destination.Push(item);
			return new AutoDisposable(() => destination.Pop());
		}
	}
}