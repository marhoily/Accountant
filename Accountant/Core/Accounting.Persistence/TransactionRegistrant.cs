using System.Collections.Generic;

using NewModel.Accounting.Core;
using NewModel.Shared.Utils;

namespace NewModel.Accounting.Persistence
{
    public sealed class TransactionRegistrant : ITransactionRegistrant
    {
        readonly ITotalsCalculator mTotalsCalculator;
        readonly IApplicator mApplicator;
        readonly IBatchOperations mBatchOperations;

        public TransactionRegistrant(ITotalsCalculator totalsCalculator, 
            IApplicator applicator, IBatchOperations batchOperations)
        {
            mTotalsCalculator = totalsCalculator;
            mApplicator = applicator;
            mBatchOperations = batchOperations;
        }

        public void AddTransaction(IEnumerable<Batch> batches, Transaction transaction)
        {
            mApplicator.Apply(batches,
                first => mBatchOperations.AddTransaction(first, transaction),
                (next, previous) => mBatchOperations.SetIngressBalance(
                    next, mTotalsCalculator.GetBalance(previous)));
        }

    }
}