using System;

using NewModel.Shared.Annotations;
using NewModel.Shared.Attributes;

namespace NewModel.Accounting.Core
{
    [DataOnlyObject]
    public sealed class Account
	{
        public string Name { get; set; }
        public string Description { get; set; }
        public AccountOptions Options { get; set; }
        public AccountModifiers Modifier { get; set; }
        public DateTime LastRevisionDate { get; set; }
        [Navigation]
        public Account Parent { get; set; }
        [Navigation]
        public Currency PrimaryCurrency { get; set; }
		public override string ToString() { return Name; }

        #region ' Known accounts '

        public static readonly Account Root = new Account
        {
            Name = "All",
            Description = "All accounts",
            Modifier = AccountModifiers.CannotBeUsedInTransactionsDirectly,
            PrimaryCurrency = Currency.Usd,
        };

        // http://en.wikipedia.org/wiki/Debits_and_credits
        public static readonly Account Asset = new Account
        {
            Parent = Root,
            Name = "Assets",
            Description = "Assets",
            Modifier = AccountModifiers.CannotBeUsedInTransactionsDirectly,
            PrimaryCurrency = Currency.Usd,
        };
        public static readonly Account Liability = new Account
        {
            Parent = Root,
            Name = "Liabilities",
            Description = "Liabilities",
            Modifier = AccountModifiers.CannotBeUsedInTransactionsDirectly,
            PrimaryCurrency = Currency.Usd,
        };
        public static readonly Account Income = new Account
        {
            Parent = Root,
            Name = "Incomes",
            Description = "Incomes",
            Modifier = AccountModifiers.CannotBeUsedInTransactionsDirectly,
            PrimaryCurrency = Currency.Usd,
        };
        public static readonly Account Expense = new Account
        {
            Parent = Root,
            Name = "Expenses",
            Description = "Expenses",
            Modifier = AccountModifiers.CannotBeUsedInTransactionsDirectly,
            PrimaryCurrency = Currency.Usd,
        };
        public static readonly Account Equity = new Account
        {
            Parent = Root,
            Name = "Equities",
            Description = "Equities",
            Modifier = AccountModifiers.CannotBeUsedInTransactionsDirectly,
            PrimaryCurrency = Currency.Usd,
            Options = AccountOptions.Hidden,
        };

        public static readonly Account Exchange = new Account
        {
            Parent = Root,
            Name = "Exchange",
            Description = "Exchange operations",
        };

        public static readonly Account Starting = new Account
        {
            Parent = Root,
            Name = "Starting",
            Description = "Account that credits everyone at start",
            PrimaryCurrency = Currency.Usd,
        };
        public static readonly Account Cache = new Account
        {
            Parent = Root,
            Name = "Cache",
            Description = "Account you would debit and immediately credit everything for caching purposes",
            PrimaryCurrency = Currency.Usd,
            Options = AccountOptions.Hidden,
        };
        public static readonly Account Secondary = new Account
        {
            Parent = Root,
            Name = "Secondary accounts",
            Description = "Accounts one my add as secondary to transactions",
        };

        #endregion
	}
}