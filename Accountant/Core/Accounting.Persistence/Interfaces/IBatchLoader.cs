namespace NewModel.Accounting.Persistence
{
    public interface IBatchLoader
    {
        Batch Load(Ledger ledger, BatchDescriptor descriptor);
    }
}