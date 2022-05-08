using System;

namespace Ab1Analyzer
{
    /// <summary>
    /// 日付を表します。
    /// </summary>
    [Serializable]
    public struct Date : IEquatable<Date>
    {
        /// <summary>
        /// 4桁の年
        /// </summary>
        public short year;

        /// <summary>
        /// 月(1-12)
        /// </summary>
        public byte month;

        /// <summary>
        /// 日(1-31)
        /// </summary>
        public byte day;

        /// <summary>
        /// <see cref="Date"/>の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="day">日</param>
        public Date(short year, byte month, byte day)
        {
            this.year = year;
            this.month = month;
            this.day = day;
        }

        /// <inheritdoc/>
        public readonly bool Equals(Date other) => year == other.year && month == other.month && day == other.day;

        /// <inheritdoc/>
        public override readonly bool Equals(object obj) => obj is Date date && Equals(date);

        /// <inheritdoc/>
        public override readonly int GetHashCode() => HashCode.Combine(year, month, day);

        /// <inheritdoc/>
        public override readonly string ToString() => string.Format("{0:00}/{1:00}/{2:00}", year, month, day);

        public static bool operator ==(Date left, Date right) => left.Equals(right);

        public static bool operator !=(Date left, Date right) => !(left == right);
    }
}
