using System;
using System.Collections.Generic;
using System.Linq;

using NewModel.Accounting.Core;

namespace NewModel.Accounting.Calculation
{
    public sealed class Calculator
    {
        #region ' Accounts '

        public Account Root(Account account)
        {
            while (account.Parent != null && account.Parent != Account.Root) account = account.Parent;
            return account;

        }

        public bool Is(Account a, Account b)
        {
            while (a != b && a.Parent != null) 
                a = a.Parent;
            return a == b;

        }

        #endregion

        #region ' EndPoint '

        bool AnyAccountIs(EndPoint endPoint, Account account)
        {
            return endPoint.Accounts.Any(a => Is(a, account));
        }

        #endregion

        #region ' Records '
        
        public bool Touches(Record record, Account account)
        {
            return AnyAccountIs(record.Credit, account) 
                || AnyAccountIs(record.Debit, account);
        }

        public MoneyBag Credits(IEnumerable<Record> entries, Func<Account, bool> predicate = null)
        {
            predicate = predicate ?? (_ => true);
            return entries.Where(t => t.Credit.Accounts.Any(predicate)).Sum(t => t.Credit.Amount);
        }

        public MoneyBag Debits(IEnumerable<Record> entries, Func<Account, bool> predicate = null)
        {
            predicate = predicate ?? (_ => true);
            return entries.Where(t => t.Debit.Accounts.Any(predicate)).Sum(t => t.Debit.Amount);
        }

        public MoneyBag Balance(IEnumerable<Record> entries, Func<Account, bool> predicate = null)
        {
            var buffered = entries.ToList();
            return Credits(buffered, predicate) - Debits(buffered, predicate);
        }

        #endregion

        #region ' Transactions '
        public bool Touches(Transaction transaction, Account account)
        {
            return Touches(transaction.Entries.Single(), account);
        }

        public bool IsRevised(Transaction transaction)
        {
            return transaction.Entries.All(entry =>
                entry.Credit.Accounts.All(account => !Is(account, Account.Asset) || account.LastRevisionDate >= transaction.Timestamp) &&
                entry.Debit. Accounts.All(account => !Is(account, Account.Asset) || account.LastRevisionDate >= transaction.Timestamp));
        }

        public MoneyBag Credits(IEnumerable<Transaction> transactions, Func<Account, bool> predicate = null)
        {
            return Credits(transactions.SelectMany(t => t.Entries));
        }

        public MoneyBag Debits(IEnumerable<Transaction> transactions, Func<Account, bool> predicate = null)
        {
            return Debits(transactions.SelectMany(t => t.Entries));
        }

        public MoneyBag Balance(IEnumerable<Transaction> transactions, Func<Account, bool> predicate = null)
        {
            return Balance(transactions.SelectMany(t => t.Entries));
        }

        #endregion
    }
}