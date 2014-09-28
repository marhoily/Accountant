using System.Collections.Generic;

namespace NewModel.Shared.FunctionsCombinator
{
	public interface IPartsExtractor {
		IEnumerable<Part> ExtractArgs<TArgs>();
		IEnumerable<Part> ExtractActors<TContainer>();
	}
}