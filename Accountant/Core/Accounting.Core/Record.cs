using NewModel.Shared.Annotations;
using NewModel.Shared.Attributes;

namespace NewModel.Accounting.Core
{
    [DataOnlyObject]
    public sealed class Record
    {
        public Record() {}

        public Record(EndPoint credit, EndPoint debit)
        {
            Credit = credit;
            Debit = debit;
        }
        public Record(Money money, Account credit, Account debit)
        {
            Credit = new EndPoint(money, credit);
            Debit = new EndPoint(money, debit);
        }
        public Record(string comment, EndPoint credit, EndPoint debit)
        {
            Credit = credit;
            Debit = debit;
            Comment = comment;
        }
        public Record(string comment, Money money, Account credit, Account debit)
        {
            Credit = new EndPoint(money, credit);
            Debit = new EndPoint(money, debit);
            Comment = comment;
        }

        public string Comment { get; set; }
        [Compound]
        public EndPoint Credit { get; set; }
        [Compound]
        public EndPoint Debit { get; set; }

        public override string ToString()
        {
            return string.Format("({0}) ----> ({1})", Debit, Credit);
        }
    }
}