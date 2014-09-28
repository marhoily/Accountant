using System;
using System.Collections.Generic;
using System.Linq;

using NewModel.Shared.Utils;

namespace NewModel.Shared.FunctionsCombinator
{
    public sealed class Calculator : ICalculator
	{
		internal Dictionary<string, Part> Parts { get; private set; }
		readonly Stack<string> mTrace = new Stack<string>();

		public Calculator(Dictionary<string, Part> parts)
		{
			Parts = parts;
		}
		
		public object Evaluate(string contract)
		{
			using (mTrace.AutoPush(contract))
			{
				Part part;
			    if (!Parts.TryGetValue(contract, out part)) 
                    throw new Exception("Cannot resolve: " + StackTrace());
			    if (part.IsCalculated) return part.Value;
				if (part.IsVisited) throw new Exception("Cycle found at: " + StackTrace());
				part.IsVisited = true;
				var args = part.DependsOn.Select(Evaluate).ToArray();
				part.Value = CallAction(part, args);
				part.IsCalculated = true;
				return part.Value;
			}
		}

		object CallAction(Part part, object[] args)
		{
			object action;
			try
			{
				action = part.Action(args);
			}
			catch (ArgumentException e)
			{
				throw new ArgumentException(StackTrace() + "(" + string.Join(", ", part.DependsOn) + "). " + e.Message);
			}
			return action;
		}

		string StackTrace()
		{
			return string.Join(" -> ", mTrace.Reverse());
		}
	}
}