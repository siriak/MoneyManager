using Newtonsoft.Json;
using System;

namespace Core
{
    public struct Date : IComparable<Date>, IEquatable<Date>
    {
        private readonly DateTime _dt;

        public static readonly Date MaxValue = new Date(DateTime.MaxValue);
        public static readonly Date MinValue = new Date(DateTime.MinValue);

        [JsonConstructor]
        public Date(int year, int month, int day) => _dt = new DateTime(year, month, day);

        public Date(DateTime dateTime) : this(dateTime.Year, dateTime.Month, dateTime.Day)
        {
        }

        public static TimeSpan operator -(Date d1, Date d2) => d1._dt - d2._dt;

        public static Date operator -(Date d, TimeSpan t) => new Date(d._dt - t);

        public static bool operator !=(Date d1, Date d2) => d1._dt != d2._dt;

        public static Date operator +(Date d, TimeSpan t) => new Date(d._dt + t);

        public static bool operator <(Date d1, Date d2) => d1._dt < d2._dt;

        public static bool operator <=(Date d1, Date d2) => d1._dt <= d2._dt;

        public static bool operator ==(Date d1, Date d2) => d1._dt == d2._dt;

        public static bool operator >(Date d1, Date d2) => d1._dt > d2._dt;

        public static bool operator >=(Date d1, Date d2) => d1._dt >= d2._dt;

        public static implicit operator DateTime(Date d) => d._dt;

        public static explicit operator Date(DateTime d) => new Date(d);

        public static Date Today => new Date(DateTime.Today);

        public int Day => _dt.Day;

        public int Month => _dt.Month;

        public int Year => _dt.Year;

        public Date AddDays(int value) => new Date(_dt.AddDays(value));

        public Date AddMonths(int value) => new Date(_dt.AddMonths(value));

        public Date AddYears(int value) => new Date(_dt.AddYears(value));

        public int CompareTo(Date other) => _dt.CompareTo(other._dt);

        public bool Equals(Date other) => _dt.Equals(other._dt);

        public override bool Equals(object obj) => obj is Date d && _dt.Equals(d._dt);

        public override int GetHashCode() => _dt.GetHashCode();

        public static Date Parse(string s) => new Date(DateTime.Parse(s));

        public override string ToString() => _dt.ToShortDateString();

        public string ToString(string format) => _dt.ToString(format);

        public static bool TryParse(string s, out Date result)
        {
            var success = DateTime.TryParse(s, out var d);
            result = new Date(d);
            return success;
        }
    }
}
