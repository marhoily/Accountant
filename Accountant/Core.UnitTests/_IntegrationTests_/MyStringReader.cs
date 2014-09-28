using System;

namespace NewModel.UnitTests._IntegrationTests_
{
    public sealed class MyStringReader
    {
        readonly string mString;
        public int Position { get; set; }
        public int TailLength { get; set; }
        public MyStringReader(string value)
        {
            mString = value;
        }

        public string ReadToTheEnd(int variableLength)
        {
            if (Position + variableLength > mString.Length - TailLength) return null;
            var substring = mString.Substring(Position, mString.Length - Position - TailLength);
            Position += substring.Length;
            return substring;
        }
        public string ReadVariable(int variableLength, string terminator)
        {
            if (Position + variableLength + terminator.Length > mString.Length - TailLength) return null;
            var first = mString.IndexOf(terminator, Position + variableLength, 
                mString.Length - Position - TailLength - variableLength, StringComparison.Ordinal);
            if (first == -1) return null;
            if (first - Position < variableLength) return null;
            var next = mString.IndexOf(terminator, first + 1, 
                mString.Length-Position-TailLength-first-1, StringComparison.Ordinal);
            if (next != -1) return null;
            var substring = mString.Substring(Position, first - Position);
            Position += substring.Length;
            return substring;
        }
        public bool ReadConstant(string constant)
        {
            if (string.CompareOrdinal(mString, Position, constant, 0, constant.Length) != 0)
                return false;
            Position += constant.Length;
            return true;
        }
    }
}