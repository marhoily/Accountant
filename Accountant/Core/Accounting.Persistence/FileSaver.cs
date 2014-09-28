
using NewModel.Shared.FileSystem;

namespace NewModel.Accounting.Persistence
{
    public sealed class FileSaver
    {
        readonly IDirectory mDirectory;
        readonly IDataWriter mWriter;

        public FileSaver(IDirectory directory, IDataWriter writer)
        {
            mDirectory = directory;
            mWriter = writer;
        }

        public void SaveLedger(Ledger ledger)
        {
            var file = mDirectory.GetFile("ledger" + mWriter.FileExtension);
            file.WriteAllText(mWriter.Write(new LedgerData(ledger)));
        }

        public void SaveBatch(Batch batch)
        {
            var file = mDirectory.GetFile("batch" + mWriter.FileExtension);
            file.WriteAllText(mWriter.Write(new BatchData(batch)));
        }
    }

    public interface IDataWriter
    {
        string Write<T>(T data);
        string FileExtension { get; }
    }
}