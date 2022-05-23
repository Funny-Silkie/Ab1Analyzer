using System;

namespace Ab1Analyzer
{
    /// <summary>
    /// 整数の分数を表します。
    /// </summary>
    [Serializable]
    public readonly struct IntegerFraction : IEquatable<IntegerFraction>, IComparable, IComparable<IntegerFraction>
    {
        /// <summary>
        /// 非数（0/0）を取得します。
        /// </summary>
        public static IntegerFraction NaN { get; } = new IntegerFraction(0, 0);

        /// <summary>
        /// 負の無限大を表す値を取得します。
        /// </summary>
        public static IntegerFraction NegativeInfinity { get; } = new IntegerFraction(-1, 0);

        /// <summary>
        /// 正の無限大を表す値を取得します。
        /// </summary>
        public static IntegerFraction PositiveInfinity { get; } = new IntegerFraction(1, 0);

        /// <summary>
        /// ゼロを表す値を取得します。
        /// </summary>
        public static IntegerFraction Zero { get; } = new IntegerFraction(0, 1);

        /// <summary>
        /// 分子
        /// </summary>
        public readonly int numerator;

        /// <summary>
        /// 分母
        /// </summary>
        public readonly int denominator;

        /// <summary>
        /// 剰余を求めます。
        /// </summary>
        /// <returns>剰余</returns>
        public readonly int Mod => numerator % denominator;

        /// <summary>
        /// 逆数を取得します。
        /// </summary>
        public IntegerFraction Reciprocal => new IntegerFraction(denominator, numerator);

        /// <summary>
        /// <see cref="IntegerFraction"/>の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="value">整数値</param>
        public IntegerFraction(int value)
        {
            numerator = value;
            denominator = 1;
        }

        /// <summary>
        /// <see cref="IntegerFraction"/>の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="numerator">分子</param>
        /// <param name="denominator">分母</param>
        public IntegerFraction(int numerator, int denominator)
        {
            if (denominator == 0) numerator = Math.Sign(numerator);
            else if (numerator == 0) denominator = 1;
            else if (denominator < 0)
            {
                denominator *= -1;
                numerator *= -1;
            }
            int gcd = CalcGcd(numerator, denominator);
            this.numerator = numerator / gcd;
            this.denominator = denominator / gcd;
        }

        /// <summary>
        /// 最大公約数を計算します。
        /// </summary>
        /// <param name="left">最大公約数を求める値</param>
        /// <param name="right">最大公約数を求める値</param>
        /// <returns><paramref name="left"/>と<paramref name="right"/>の最大公約数</returns>
        private static int CalcGcd(int left, int right)
        {
            if (left < right) return CalcGcd(right, left);
            while (right != 0)
            {
                int remainder = left % right;
                left = right;
                right = remainder;
            }
            return left;
        }

        /// <summary>
        /// 指定した数が非数であるかどうかを検証します。
        /// </summary>
        /// <param name="value">検証する値</param>
        /// <returns><paramref name="value"/>が非数であったらtrue，それ以外でfalse</returns>
        public static bool IsNaN(IntegerFraction value) => value.denominator == 0 && value.numerator == 0;

        /// <summary>
        /// 値が有限の数を表しているかどうかを検証します。
        /// </summary>
        /// <param name="value">検証する値</param>
        /// <returns><paramref name="value"/>が有限の数を表していたらtrue，それ以外でfalse</returns>
        public static bool IsFinite(IntegerFraction value) => value.denominator != 0;

        /// <summary>
        /// 値が負の無限大を表すかどうかを検証します。
        /// </summary>
        /// <param name="value">検証する値</param>
        /// <returns><paramref name="value"/>が負の無限大であったらtrue，それ以外でfalse</returns>
        public static bool IsNegativeInfinity(IntegerFraction value) => value.denominator == 0 && value.numerator == -1;

        /// <summary>
        /// 値が正の無限大を表すかどうかを検証します。
        /// </summary>
        /// <param name="value">検証する値</param>
        /// <returns><paramref name="value"/>が正の無限大であったらtrue，それ以外でfalse</returns>
        public static bool IsPositiveInfinity(IntegerFraction value) => value.denominator == 0 && value.numerator == 1;

        /// <summary>
        /// 通分を行います。
        /// </summary>
        /// <param name="left">通分する分数</param>
        /// <param name="right">通分する分数</param>
        /// <returns>通分後の値</returns>
        public static (IntegerFraction left, IntegerFraction right) Reduction(IntegerFraction left, IntegerFraction right)
        {
            if (left.denominator == right.denominator) return (left, right);
            if (left.denominator % right.denominator == 0)
            {
                int div = left.denominator / right.denominator;
                right *= div;
                return (left, right);
            }
            if (right.denominator % right.denominator == 0)
            {
                int div = right.denominator / left.denominator;
                left *= div;
                return (left, right);
            }
            int denominator = Math.Min(left.denominator, right.denominator) * CalcGcd(left.denominator, right.denominator);
            left = new IntegerFraction(left.numerator * (denominator / left.denominator), denominator);
            right = new IntegerFraction(right.numerator * (denominator / right.denominator), denominator);
            return (left, right);
        }

        /// <inheritdoc/>
        public readonly int CompareTo(IntegerFraction obj) => ToDouble().CompareTo(obj.ToDouble());

        readonly int IComparable.CompareTo(object obj)
        {
            return obj switch
            {
                IntegerFraction other => CompareTo(other),
                sbyte v => ToDouble().CompareTo(v),
                byte v => ToDouble().CompareTo(v),
                short v => ToDouble().CompareTo(v),
                ushort v => ToDouble().CompareTo(v),
                int v => ToDouble().CompareTo(v),
                uint v => ToDouble().CompareTo(v),
                long v => ToDouble().CompareTo(v),
                ulong v => ToDouble().CompareTo(v),
                float v => ToDouble().CompareTo(v),
                double v => ToDouble().CompareTo(v),
                _ => throw new ArgumentException("無効な型が渡されました", nameof(obj)),
            };
        }

        /// <inheritdoc/>
        public readonly bool Equals(IntegerFraction other) => numerator == other.numerator && denominator == other.denominator;

        /// <inheritdoc/>
        public override readonly bool Equals(object obj) => obj is IntegerFraction fraction && Equals(fraction);

        /// <inheritdoc/>
        public override readonly int GetHashCode() => HashCode.Combine(numerator, denominator);

        /// <summary>
        /// 計算を行い小数を取得します。
        /// </summary>
        /// <returns>計算後の値</returns>
        public readonly double ToDouble()
        {
            if (denominator == 0)
                switch (numerator)
                {
                    case -1: return double.NegativeInfinity;
                    case 0: return double.NaN;
                    case 1: return double.PositiveInfinity;
                    default: throw new InvalidOperationException();
                }
            if (numerator == 0) return 0;
            if (numerator == denominator) return 1;
            if (numerator * -1 == denominator) return -1;
            return numerator / denominator;
        }

        /// <inheritdoc/>
        public override readonly string ToString() => $"{numerator} / {denominator}";

        public static bool operator ==(IntegerFraction left, IntegerFraction right) => left.Equals(right);

        public static bool operator !=(IntegerFraction left, IntegerFraction right) => !(left == right);

        public static bool operator <(IntegerFraction left, IntegerFraction right) => left.CompareTo(right) < 0;

        public static bool operator <=(IntegerFraction left, IntegerFraction right) => left.CompareTo(right) <= 0;

        public static bool operator >(IntegerFraction left, IntegerFraction right) => left.CompareTo(right) > 0;

        public static bool operator >=(IntegerFraction left, IntegerFraction right) => left.CompareTo(right) >= 0;

        public static IntegerFraction operator +(IntegerFraction value) => value;

        public static IntegerFraction operator +(IntegerFraction left, int right) => left + new IntegerFraction(right);

        public static IntegerFraction operator +(int left, IntegerFraction right) => right + left;

        public static IntegerFraction operator +(IntegerFraction left, IntegerFraction right)
        {
            (left, right) = Reduction(left, right);
            return new IntegerFraction(left.numerator + right.numerator, left.numerator);
        }

        public static IntegerFraction operator -(IntegerFraction value) => new IntegerFraction(-value.numerator, value.denominator);

        public static IntegerFraction operator -(IntegerFraction left, IntegerFraction right) => left + (-right);

        public static IntegerFraction operator -(IntegerFraction left, int right) => left + (-right);

        public static IntegerFraction operator -(int left, IntegerFraction right) => left + (-right);

        public static IntegerFraction operator *(IntegerFraction left, IntegerFraction right) => new IntegerFraction(left.numerator * right.numerator, left.denominator * right.denominator);

        public static IntegerFraction operator *(IntegerFraction left, int right) => new IntegerFraction(left.numerator * right, left.denominator * right);

        public static IntegerFraction operator *(int left, IntegerFraction right) => right * left;

        public static IntegerFraction operator /(IntegerFraction left, IntegerFraction right) => left * right.Reciprocal;

        public static IntegerFraction operator /(IntegerFraction left, int right) => left * new IntegerFraction(1, right);

        public static IntegerFraction operator /(int left, IntegerFraction right) => new IntegerFraction(left, 1) * right.Reciprocal;

        public static explicit operator double(IntegerFraction value) => value.ToDouble();

        public static implicit operator IntegerFraction(int value) => new IntegerFraction(value);
    }
}
