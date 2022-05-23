using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ab1Analyzer
{
    /// <summary>
    /// DNA配列を表すクラスです。
    /// </summary>
    [Serializable]
    public sealed class DNASequence : IEnumerable<DNABase>, IEquatable<DNASequence>, IComparable, IComparable<DNASequence>, ICloneable
    {
        private DNABase[] items;
        private string sequenceString;

        /// <summary>
        /// 空配列を表すインスタンスを取得します。
        /// </summary>
        public static DNASequence Empty { get; } = new DNASequence
        {
            items = Array.Empty<DNABase>(),
            sequenceString = String.Empty,
        };

        /// <summary>
        /// 配列長を取得します。
        /// </summary>
        public int Length => items.Length;

        /// <summary>
        /// <see cref="DNASequence"/>の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="sequence">配列</param>
        public DNASequence(params DNABase[] sequence)
        {
            if (sequence == null || sequence.Length == 0) this.items = Array.Empty<DNABase>();
            else this.items = (DNABase[])sequence.Clone();
        }

        /// <summary>
        /// <see cref="DNASequence"/>の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="sequence">配列</param>
        public DNASequence(IEnumerable<DNABase> sequence)
        {
            if (sequence == null) this.items = Array.Empty<DNABase>();
            else this.items = sequence.ToArray();
        }

        /// <summary>
        /// <see cref="DNASequence"/>の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="sequence">配列</param>
        /// <param name="start">開始インデックス</param>
        /// <param name="count">要素数</param>
        /// <exception cref="ArgumentException"><paramref name="start"/>と<paramref name="count"/>を加味した際のインデックスが<paramref name="sequence"/>のサイズを上回る</exception>
        /// <exception cref="ArgumentNullException"><paramref name="sequence"/>がnull</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="start"/>または<paramref name="count"/>が0未満</exception>
        public DNASequence(DNABase[] sequence, int start, int count)
        {
            items = sequence.SubArray(start, count);
        }

        /// <summary>
        /// <see cref="DNASequence"/>の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="value">使用する塩基</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public DNASequence(DNABase value) : this(value, 1)
        {
        }

        /// <summary>
        /// <see cref="DNASequence"/>の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="value">反復する塩基</param>
        /// <param name="count">反復数</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public DNASequence(DNABase value, int count)
        {
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count), "引数が0未満です");
            items = new DNABase[count];
            Array.Fill(items, value);
        }

        /// <summary>
        /// <see cref="DNASequence"/>の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="sequence">配列</param>
        public DNASequence(ReadOnlySpan<DNABase> sequence)
        {
            items = sequence.ToArray();
        }

        /// <summary>
        /// 指定したインデックスの塩基を取得します。
        /// </summary>
        /// <param name="index">インデックス</param>
        /// <exception cref="IndexOutOfRangeException"><paramref name="index"/>が0未満または<see cref="Length"/>以上</exception>
        /// <returns><paramref name="index"/>に対応する塩基</returns>
        public DNABase this[int index] => items[index];

        public DNABase this[Index index] => items[index];

        public DNASequence this[Range range] => DirectlyCreate(items[range]);

        /// <summary>
        /// 2つの配列を結合します。
        /// </summary>
        /// <param name="seq0">結合する配列</param>
        /// <param name="seq1">結合する配列</param>
        /// <returns>結合された配列</returns>
        public static DNASequence Concat(DNASequence seq0, DNASequence seq1)
        {
            int length0 = seq0?.Length ?? 0;
            int length1 = seq1?.Length ?? 0;

            if (length0 + length1 == 0) return new DNASequence(Array.Empty<DNABase>());

            var array = new DNABase[length0 + length1];
            if (seq0 != null) Array.Copy(seq0.items, 0, array, 0, length0);
            if (seq1 != null) Array.Copy(seq1.items, 0, array, length1, length1);
            return DirectlyCreate(array);
        }

        /// <summary>
        /// 3つの配列を結合します。
        /// </summary>
        /// <param name="seq0">結合する配列</param>
        /// <param name="seq1">結合する配列</param>
        /// <param name="seq2">結合する配列</param>
        /// <returns>結合された配列</returns>
        public static DNASequence Concat(DNASequence seq0, DNASequence seq1, DNASequence seq2)
        {
            int length0 = seq0?.Length ?? 0;
            int length1 = seq1?.Length ?? 0;
            int length2 = seq2?.Length ?? 0;

            if (length0 + length1 + length2 == 0) return new DNASequence(Array.Empty<DNABase>());

            var array = new DNABase[length0 + length1 + length2];
            if (seq0 != null) Array.Copy(seq0.items, 0, array, 0, length0);
            if (seq1 != null) Array.Copy(seq1.items, 0, array, length1, length1);
            if (seq2 != null) Array.Copy(seq2.items, 0, array, length1 + length2, length2);
            return DirectlyCreate(array);
        }

        /// <summary>
        /// 配列をそのまま適用した<see cref="DNASequence"/>の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="array">使用する配列</param>
        /// <exception cref="ArgumentNullException"><paramref name="array"/>がnull</exception>
        /// <returns><paramref name="array"/>を直接<see cref="items"/>とした<see cref="DNASequence"/>の新しいインスタンス</returns>
        private static DNASequence DirectlyCreate(DNABase[] array)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            var result = new DNASequence();
            result.items = array;
            return result;
        }

        /// <summary>
        /// 文字列から<see cref="DNASequence"/>の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="value">読み込む文字列</param>
        /// <exception cref="ArgumentException"><paramref name="value"/>に塩基を表さない文字が含まれている</exception>
        /// <exception cref="ArgumentNullException"><paramref name="value"/>がnull</exception>
        /// <returns><see cref="DNASequence"/>の新しいインスタンス</returns>
        public static DNASequence Parse(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            return new DNASequence(value.Select(DNABase.Parse));
        }

        /// <summary>
        /// 文字列から<see cref="DNASequence"/>の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="value">読み込む文字列</param>
        /// <param name="sequence"><see cref="DNASequence"/>の新しいインスタンス 生成に失敗したら既定値</param>
        /// <returns><paramref name="sequence"/>を生成できたらtrue，それ以外でfalse</returns>
        public static bool TryParse(string value, out DNASequence sequence)
        {
            if (value == null)
            {
                sequence = default;
                return false;
            }
            var list = new List<DNABase>();
            for (int i = 0; i < value.Length; i++)
            {
                if (!DNABase.TryParse(value[i], out DNABase b))
                {
                    sequence = default;
                    return false;
                }
                list.Add(b);
            }

            sequence = DirectlyCreate(list.ToArray());
            return true;
        }

        /// <summary>
        /// このインスタンスの複製を生成します。
        /// </summary>
        /// <returns>このインスタンスの複製</returns>
        public DNASequence Clone() => new DNASequence
        {
            items = items,
            sequenceString = sequenceString,
        };

        object ICloneable.Clone() => Clone();

        /// <inheritdoc/>
        public int CompareTo(DNASequence obj)
        {
            if (obj is null) return 1;
            int length = Math.Min(Length, obj.Length);
            int result = 0;
            for (int i = 0; i < length; i++)
            {
                result = this[i].CompareTo(obj[i]);
                if (result != 0) return result;
            }
            return Length.CompareTo(obj.Length);
        }

        int IComparable.CompareTo(object obj)
        {
            if (obj is null) return 1;
            if (obj is DNASequence other) return CompareTo(other);
            throw new ArgumentException("比較不能な型です", nameof(obj));
        }

        /// <inheritdoc/>
        public bool Equals(DNASequence other)
        {
            if (other is null || Length != other.Length) return false;
            for (int i = 0; i < Length; i++)
                if (this[i] != other[i])
                    return false;
            return true;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj) => Equals(obj as DNASequence);

        /// <summary>
        /// 相補配列を取得します。
        /// </summary>
        /// <returns>相補配列</returns>
        public DNASequence GetComplement()
        {
            return DirectlyCreate(items.ConvertAll(x => x.Complement));
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            for (int i = 0; i < Length; i++) hashCode.Add(this[i]);
            return hashCode.ToHashCode();
        }

        /// <inheritdoc/>
        public IEnumerator<DNABase> GetEnumerator() => ((IEnumerable<DNABase>)items).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// 部分配列を生成します。
        /// </summary>
        /// <param name="startIndex">抜き出し開始インデックス</param>
        /// <param name="length">抜き出す要素数</param>
        /// <typeparam name="T">配列の要素の型</typeparam>
        /// <exception cref="ArgumentException"><paramref name="startIndex"/>と<paramref name="length"/>を加味した際のインデックスが配列長を上回る</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="startIndex"/>または<paramref name="length"/>が0未満</exception>
        /// <returns>部分配列</returns>
        public DNASequence SubSequence(int startIndex, int length) => new DNASequence(items, startIndex, length);

        /// <inheritdoc/>
        public override string ToString()
        {
            if (sequenceString == null) sequenceString = string.Join(string.Empty, this);

            return sequenceString;
        }

        public static bool operator ==(DNASequence left, DNASequence right)
        {
            if (left is null) return right is null;
            return left.Equals(right);
        }

        public static bool operator !=(DNASequence left, DNASequence right) => !(left == right);

        public static DNASequence operator +(DNASequence left, DNASequence right) => Concat(left, right);

        public static DNASequence operator +(DNASequence left, DNABase right)
        {
            if (left == null || left.Length == 0) return new DNASequence(right);
            var array = new DNABase[left.Length + 1];
            Array.Copy(array, 0, left.items, 0, left.Length);
            array[^1] = right;
            return DirectlyCreate(array);
        }
    }
}
