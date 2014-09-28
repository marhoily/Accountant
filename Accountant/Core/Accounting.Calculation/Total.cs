using System;

using NewModel.Accounting.Core;
using NewModel.Shared.Annotations;
using NewModel.Shared.Attributes;

namespace NewModel.Accounting.Calculation
{
    [DataOnlyObject]
    public sealed class Total
    {
        [Compound]
        public MoneyBag Debit { get; set; }
        [Compound]
        public MoneyBag Credit { get; set; }

        [NotMapped]
        public MoneyBag Value
        {
            get { return Debit - Credit; }
        }

        public static Total operator +(Total a, Total b)
        {
            throw new NotImplementedException();
        }

        public static Total operator -(Total a, Total b)
        {
            throw new NotImplementedException();
        }
    }
    [DataOnlyObject]
    public sealed class AccountTotal
    {
        [Compound]
        public Total Total { get; set; }
        [Navigation]
        public Account Account { get; set; }
    }
}