using System;
using System.Collections.Generic;
using System.Linq;

using NewModel.Accounting.Calculation;
using NewModel.Accounting.Core;

namespace NewModel.Accounting.Persistence
{
    public sealed class LedgerOperations2
    {
        readonly IBatchesOperations mBatchesOperations;
        readonly ITransactionRegistrant mTransactionRegistrant;

        public LedgerOperations2(IBatchesOperations batchesOperations, ITransactionRegistrant transactionRegistrant)
        {
            mBatchesOperations = batchesOperations;
            mTransactionRegistrant = transactionRegistrant;
        }


        public void RegisterTransaction(Transaction transaction)
        {
            var chain = mBatchesOperations.GetBatchesChain(transaction.Timestamp);
            mTransactionRegistrant.AddTransaction(chain, transaction);
        }
    }
    public sealed class LedgerOperations3
    {
        readonly IBatchesOperations mBatchesOperations;
        readonly IBalanceCalculator mBalanceCalculator;
        readonly ITotalExtractor mTotalExtractor;

        public LedgerOperations3(IBatchesOperations batchesOperations,
            IBalanceCalculator balanceCalculator, ITotalExtractor totalExtractor)
        {
            mBatchesOperations = batchesOperations;
            mBalanceCalculator = balanceCalculator;
            mTotalExtractor = totalExtractor;
        }

        public TransactionsTotal GetTransactionsTotalAt(DateTime moment, IEnumerable<Account> accounts)
        {
            var batch = mBatchesOperations.GetBatch(moment);
            var transactions = batch.Transactions.Where(t => t.Timestamp < moment).ToList();
            var ingressTotal = mTotalExtractor.Extract(batch.IngressBalance, accounts);
            return new TransactionsTotal
            {
                Transactions = transactions,
                Total = ingressTotal + mBalanceCalculator.Calculate(transactions),
            };
        }
    }

    public sealed class LedgerOperations
    {
        readonly IBatchesOperations mBatchesOperations;

        public LedgerOperations(IBatchesOperations batchesOperations)
        {
            mBatchesOperations = batchesOperations;
        }

        public IEnumerable<Transaction> GetTransactions(Interval interval)
        {
            return from batch in mBatchesOperations.GetBatches(interval)
                   from transaction in batch.Transactions
                   where interval.Contains(transaction.Timestamp)
                   select transaction;
        }

        public IEnumerable<ExchangeRate> GetExchangeRates(Interval interval)
        {
            return from batch in mBatchesOperations.GetBatches(interval)
                   from exchangeRate in batch.ExchangeRates
                   where interval.Contains(exchangeRate.StartAt)
                   select exchangeRate;
        }

    }
    public interface ITotalExtractor {
        Total Extract(List<AccountTotal> ingressBalance, IEnumerable<Account> accounts);
    }
    public class LedgerCalculator
    {
        readonly LedgerOperations3 mLedgerOperations;

        public LedgerCalculator(LedgerOperations3 ledgerOperations)
        {
            mLedgerOperations = ledgerOperations;
        }

        public IntervalBalance GetBalance(Interval interval, IEnumerable<Account> accounts)
        {
            var bufferedAccounts = accounts.ToList();
            var ingress = mLedgerOperations.GetTransactionsTotalAt(interval.Start, bufferedAccounts);
            var egress = mLedgerOperations.GetTransactionsTotalAt(interval.End, bufferedAccounts);
            return new IntervalBalance
            {
                Interval = interval,
                Ingress = ingress.Total,
                Egress = egress.Total,
                Amount = egress.Total - ingress.Total,
                Transactions = egress.Transactions,
                Accounts = bufferedAccounts,
            };
        }        
    }
}