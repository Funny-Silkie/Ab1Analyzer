using System;

namespace Ab1Analyzer
{
    /// <summary>
    /// 32bit整数を用いる座標を表します。
    /// </summary>
    [Serializable]
    public struct VPoint : IEquatable<VPoint>
    {
        /// <summary>
        /// 上下軸
        /// </summary>
        public int v;

        /// <summary>
        /// 左右軸
        /// </summary>
        public int h;

        /// <summary>
        /// <see cref="VPoint"/>の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="v">上下軸</param>
        /// <param name="h">左右軸</param>
        public VPoint(int v, int h)
        {
            this.v = v;
            this.h = h;
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly void Deconstruct(out int v, out int h)
        {
            v = this.v;
            h = this.h;
        }

        /// <inheritdoc/>
        public readonly bool Equals(VPoint other) => v == other.v && h == other.h;

        /// <inheritdoc/>
        public override readonly bool Equals(object obj) => obj is VPoint point && Equals(point);

        /// <inheritdoc/>
        public override readonly int GetHashCode() => HashCode.Combine(v, h);

        /// <inheritdoc/>
        public override readonly string ToString() => $"({v}, {h})";

        /// <summary>
        /// <see cref="Point"/>に変換します。
        /// </summary>
        /// <returns><see cref="Point"/>の新しいインスタンス</returns>
        public readonly Point To16bit() => new Point((short)v, (short)h);

        public static bool operator ==(VPoint left, VPoint right) => left.Equals(right);

        public static bool operator !=(VPoint left, VPoint right) => !(left == right);

        public static VPoint operator +(VPoint value) => value;

        public static VPoint operator -(VPoint value) => new VPoint(-value.v, -value.h);

        public static VPoint operator +(VPoint left, VPoint right) => new VPoint((left.v + right.v), (left.h + right.h));

        public static VPoint operator -(VPoint left, VPoint right) => new VPoint((left.v - right.v), (left.h - right.h));

        public static implicit operator VPoint(Point value) => value.To32bit();

        public static explicit operator Point(VPoint value) => value.To16bit();
    }
}
