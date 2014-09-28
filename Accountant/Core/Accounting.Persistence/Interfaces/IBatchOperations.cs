using System.Collections.Generic;

using NewModel.Accounting.Calculation;
using NewModel.Accounting.Core;

namespace NewModel.Accounting.Persistence
{
    public interface IBatchOperations {
        void SetIngressBalance(Batch batch, List<AccountTotal> ingressBalance);
        void AddTransaction(Batch batch, Transaction transaction);
    }
}