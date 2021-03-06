﻿using System;
using System.Collections.Generic;
using System.Linq;

using NewModel.Accounting.Core;

namespace NewModel.Accounting.Calculation
{
    public sealed class MoneyBag : List<Money>
    {
        public MoneyBag() {}

        public MoneyBag(IEnumerable<Money> money)
            : base(money)
        {
        }
        public MoneyBag(params Money[] money)
            : base(money)
        {
        }

        public decimal this[Currency index]
        {
            get
            {
                var m = this.FirstOrDefault(c => c.Currency == index);
                return m == null ? 0 : m.Amount;
            }
        }

        public bool IsZero { get { return Count == 0; } }
        public static MoneyBag operator -(MoneyBag x)
        {
            return new MoneyBag(x.Select(m => new Money(m.Currency, -m.Amount)));
        }
        public static MoneyBag operator -(MoneyBag x, MoneyBag y)
        {
            return x + (-y);
        }
        public static MoneyBag operator +(MoneyBag x, MoneyBag y)
        {
            return new MoneyBag(x
                .Concat(y)
                .GroupBy(m => m.Currency)
                .Select(Sum)
                .Where(m => m.Amount != 0));
        }
        static Money Sum(IEnumerable<Money> source)
        {
            var list = source.ToList();
            if (list.Count == 0) throw new Exception("Empty sequence");
            if (list.Count == 1) return list[0];
            if (list.Select(i => i.Currency).Distinct().Count() != 1)
                throw new Exception("Cannot sum different currencies");
            return new Money(list[0].Currency, list.Sum(i => i.Amount));
        }

        public static MoneyBag operator +(MoneyBag x, Money y)
        {
            return x + new MoneyBag(y);
        }

        public override string ToString()
        {
            return string.Join(", ", this);
        }
    }
}
