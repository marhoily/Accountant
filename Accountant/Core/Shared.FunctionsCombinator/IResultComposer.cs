namespace NewModel.Shared.FunctionsCombinator
{
	public interface IResultComposer {
		TResult Compose<TResult>(ICalculator calculator)
			where TResult : new();
	}
}