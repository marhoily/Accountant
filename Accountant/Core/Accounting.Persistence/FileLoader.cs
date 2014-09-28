using System;
using System.IO;
using NewModel.Shared.FileSystem;

namespace NewModel.Accounting.Persistence
{
    public sealed class FileLoader
    {
        readonly IDirectory mDirectory;
        readonly IDataReader mReader;

        public FileLoader(IDirectory directory, IDataReader reader)
        {
            mDirectory = directory;
            mReader = reader;
        }

        public Ledger LoadLedger()
        {
            var file = mDirectory.GetFile("ledger.json");
            if (!file.Exists) throw new Exception("??");
            using (var textReader = file.OpenText())
                return mReader.Read<LedgerData>(textReader).Back(new Context());
        }

        public Batch LoadBatch()
        {
            var file = mDirectory.GetFile("ledger.json");
            if (!file.Exists) throw new Exception("??");
            using (var textReader = file.OpenText())
                return mReader.Read<BatchData>(textReader).Back(new Context());
        }
    }

    public interface IDataReader
    {
        T Read<T>(TextReader textReader);
    }
}