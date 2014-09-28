using System.Collections.Generic;
using System.Linq;

namespace NewModel.Shared.FunctionsCombinator
{
	public sealed class CombinatorFactories : ICombinatorFactories
	{
		public ICalculator CreateCalculator(Dictionary<string, Part> parts)
		{
			return new Calculator(parts);
		}
		public IResultComposer CreateResultComposer()
		{
			return new ResultComposer();
		}
		public IPartsExtractor CreatePartsExtractor<TArgs>(TArgs args)
		{
			return new PartsExtractor(args);
		}
		public Dictionary<string, Part> ExtractParts<TContainer, TArgs>(IPartsExtractor extractor)
		{
			var extractArgs = extractor.ExtractArgs<TArgs>();
			var extractActors = extractor.ExtractActors<TContainer>();
			return extractArgs.Concat(extractActors).ToDictionary(p => p.Contract, p => p);
		}
	}
}