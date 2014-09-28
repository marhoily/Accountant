using System;

using NewModel.Shared.Annotations;
using NewModel.Shared.Attributes;

namespace NewModel.Accounting.Core
{
    [DataOnlyObject]
    public sealed class ExchangeRate
    {
        public DateTime StartAt { get; set; }
        public decimal Rate { get; set; }
        public decimal Scale { get; set; }
        public ExchangeRateSources Source { get; set; }

        [Navigation]
        public Currency CurrencyFrom { get; set; }
        [Navigation]
        public Currency CurrencyTo { get; set; }

        public ExchangeRate() { }

        public ExchangeRate(DateTime startAt, Currency currencyFrom, Currency currencyTo, decimal rate, decimal scale, ExchangeRateSources source)
        {
            StartAt = startAt;
            CurrencyFrom = currencyFrom;
            CurrencyTo = currencyTo;
            Rate = rate;
            Scale = scale;
            Source = source;
        }
    }
}