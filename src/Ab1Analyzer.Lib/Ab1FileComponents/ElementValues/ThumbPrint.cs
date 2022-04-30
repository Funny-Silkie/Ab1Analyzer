using System;

namespace Ab1Analyzer
{
    /// <summary>
    /// ローカルコンピュータで生成される一意のファイル識別子を表します。
    /// </summary>
    [Serializable]
    public struct ThumbPrint : IEquatable<ThumbPrint>
    {
        public int d;
        public int u;
        public byte c;
        public byte n;

        /// <summary>
        /// <see cref="ThumbPrint"/>の新しいインスタンスを初期化します。
        /// </summary>
        public ThumbPrint(int d, int u, byte c, byte n)
        {
            this.d = d;
            this.u = u;
            this.c = c;
            this.n = n;
        }

        /// <inheritdoc/>
        public readonly bool Equals(ThumbPrint other) => d == other.d && u == other.u && c == other.c && n == other.n;

        /// <inheritdoc/>
        public override readonly bool Equals(object obj) => obj is ThumbPrint print && Equals(print);

        /// <inheritdoc/>
        public override readonly int GetHashCode() => HashCode.Combine(d, u, c, n);

        public static bool operator ==(ThumbPrint left, ThumbPrint right) => left.Equals(right);

        public static bool operator !=(ThumbPrint left, ThumbPrint right) => !(left == right);
    }
}
