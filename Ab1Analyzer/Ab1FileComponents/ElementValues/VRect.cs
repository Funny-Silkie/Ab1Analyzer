using System;

namespace Ab1Analyzer
{
    /// <summary>
    /// 32bit整数を用いる矩形を表します。
    /// </summary>
    [Serializable]
    public struct VRect : IEquatable<VRect>
    {
        public int top;
        public int left;
        public int bottom;
        public int right;

        /// <summary>
        /// <see cref="VRect"/>の新しいインスタンスを初期化します。
        /// </summary>
        public VRect(int top, int left, int bottom, int right)
        {
            this.top = top;
            this.left = left;
            this.bottom = bottom;
            this.right = right;
        }

        /// <inheritdoc/>
        public readonly bool Equals(VRect other) => top == other.top && left == other.left && bottom == other.bottom && right == other.right;

        /// <inheritdoc/>
        public override readonly bool Equals(object obj) => obj is VRect rect && Equals(rect);

        /// <inheritdoc/>
        public override readonly int GetHashCode() => HashCode.Combine(top, left, bottom, right);

        public static bool operator ==(VRect left, VRect right) => left.Equals(right);

        public static bool operator !=(VRect left, VRect right) => !(left == right);
    }
}
