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

        /// <inheritdoc/>
        public readonly bool Equals(VPoint other) => v == other.v && h == other.h;

        /// <inheritdoc/>
        public override readonly bool Equals(object obj) => obj is VPoint point && Equals(point);

        /// <inheritdoc/>
        public override readonly int GetHashCode() => HashCode.Combine(v, h);

        public static bool operator ==(VPoint left, VPoint right) => left.Equals(right);

        public static bool operator !=(VPoint left, VPoint right) => !(left == right);
    }
}
