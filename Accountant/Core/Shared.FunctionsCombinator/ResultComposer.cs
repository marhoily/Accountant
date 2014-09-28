namespace NewModel.Shared.FunctionsCombinator
{
	public sealed class ResultComposer : IResultComposer
	{
		public TResult Compose<TResult>(ICalculator calculator)
			where TResult : new()
		{
			var result = new TResult();
			foreach (var prop in result.GetType().GetProperties())
			{
				prop.SetValue(result, calculator.Evaluate(prop.Name));
			}
			return result;
		}
	}
}