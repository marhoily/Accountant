using System.Collections.Generic;

using NewModel.Accounting.Core;
using NewModel.Shared.Annotations;
using NewModel.Shared.Attributes;

namespace NewModel.Accounting.Persistence
{
    [DataOnlyObject]
    public sealed class Ledger
    {
        [Compound]
        public List<Currency> Currencies { get; set; }
        [Compound]
        public List<Account> Accounts { get; set; }
        public List<BatchDescriptor> Batches { get; set; }
        [NotMapped]
        public List<Batch> LoadedBatches { get; set; }
    }
}