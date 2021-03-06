using System;

namespace Ab1Analyzer
{
    /// <summary>
    /// 16bit整数を用いる矩形を表します。
    /// </summary>
    [Serializable]
    public struct Rect : IEquatable<Rect>
    {
        public short top;
        public short left;
        public short bottom;
        public short right;

        /// <summary>
        /// <see cref="Rect"/>の新しいインスタンスを初期化します。
        /// </summary>
        public Rect(short top, short left, short bottom, short right)
        {
            this.top = top;
            this.left = left;
            this.bottom = bottom;
            this.right = right;
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly void Deconstruct(out short top, out short left, out short bottom, out short right)
        {
            top = this.top;
            left = this.left;
            bottom = this.bottom;
            right = this.right;
        }

        /// <inheritdoc/>
        public readonly bool Equals(Rect other) => top == other.top && left == other.left && bottom == other.bottom && right == other.right;

        /// <inheritdoc/>
        public override readonly bool Equals(object obj) => obj is Rect rect && Equals(rect);

        /// <inheritdoc/>
        public override readonly int GetHashCode() => HashCode.Combine(top, left, bottom, right);

        /// <inheritdoc/>
        public override readonly string ToString() => $"({top}, {left}, {bottom}, {right})";

        /// <summary>
        /// <see cref="VRect"/>に変換します。
        /// </summary>
        /// <returns><see cref="VRect"/>の新しいインスタンス</returns>
        public readonly VRect To32bit() => new VRect(top, left, bottom, right);

        public static bool operator ==(Rect left, Rect right) => left.Equals(right);

        public static bool operator !=(Rect left, Rect right) => !(left == right);
    }
}
