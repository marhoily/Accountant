using System.Collections.Generic;

namespace NewModel.Shared.FunctionsCombinator
{
	public interface ICombinatorFactories {
		ICalculator CreateCalculator(Dictionary<string, Part> parts);
		IResultComposer CreateResultComposer();
		IPartsExtractor CreatePartsExtractor<TArgs>(TArgs args);
		Dictionary<string, Part> ExtractParts<TContainer, TArgs>(IPartsExtractor extractor);
	}
}