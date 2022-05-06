using System;
using System.Collections.Generic;

namespace Ab1Analyzer
{
    /// <summary>
    /// DNAの塩基を表します。
    /// </summary>
    [Serializable]
    public readonly struct DNABase : IEquatable<DNABase>, IComparable, IComparable<DNABase>
    {
        private const byte BIT_A = 0b0001;
        private const byte BIT_T = 0b0010;
        private const byte BIT_G = 0b0100;
        private const byte BIT_C = 0b1000;

        private static readonly char[] Chars = new[]
        {
            'A', 'T', 'G', 'C',
            'W', 'R', 'M', 'K', 'Y', 'S',
            'D', 'H', 'V', 'B',
            'N',
        };

        /// <summary>
        /// 塩基Aを表すインスタンスを取得します。
        /// </summary>
        public static DNABase A { get; } = new DNABase('A', BIT_A);

        /// <summary>
        /// 塩基Tを表すインスタンスを取得します。
        /// </summary>
        public static DNABase T { get; } = new DNABase('T', BIT_T);

        /// <summary>
        /// 塩基Gを表すインスタンスを取得します。
        /// </summary>
        public static DNABase G { get; } = new DNABase('G', BIT_G);

        /// <summary>
        /// 塩基Cを表すインスタンスを取得します。
        /// </summary>
        public static DNABase C { get; } = new DNABase('C', BIT_C);

        /// <summary>
        /// 塩基Wを表すインスタンスを取得します。
        /// </summary>
        /// <remarks>A+T</remarks>
        public static DNABase W { get; } = A + T;

        /// <summary>
        /// 塩基Rを表すインスタンスを取得します。
        /// </summary>
        /// <remarks>A+G</remarks>
        public static DNABase R { get; } = A + G;

        /// <summary>
        /// 塩基Mを表すインスタンスを取得します。
        /// </summary>
        /// <remarks>A+C</remarks>
        public static DNABase M { get; } = A + C;

        /// <summary>
        /// 塩基Kを表すインスタンスを取得します。
        /// </summary>
        /// <remarks>T+G</remarks>
        public static DNABase K { get; } = T + G;

        /// <summary>
        /// 塩基Yを表すインスタンスを取得します。
        /// </summary>
        /// <remarks>T+C</remarks>
        public static DNABase Y { get; } = T + C;

        /// <summary>
        /// 塩基Sを表すインスタンスを取得します。
        /// </summary>
        /// <remarks>G+C</remarks>
        public static DNABase S { get; } = G + C;

        /// <summary>
        /// 塩基Dを表すインスタンスを取得します。
        /// </summary>
        /// <remarks>A+T+G</remarks>
        public static DNABase D { get; } = A + T + G;

        /// <summary>
        /// 塩基Hを表すインスタンスを取得します。
        /// </summary>
        /// <remarks>A+T+C</remarks>
        public static DNABase H { get; } = A + T + C;

        /// <summary>
        /// 塩基Vを表すインスタンスを取得します。
        /// </summary>
        /// <remarks>A+G+C</remarks>
        public static DNABase V { get; } = A + G + C;

        /// <summary>
        /// 塩基Bを表すインスタンスを取得します。
        /// </summary>
        /// <remarks>T+G+C</remarks>
        public static DNABase B { get; } = T + G + C;

        /// <summary>
        /// 塩基Nを表すインスタンスを取得します。
        /// </summary>
        /// <remarks>A+T+G+C</remarks>
        public static DNABase N { get; } = A + T + G + C;

        private readonly byte bits;

        /// <summary>
        /// <see cref="Value"/>が複数種類の塩基を指しているかどうかを表す値を取得します。
        /// </summary>
        public bool IsMultiple => !(Value == 'A' || Value == 'T' || Value == 'G' || Value == 'C');

        /// <summary>
        /// 塩基を表す文字を取得します。
        /// </summary>
        public char Value { get; }

        /// <summary>
        /// <see cref="DNABase"/>の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="value">塩基</param>
        /// <param name="bits">
        /// 塩基のビット情報
        /// <list type="table">A: 0b00000001</list>
        /// <list type="table">T: 0b00000010</list>
        /// <list type="table">G: 0b00000100</list>
        /// <list type="table">C: 0b00001000</list>
        /// </param>
        internal DNABase(char value, byte bits)
        {
            Value = value;
            this.bits = bits;
        }

        /// <summary>
        /// <see cref="DNABase"/>の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="bits">
        /// 塩基のビット情報
        /// <list type="table">A: 0b00000001</list>
        /// <list type="table">T: 0b00000010</list>
        /// <list type="table">G: 0b00000100</list>
        /// <list type="table">C: 0b00001000</list>
        /// </param>
        internal DNABase(byte bits)
        {
            this.bits = bits;
            Value = FromBits(bits);
        }

        /// <summary>
        /// 塩基の文字からビット情報を取得します。
        /// </summary>
        /// <param name="value">塩基を表す文字</param>
        /// <param name="success">ビット情報を取得出来たらtrue，それ以外でfalse</param>
        /// <returns>塩基を表すビット情報</returns>
        private static byte FromChar(char value, out bool success)
        {
            success = true;
            switch (value)
            {
                case 'A': return BIT_A;
                case 'T': return BIT_T;
                case 'G': return BIT_G;
                case 'C': return BIT_C;
                case 'W': return BIT_A | BIT_T;
                case 'R': return BIT_A | BIT_G;
                case 'M': return BIT_A | BIT_C;
                case 'K': return BIT_T | BIT_G;
                case 'Y': return BIT_T | BIT_C;
                case 'S': return BIT_G | BIT_C;
                case 'D': return BIT_A | BIT_T | BIT_G;
                case 'H': return BIT_A | BIT_T | BIT_C;
                case 'V': return BIT_A | BIT_G | BIT_C;
                case 'B': return BIT_T | BIT_G | BIT_C;
                case 'N': return BIT_A | BIT_T | BIT_G | BIT_C;
                default:
                    success = false;
                    return default;
            }
        }

        /// <summary>
        /// ビット情報から塩基の文字を取得します。
        /// </summary>
        /// <param name="bits">ビット情報</param>
        /// <exception cref="ArgumentException"><paramref name="bits"/>が塩基情報を持たない</exception>
        /// <returns>塩基を表す文字</returns>
        private static char FromBits(byte bits)
        {
            bool hasA = (bits & BIT_A) != 0;
            bool hasT = (bits & BIT_T) != 0;
            bool hasG = (bits & BIT_G) != 0;
            bool hasC = (bits & BIT_C) != 0;
            if (hasA)
            {
                if (hasT)
                {
                    if (hasG) return hasC ? 'N' : 'D';
                    return hasC ? 'H' : 'W';
                }
                if (hasG) return hasC ? 'V' : 'R';
                return hasC ? 'M' : 'A';
            }
            if (hasT)
            {
                if (hasG) return hasC ? 'B' : 'K';
                return hasC ? 'Y' : 'T';
            }
            if (hasG) return hasC ? 'S' : 'G';
            return hasC ? 'C' : throw new ArgumentException("塩基が含まれていません", nameof(bits));
        }

        /// <summary>
        /// 文字から<see cref="DNABase"/>の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="value">塩基を表す文字</param>
        /// <exception cref="ArgumentException"><paramref name="value"/>が塩基を表さない</exception>
        /// <returns><see cref="DNABase"/>の新しいインスタンス</returns>
        public static DNABase Parse(char value)
        {
            byte bits = FromChar(value, out bool success);
            if (!success) throw new ArgumentException("塩基を表していません", nameof(value));
            return new DNABase(value, bits);
        }

        /// <summary>
        /// 文字から<see cref="DNABase"/>の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="value">塩基を表す文字</param>
        /// <param name="result"><see cref="DNABase"/>の新しいインスタンス</param>
        /// <returns><paramref name="result"/>を取得出来たらtrue，それ以外でfalse</returns>
        public static bool TryParse(char value, out DNABase result)
        {
            byte bits = FromChar(value, out bool success);
            result = new DNABase(value, bits);
            return success;
        }

        /// <summary>
        /// 含まれているA，T，G，Cの塩基を取得します。
        /// </summary>
        /// <returns>A，T，G，Cからなる塩基の配列</returns>
        public DNABase[] AsATGC()
        {
            var list = new List<DNABase>(4);

            if ((bits & BIT_A) != 0) list.Add(A);
            if ((bits & BIT_T) != 0) list.Add(T);
            if ((bits & BIT_G) != 0) list.Add(G);
            if ((bits & BIT_C) != 0) list.Add(C);

            return list.ToArray();
        }

        /// <inheritdoc/>
        public int CompareTo(DNABase obj) => Chars.IndexOf(Value).CompareTo(Chars.IndexOf(obj.Value));

        int IComparable.CompareTo(object obj)
        {
            if (obj is DNABase other) return CompareTo(other);
            throw new ArgumentException("比較不能な型です", nameof(obj));
        }

        /// <summary>
        /// 加算を行います。
        /// </summary>
        /// <param name="left">左辺の値</param>
        /// <param name="right">右辺の値</param>
        /// <returns>加算後の値</returns>
        private static DNABase Add(DNABase left, DNABase right)
        {
            byte bits = (byte)(left.bits | right.bits);
            return new DNABase(FromBits(bits), bits);
        }

        /// <inheritdoc/>
        public bool Equals(DNABase other) => Value == other.Value;

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is DNABase other && Equals(other);

        /// <inheritdoc/>
        public override string ToString() => Value.ToString();

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(Value);

        public static bool operator ==(DNABase left, DNABase right) => left.Equals(right);

        public static bool operator !=(DNABase left, DNABase right) => !(left == right);

        public static DNABase operator +(DNABase left, DNABase right) => Add(left, right);
    }
}
