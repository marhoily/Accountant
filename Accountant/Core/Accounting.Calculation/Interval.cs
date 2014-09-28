using System;

using NewModel.Shared.Annotations;
using NewModel.Shared.Annotations.ReSharper;

namespace NewModel.Accounting.Calculation
{
    public sealed class Interval : IEquatable<Interval>
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        [UsedImplicitly("by deserializer")] public Interval() {}

        public Interval(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }
        
        #region ' Equality '

        public bool Equals(Interval other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Start.Equals(other.Start) && End.Equals(other.End);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Interval && Equals((Interval) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Start.GetHashCode()*397) ^ End.GetHashCode();
            }
        }

        public static bool operator ==(Interval left, Interval right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Interval left, Interval right)
        {
            return !Equals(left, right);
        }

        #endregion

        public Interval Intersect(Interval interval)
        {
            var min = Max(interval.Start, Start);
            var max = Min(interval.End, End);
            return min >= max ? null : new Interval(min, max);
        }

        #region ' Helpers '
        static DateTime Max(DateTime x, DateTime y)
        {
            return x > y ? x : y;
        }

        static DateTime Min(DateTime x, DateTime y)
        {
            return x < y ? x : y;
        }
        #endregion

        public bool Contains(DateTime timestamp)
        {
            return timestamp >= Start && timestamp < End;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}", Start, End);
        }

        public static bool operator >=(Interval interval, DateTime moment)
        {
            return interval.Start >= moment;
        }

        public static bool operator <=(Interval interval, DateTime moment)
        {
            return interval.End < moment;
        }
    }
}