using System;
using System.Collections.Generic;
using System.Linq;

using NewModel.Shared.Annotations;
using NewModel.Shared.Attributes;

namespace NewModel.Accounting.Core
{
    [DataOnlyObject]
    public sealed class Transaction 
    {
        public string Comment { get; set; }
        public DateTime Timestamp { get; set; }
        [Compound]
        public List<Record> Entries { get; set; }

        public TransactionType TransactionType { get; set; }
        
        public Transaction() {}
        public Transaction(DateTime timestamp, string comment, EndPoint credit, EndPoint debit)
        {
            Timestamp = timestamp;
            Entries = new List<Record>{ new Record(comment, credit, debit)};
            Comment = comment;
        }
        public Transaction(DateTime timestamp, string comment, Money money, Account credit, Account debit)
        {
            Timestamp = timestamp;
            Entries = new List<Record>{new Record(comment, money, credit, debit)};
            Comment = comment;
        }
        public Transaction(DateTime timestamp, string comment, params Record[] records)
        {
            Timestamp = timestamp;
            Entries = records.ToList();
            Comment = comment;
        }

        public override string ToString()
		{
			return string.Join(Environment.NewLine, Entries);
		}
    }
}