using System.Collections.Generic;
using System.Linq;

using NewModel.Accounting.Core;
using NewModel.Shared.Annotations;

namespace NewModel.Accounting.Calculation
{
//    [DataOnlyObject]
//    public sealed class TransactionsBalance
//    {
//        public Total Ingress { get; set; }
//        public Total Amount { get; set; }
//        public Total Egress { get; set; }
//        public IEnumerable<Transaction> Transactions { get; set; }
//    }
    [DataOnlyObject]
    public sealed class TransactionsTotal
    {
        public Total Total { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}