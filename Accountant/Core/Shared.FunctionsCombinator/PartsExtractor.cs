using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using NewModel.Shared.Utils;

namespace NewModel.Shared.FunctionsCombinator
{
	public sealed class PartsExtractor : IPartsExtractor
	{
		internal object Args { get; private set; }

		public PartsExtractor(object args)
		{
			Args = args;
		}

		public IEnumerable<Part> ExtractArgs<TArgs>()
		{
			return typeof(TArgs).GetProperties().Select(property => new Part
				{
					Contract = property.Name,
					Action = a => property.GetValue(Args),
					DependsOn = new List<string>(),
				});
		}

		public IEnumerable<Part> ExtractActors<TContainer>()
		{
			foreach (var methodInfo in typeof(TContainer).GetMethods())
				if (methodInfo.DeclaringType == typeof (TContainer))
					if (!methodInfo.IsStatic) throw new Exception("Method " + methodInfo.Name + 
						" is not static. Container class can only have static methods declared!");
					else yield return new Part
							{
								Contract = methodInfo.Name,
								Action = a => methodInfo.Invoke(null, a),
								DependsOn = methodInfo.GetParameters().Select(p => p.Name.UppercaseFirstLetter()).ToList(),
							};
		}
	}
}