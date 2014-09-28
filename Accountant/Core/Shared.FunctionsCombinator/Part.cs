using System;
using System.Collections.Generic;

using NewModel.Shared.Annotations;

namespace NewModel.Shared.FunctionsCombinator
{
	[DataOnlyObject]
	public sealed class Part
	{
		public string Contract { get; set; }
		public object Value { get; set; }
		public bool IsVisited { get; set; }
		public bool IsCalculated { get; set; }
		public List<string> DependsOn { get; set; }
		public Func<object[], object> Action { get; set; }
	}
}