using System.Collections.Generic;

using NewModel.Accounting.Core;
using NewModel.Shared.Annotations;

namespace NewModel.Accounting.Calculation
{

    [DataOnlyObject]
    public sealed class IntervalBalance
    {
        public Interval Interval { get; set; }
        public Total Ingress { get; set; }
        public Total Amount { get; set; }
        public Total Egress { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
        public IEnumerable<Account> Accounts { get; set; }
    }
}