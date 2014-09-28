using System;

namespace NewModel.Accounting.Core
{
    [Flags]
    public enum AccountOptions
    {
        None,
        Hidden = 0x1,
        PrimaryCurrencyIsExclusive = 0x2,
    }
}