using NewModel.Shared.Annotations;

namespace NewModel.Accounting.Core
{
    [DataOnlyObject]
    public sealed class Currency
	{
	    #region ' Currencies '

        public static readonly Currency Usd = new Currency
	        {
	            Name = "USD",
	            Symbol = "$",
	            SymbolPosition = SymbolPosition.Prepend,
	            Description = "United states dollar",
	        };
        public static readonly Currency Eur = new Currency
	        {
	            Name = "EUR",
                Symbol = "€",
	            SymbolPosition = SymbolPosition.Prepend,
	            Description = "Euro",
	        };
        public static readonly Currency Byr = new Currency
	        {
	            Name = "BYR",
	            Symbol = "р.",
	            SymbolPosition = SymbolPosition.Append,
	            Description = "Belorussian ruble",
	        };
	    public static readonly Currency Rur = new Currency
	        {
	            Name = "RUR",
	            Symbol = "рр.",
	            SymbolPosition = SymbolPosition.Append,
	            Description = "Russian ruble",
	        };

	    #endregion

		public string Name { get; set; }
		public string Symbol { get; set; }
		public SymbolPosition SymbolPosition { get; set; }
		public string Description { get; set; }

		public static Money operator *(decimal amount, Currency currency)
		{
			return new Money(currency, amount);
		}
	}
}
