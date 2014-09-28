using System;
using System.Collections.Generic;
using System.Linq;

using NewModel.Accounting.Core;

namespace NewModel.Accounting.Calculation
{
    public interface IBalanceCalculator {
        Total Calculate(IEnumerable<Transaction> transactions);
    }
    public sealed class BalanceCalculator : IBalanceCalculator
    {
        readonly Calculator mCalculator;

        public BalanceCalculator(Calculator calculator)
        {
            mCalculator = calculator;
        }

        public Total Calculate(IEnumerable<Transaction> transactions)
        {
            var transactionsBuffer = transactions.ToList();
            return new Total
            {
                Credit = mCalculator.Credits(transactionsBuffer),
                Debit = mCalculator.Debits(transactionsBuffer)
            };
        }
    }
}