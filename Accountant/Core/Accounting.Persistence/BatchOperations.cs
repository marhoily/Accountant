using System.Collections.Generic;

using NewModel.Accounting.Calculation;
using NewModel.Accounting.Core;

namespace NewModel.Accounting.Persistence
{
    public sealed class BatchOperations : IBatchOperations
    {
        public void SetIngressBalance(Batch batch, List<AccountTotal> ingressBalance)
        {
            batch.IngressBalance = ingressBalance;
            batch.IsModified = true;
        }

        public void AddTransaction(Batch batch, Transaction transaction)
        {
            batch.Transactions.Add(transaction);
            batch.IsModified = true;
        }        
    }
}