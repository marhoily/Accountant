namespace NewModel.Accounting.Core
{
    public enum AccountModifiers
    {
        None,
        CannotBeUsedInTransactionsDirectly, 
        CanHaveChildAccounts, 
    }
}