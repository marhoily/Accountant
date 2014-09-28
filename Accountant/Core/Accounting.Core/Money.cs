using System;

using NewModel.Shared.Attributes;

namespace NewModel.Accounting.Core
{
    public sealed class Money
	{
		public decimal Amount { get; set; }
        [Navigation]
		public Currency Currency { get; set; }
        public Money() {}

        public Money(Currency currency, decimal amount)
		{
			Amount = amount;
			Currency = currency;
		}
		public override string ToString()
		{
			switch (Currency.SymbolPosition)
			{
				case SymbolPosition.Prepend:
					return Currency.Symbol + Amount.ToString("N");
				case SymbolPosition.Append:
					return Amount.ToString("N") + Currency.Symbol;
				default:
					throw new NotSupportedException();
			}
		}

		public static Money operator -(Money x)
		{
			return new Money(x.Currency, -x.Amount);
		}
	}
}