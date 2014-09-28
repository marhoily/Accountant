
using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using NewModel.Accounting;
using NewModel.Accounting.Calculation;
using NewModel.Accounting.Core;
using NewModel.Shared.Annotations;

namespace NewModel.Accounting.Persistence
{
    [CompilerGenerated]
	public sealed class Context
	{
		public readonly Dictionary<string, Account> 
			AccountByKey = new Dictionary<string, Account>();

		public readonly Dictionary<string, Currency> 
			CurrencyByKey = new Dictionary<string, Currency>();

	}

    [CompilerGenerated]
	public sealed class AccountData
	{
		public AccountData()
		{
		}
		public AccountData(Account value)
		{
			Name = value.Name;
			Description = value.Description;
			Options = value.Options;
			Modifier = value.Modifier;
			LastRevisionDate = value.LastRevisionDate;
			Parent = value.Parent == null ? null : value.Parent.Name;
			PrimaryCurrency = value.PrimaryCurrency == null ? null : value.PrimaryCurrency.Name;
		}

		public string Name { get; set; }
		public string Description { get; set; }
		public AccountOptions Options { get; set; }
		public AccountModifiers Modifier { get; set; }
		public DateTime LastRevisionDate { get; set; }
		public string Parent { get; set; }
		public string PrimaryCurrency { get; set; }

     
        public Account Back(Context context)
        {
            var result = new Account
				{
					Name = Name,
					Description = Description,
					Options = Options,
					Modifier = Modifier,
					LastRevisionDate = LastRevisionDate,
					Parent = Parent == null ? null : context.AccountByKey[Parent],
					PrimaryCurrency = PrimaryCurrency == null ? null : context.CurrencyByKey[PrimaryCurrency],
				};
			context.AccountByKey[Name] = result;
			return result;
        }
	}
    [CompilerGenerated]
	public sealed class AccountTotalData
	{
		public AccountTotalData()
		{
		}
		public AccountTotalData(AccountTotal value)
		{
			Total = value.Total == null ? null : new TotalData(value.Total);
			Account = value.Account == null ? null : value.Account.Name;
		}

		public TotalData Total { get; set; }
		public string Account { get; set; }

     
        public AccountTotal Back(Context context)
        {
            var result = new AccountTotal
				{
					Total = Total.Back(context),
					Account = Account == null ? null : context.AccountByKey[Account],
				};
			return result;
        }
	}
    [CompilerGenerated]
	public sealed class BatchData
	{
		public BatchData()
		{
		}
		public BatchData(Batch value)
		{
			IngressBalance = value.IngressBalance.Select(x => new AccountTotalData(x)).ToList();
			ReferenceInterval = value.ReferenceInterval;
			Transactions = value.Transactions.Select(x => new TransactionData(x)).ToList();
			ExchangeRates = value.ExchangeRates.Select(x => new ExchangeRateData(x)).ToList();
		}

		public List<AccountTotalData> IngressBalance { get; set; }
		public Interval ReferenceInterval { get; set; }
		public List<TransactionData> Transactions { get; set; }
		public List<ExchangeRateData> ExchangeRates { get; set; }

     
        public Batch Back(Context context)
        {
            var result = new Batch
				{
					IngressBalance = new List<AccountTotal>(IngressBalance.Select(x => x.Back(context))),
					ReferenceInterval = ReferenceInterval,
					Transactions = new List<Transaction>(Transactions.Select(x => x.Back(context))),
					ExchangeRates = new List<ExchangeRate>(ExchangeRates.Select(x => x.Back(context))),
				};
			return result;
        }
	}
    [CompilerGenerated]
	public sealed class CurrencyData
	{
		public CurrencyData()
		{
		}
		public CurrencyData(Currency value)
		{
			Name = value.Name;
			Symbol = value.Symbol;
			SymbolPosition = value.SymbolPosition;
			Description = value.Description;
		}

		public string Name { get; set; }
		public string Symbol { get; set; }
		public SymbolPosition SymbolPosition { get; set; }
		public string Description { get; set; }

     
        public Currency Back(Context context)
        {
            var result = new Currency
				{
					Name = Name,
					Symbol = Symbol,
					SymbolPosition = SymbolPosition,
					Description = Description,
				};
			context.CurrencyByKey[Name] = result;
			return result;
        }
	}
    [CompilerGenerated]
	public sealed class EndPointData
	{
		public EndPointData()
		{
		}
		public EndPointData(EndPoint value)
		{
			Amount = value.Amount == null ? null : new MoneyData(value.Amount);
			Accounts = value.Accounts.Select(x => x.Name).ToArray();
		}

		public MoneyData Amount { get; set; }
		public string[] Accounts { get; set; }

     
        public EndPoint Back(Context context)
        {
            var result = new EndPoint
				{
					Amount = Amount.Back(context),
					Accounts = Accounts.Select(x => context.AccountByKey[x]).ToList(),
				};
			return result;
        }
	}
    [CompilerGenerated]
	public sealed class ExchangeRateData
	{
		public ExchangeRateData()
		{
		}
		public ExchangeRateData(ExchangeRate value)
		{
			StartAt = value.StartAt;
			Rate = value.Rate;
			Scale = value.Scale;
			Source = value.Source;
			CurrencyFrom = value.CurrencyFrom == null ? null : value.CurrencyFrom.Name;
			CurrencyTo = value.CurrencyTo == null ? null : value.CurrencyTo.Name;
		}

		public DateTime StartAt { get; set; }
		public decimal Rate { get; set; }
		public decimal Scale { get; set; }
		public ExchangeRateSources Source { get; set; }
		public string CurrencyFrom { get; set; }
		public string CurrencyTo { get; set; }

     
        public ExchangeRate Back(Context context)
        {
            var result = new ExchangeRate
				{
					StartAt = StartAt,
					Rate = Rate,
					Scale = Scale,
					Source = Source,
					CurrencyFrom = CurrencyFrom == null ? null : context.CurrencyByKey[CurrencyFrom],
					CurrencyTo = CurrencyTo == null ? null : context.CurrencyByKey[CurrencyTo],
				};
			return result;
        }
	}
    [CompilerGenerated]
	public sealed class LedgerData
	{
		public LedgerData()
		{
		}
		public LedgerData(Ledger value)
		{
			Currencies = value.Currencies.Select(x => new CurrencyData(x)).ToList();
			Accounts = value.Accounts.Select(x => new AccountData(x)).ToList();
			Batches = value.Batches;
		}

		public List<CurrencyData> Currencies { get; set; }
		public List<AccountData> Accounts { get; set; }
		public List<BatchDescriptor> Batches { get; set; }

     
        public Ledger Back(Context context)
        {
            var result = new Ledger
				{
					Currencies = new List<Currency>(Currencies.Select(x => x.Back(context))),
					Accounts = new List<Account>(Accounts.Select(x => x.Back(context))),
					Batches = Batches,
				};
			return result;
        }
	}
    [CompilerGenerated]
	public sealed class MoneyData
	{
		public MoneyData()
		{
		}
		public MoneyData(Money value)
		{
			Amount = value.Amount;
			Currency = value.Currency == null ? null : value.Currency.Name;
		}

		public decimal Amount { get; set; }
		public string Currency { get; set; }

     
        public Money Back(Context context)
        {
            var result = new Money
				{
					Amount = Amount,
					Currency = Currency == null ? null : context.CurrencyByKey[Currency],
				};
			return result;
        }
	}
    [CompilerGenerated]
	public sealed class RecordData
	{
		public RecordData()
		{
		}
		public RecordData(Record value)
		{
			Comment = value.Comment;
			Credit = value.Credit == null ? null : new EndPointData(value.Credit);
			Debit = value.Debit == null ? null : new EndPointData(value.Debit);
		}

		public string Comment { get; set; }
		public EndPointData Credit { get; set; }
		public EndPointData Debit { get; set; }

     
        public Record Back(Context context)
        {
            var result = new Record
				{
					Comment = Comment,
					Credit = Credit.Back(context),
					Debit = Debit.Back(context),
				};
			return result;
        }
	}
    [CompilerGenerated]
	public sealed class TotalData
	{
		public TotalData()
		{
		}
		public TotalData(Total value)
		{
			Debit = value.Debit.Select(x => new MoneyData(x)).ToList();
			Credit = value.Credit.Select(x => new MoneyData(x)).ToList();
		}

		public List<MoneyData> Debit { get; set; }
		public List<MoneyData> Credit { get; set; }

     
        public Total Back(Context context)
        {
            var result = new Total
				{
					Debit = new MoneyBag(Debit.Select(x => x.Back(context))),
					Credit = new MoneyBag(Credit.Select(x => x.Back(context))),
				};
			return result;
        }
	}
    [CompilerGenerated]
	public sealed class TransactionData
	{
		public TransactionData()
		{
		}
		public TransactionData(Transaction value)
		{
			Comment = value.Comment;
			Timestamp = value.Timestamp;
			Entries = value.Entries.Select(x => new RecordData(x)).ToList();
			TransactionType = value.TransactionType;
		}

		public string Comment { get; set; }
		public DateTime Timestamp { get; set; }
		public List<RecordData> Entries { get; set; }
		public TransactionType TransactionType { get; set; }

     
        public Transaction Back(Context context)
        {
            var result = new Transaction
				{
					Comment = Comment,
					Timestamp = Timestamp,
					Entries = new List<Record>(Entries.Select(x => x.Back(context))),
					TransactionType = TransactionType,
				};
			return result;
        }
	}
}