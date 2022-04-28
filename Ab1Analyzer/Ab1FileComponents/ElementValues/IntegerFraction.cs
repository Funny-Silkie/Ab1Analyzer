using System;

namespace Ab1Analyzer
{
    /// <summary>
    /// 整数の分数を表します。
    /// </summary>
    [Serializable]
    public struct IntegerFraction : IEquatable<IntegerFraction>
    {
        /// <summary>
        /// 分子
        /// </summary>
        public int numerator;

        /// <summary>
        /// 分母
        /// </summary>
        public int denominator;

        /// <summary>
        /// <see cref="IntegerFraction"/>の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="numerator">分子</param>
        /// <param name="denominator">分母</param>
        public IntegerFraction(int numerator, int denominator)
        {
            this.numerator = numerator;
            this.denominator = denominator;
        }

        /// <inheritdoc/>
        public readonly bool Equals(IntegerFraction other) => numerator == other.numerator && denominator == other.denominator;

        /// <inheritdoc/>
        public override readonly bool Equals(object obj) => obj is IntegerFraction fraction && Equals(fraction);

        /// <inheritdoc/>
        public override readonly int GetHashCode() => HashCode.Combine(numerator, denominator);

        public static bool operator ==(IntegerFraction left, IntegerFraction right) => left.Equals(right);

        public static bool operator !=(IntegerFraction left, IntegerFraction right) => !(left == right);
    }
}
