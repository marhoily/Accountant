using System.Collections.Generic;

using NewModel.Accounting.Calculation;
using NewModel.Accounting.Core;
using NewModel.Shared.Annotations;
using NewModel.Shared.Attributes;

namespace NewModel.Accounting.Persistence
{
    [DataOnlyObject]
    public sealed class Batch
    {
        [Compound]
        public List<AccountTotal> IngressBalance { get; set; }
        public Interval ReferenceInterval { get; set; }
        [Compound]
        public List<Transaction> Transactions { get; set; }
        [Compound]
        public List<ExchangeRate> ExchangeRates { get; set; }
        [NotMapped]
        public bool IsModified { get; set; }
    }
}