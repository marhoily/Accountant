
namespace NewModel.Shared.Utils
{
	public static class StringExtensions
	{

		public static string UppercaseFirstLetter(this string input)
		{
			if (input == null) return null;
			if (input.Length < 2) return input.ToUpper();
			return char.ToUpper(input[0]) + input.Substring(1);
		}
	}
}