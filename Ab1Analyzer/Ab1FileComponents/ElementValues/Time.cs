using System;

namespace Ab1Analyzer
{
    /// <summary>
    /// 一日の時間を表します。
    /// </summary>
    [Serializable]
    public struct Time : IEquatable<Time>
    {
        /// <summary>
        /// 時間(0-23)
        /// </summary>
        public byte hour;

        /// <summary>
        /// 分(0-59)
        /// </summary>
        public byte minute;

        /// <summary>
        /// 秒(0-59)
        /// </summary>
        public byte second;

        /// <summary>
        /// 1/100秒 (0-99)
        /// </summary>
        public byte hsecond;

        /// <summary>
        /// <see cref="Time"/>の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="hour">時間</param>
        /// <param name="minute">分</param>
        /// <param name="second">秒</param>
        /// <param name="hsecond">1/100秒</param>
        public Time(byte hour, byte minute, byte second, byte hsecond)
        {
            this.hour = hour;
            this.minute = minute;
            this.second = second;
            this.hsecond = hsecond;
        }

        /// <inheritdoc/>
        public readonly bool Equals(Time other) => hour == other.hour && minute == other.minute && second == other.second && hsecond == other.hsecond;

        /// <inheritdoc/>
        public override readonly bool Equals(object obj) => obj is Time time && Equals(time);

        /// <inheritdoc/>
        public override readonly int GetHashCode() => HashCode.Combine(hour, minute, second, hsecond);

        /// <inheritdoc/>
        public override readonly string ToString() => $"{hour}:{minute}:{second}:{hsecond}";

        public static bool operator ==(Time left, Time right) => left.Equals(right);

        public static bool operator !=(Time left, Time right) => !(left == right);
    }
}
