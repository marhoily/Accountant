using System;
using System.Collections.Generic;
using System.Linq;

using NewModel.Accounting.Calculation;

namespace NewModel.Accounting.Persistence
{
    public sealed class BatchesOperations : IBatchesOperations
    {
        readonly Ledger mLedger;
        readonly IBatchLoader mLoader;

        public BatchesOperations(Ledger ledger, IBatchLoader loader)
        {
            mLedger = ledger;
            mLoader = loader;
        }

        public IEnumerable<Batch> GetBatches(Interval interval)
        {
            return from descriptor in mLedger.Batches
                where descriptor.ReferenceInterval.Intersect(interval) != null
                select mLoader.Load(mLedger, descriptor);
        }

        public IEnumerable<Batch> GetBatchesChain(DateTime moment)
        {
            /* mLedger.Batches collection is not empty and the first batch
             * always starts at DateTime.MinValue, so this method always
             * returns meaningful batches chain
             */
            return from descriptor in mLedger.Batches
                where descriptor.ReferenceInterval >= moment
                orderby descriptor.ReferenceInterval
                select mLoader.Load(mLedger, descriptor);
        }

        public Batch GetBatch(DateTime moment)
        {
            return mLoader.Load(mLedger, 
                mLedger.Batches.Single(b => b.ReferenceInterval.Contains(moment)));
        }
    }
}