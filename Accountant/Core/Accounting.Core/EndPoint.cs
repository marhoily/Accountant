using System.Collections.Generic;
using System.Linq;

using NewModel.Shared.Annotations;
using NewModel.Shared.Attributes;

namespace NewModel.Accounting.Core
{
    [DataOnlyObject]
    public sealed class EndPoint
    {
        [Compound]
        public Money Amount { get; set; }

        /// <summary>
        /// a) you don't have two primary accounts 
        ///    on the same side of one transaction
        /// b) you don't have transaction with only 
        ///    secondary accounts on a side of one transaction
        /// </summary>
        [Navigation]
        public List<Account> Accounts { get; set; }

        public EndPoint() { }

        public EndPoint(Money amount, params Account[] accounts)
        {
            Amount = amount;
            Accounts = accounts.ToList();
        }
    }
}