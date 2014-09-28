using System.Collections.Generic;

using NewModel.Accounting.Core;

namespace NewModel.Accounting.Persistence
{
    public interface ITransactionRegistrant {
        void AddTransaction(IEnumerable<Batch> batches, Transaction transaction);
    }
}