using System;

using FluentAssertions;

using NewModel.Accounting;
using NewModel.Accounting.Calculation;
using NewModel.Accounting.Core;

using NUnit.Framework;

namespace NewModel.UnitTests._IntegrationTests_
{
    [TestFixture]
    public sealed class Integration_CalculatorTests
    {
        Calculator mCalculator;
        Transaction mGotSalary;
        Transaction mReturnLoan;
        Transaction mPayTaxes;
        Transaction mExchange;

        [SetUp]
        public void Setup()
        {
            mCalculator = new Calculator();

            mGotSalary = new Transaction(
                new DateTime(2000, 1, 1), "Got 3р. salary", 3*Currency.Byr,
                credit: Account.Income, debit: Account.Asset);

            mPayTaxes = new Transaction(
                new DateTime(2000, 1, 2), "Pay 1р. taxes", 1 * Currency.Byr,
                credit: Account.Income, debit: Account.Asset);

            mReturnLoan = new Transaction(
                new DateTime(2000, 1, 3), "Exchange 5р. to return $3 loan",
                credit: new EndPoint(5 * Currency.Byr, Account.Asset),
                debit:  new EndPoint(3 * Currency.Usd, Account.Liability));

            mExchange = new Transaction(
                new DateTime(2000, 1, 4), "Exchange 5р. to $3 with 2 transactions",
                new Record(5 * Currency.Byr, Account.Asset, Account.Exchange),
                new Record(3 * Currency.Usd, Account.Exchange, Account.Asset));
        }

        [Test]
        public void Balance_Should_Sum_Up_Debits_And_Credits()
        {
            mCalculator.Balance(new[] { mGotSalary, mPayTaxes, mReturnLoan, mExchange })
                .ToString().Should().Be("5.00р., $-3.00");
        }
    }
}