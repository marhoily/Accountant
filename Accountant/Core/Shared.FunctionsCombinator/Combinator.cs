namespace NewModel.Shared.FunctionsCombinator
{
	public sealed class Combinator<TArgs, TContainer, TResult> where TResult : new()
	{
		readonly ICombinatorFactories mCombinatorFactories;
		public Combinator()
		{
			mCombinatorFactories = new CombinatorFactories();
		}

		internal Combinator(ICombinatorFactories combinatorFactories)
		{
			mCombinatorFactories = combinatorFactories;
		}

		public TResult Evaluate(TArgs args)
		{
			var extractor = mCombinatorFactories.CreatePartsExtractor(args);
			var resultComposer = mCombinatorFactories.CreateResultComposer();
			var parts = mCombinatorFactories.ExtractParts<TContainer, TArgs>(extractor);
			var calculator = mCombinatorFactories.CreateCalculator(parts);
			return resultComposer.Compose<TResult>(calculator);
		}

	}
}