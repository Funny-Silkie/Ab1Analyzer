using System;

namespace Ab1Analyzer
{
    /// <summary>
    /// ABIFタグを表します。
    /// </summary>
    [Serializable]
    public struct Tag : IEquatable<Tag>
    {
        public string name;
        public int number;

        /// <summary>
        /// <see cref="Tag"/>の新しいインスタンスを初期化します。
        /// </summary>
        public Tag(string name, int number)
        {
            this.name = name;
            this.number = number;
        }

        /// <inheritdoc/>
        public readonly bool Equals(Tag other) => name == other.name && number == other.number;

        /// <inheritdoc/>
        public override readonly bool Equals(object obj) => obj is Tag tag && Equals(tag);

        /// <inheritdoc/>
        public override readonly int GetHashCode() => HashCode.Combine(name, number);

        public static bool operator ==(Tag left, Tag right) => left.Equals(right);

        public static bool operator !=(Tag left, Tag right) => !(left == right);
    }
}
