using System;

namespace Ab1Analyzer
{
    /// <summary>
    /// 16bit整数を用いる座標を表します。
    /// </summary>
    [Serializable]
    public struct Point : IEquatable<Point>
    {
        /// <summary>
        /// 上下軸
        /// </summary>
        public short v;

        /// <summary>
        /// 左右軸
        /// </summary>
        public short h;

        /// <summary>
        /// <see cref="Point"/>の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="v">上下軸</param>
        /// <param name="h">左右軸</param>
        public Point(short v, short h)
        {
            this.v = v;
            this.h = h;
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly void Deconstruct(out short v, out short h)
        {
            v = this.v;
            h = this.h;
        }

        /// <inheritdoc/>
        public readonly bool Equals(Point other) => v == other.v && h == other.h;

        /// <inheritdoc/>
        public override readonly bool Equals(object obj) => obj is Point point && Equals(point);

        /// <inheritdoc/>
        public override readonly int GetHashCode() => HashCode.Combine(v, h);

        /// <inheritdoc/>
        public override readonly string ToString() => $"({v}, {h})";

        /// <summary>
        /// <see cref="VPoint"/>に変換します。
        /// </summary>
        /// <returns><see cref="VPoint"/>の新しいインスタンス</returns>
        public readonly VPoint To32bit() => new VPoint(v, h);

        public static bool operator ==(Point left, Point right) => left.Equals(right);

        public static bool operator !=(Point left, Point right) => !(left == right);

        public static Point operator +(Point value) => value;

        public static Point operator -(Point value) => new Point((short)-value.v, (short)-value.h);

        public static Point operator +(Point left, Point right) => new Point((short)(left.v + right.v), (short)(left.h + right.h));

        public static Point operator -(Point left, Point right) => new Point((short)(left.v - right.v), (short)(left.h - right.h));
    }
}
