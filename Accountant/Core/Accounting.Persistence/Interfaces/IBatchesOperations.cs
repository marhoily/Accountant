using System;
using System.Collections.Generic;

using NewModel.Accounting.Calculation;

namespace NewModel.Accounting.Persistence
{
    public interface IBatchesOperations {
        IEnumerable<Batch> GetBatches(Interval interval);
        IEnumerable<Batch> GetBatchesChain(DateTime moment);
        Batch GetBatch(DateTime moment);
    }
}