using System.Collections.Generic;

using NewModel.Accounting.Calculation;

namespace NewModel.Accounting.Persistence
{
    public interface ITotalsCalculator {
        List<AccountTotal> GetBalance(Batch batch);
    }
}