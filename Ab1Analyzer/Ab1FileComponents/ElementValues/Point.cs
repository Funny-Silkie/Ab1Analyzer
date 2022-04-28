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

        /// <inheritdoc/>
        public readonly bool Equals(Point other) => v == other.v && h == other.h;

        /// <inheritdoc/>
        public override readonly bool Equals(object obj) => obj is Point point && Equals(point);

        /// <inheritdoc/>
        public override readonly int GetHashCode() => HashCode.Combine(v, h);

        public static bool operator ==(Point left, Point right) => left.Equals(right);

        public static bool operator !=(Point left, Point right) => !(left == right);
    }
}
