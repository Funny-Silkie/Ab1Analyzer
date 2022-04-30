using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ab1Analyzer
{
    /// <summary>
    /// 配列データを表すクラスです。
    /// </summary>
    [Serializable]
    public class SequenceData : IReadOnlyCollection<(short a, short t, short g, short c)>
    {
        private readonly short[] a;
        private readonly short[] t;
        private readonly short[] g;
        private readonly short[] c;

        /// <summary>
        /// Aの解析データを取得します。
        /// </summary>
        public SingleData A => _a ??= new SingleData(this, a, 'A');

        private SingleData _a;

        /// <summary>
        /// Tの解析データを取得します。
        /// </summary>
        public SingleData T => _t ??= new SingleData(this, t, 'T');

        private SingleData _t;

        /// <summary>
        /// Gの解析データを取得します。
        /// </summary>
        public SingleData G => _g ??= new SingleData(this, g, 'G');

        private SingleData _g;

        /// <summary>
        /// Cの解析データを取得します。
        /// </summary>
        public SingleData C => _c ??= new SingleData(this, c, 'C');

        private SingleData _c;

        /// <summary>
        /// 格納されている要素数を取得します。
        /// </summary>
        public int Count { get; }

        /// <summary>
        /// <see cref="SequenceData"/>の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="a">Aに対応する解析データ</param>
        /// <param name="t">Tに対応する解析データ</param>
        /// <param name="g">Gに対応する解析データ</param>
        /// <param name="c">Cに対応する解析データ</param>
        /// <exception cref="ArgumentNullException">引数の何れかがnull</exception>
        internal SequenceData(object[] a, object[] t, object[] g, object[] c)
        {
            if (a == null) throw new ArgumentNullException(nameof(a));
            if (t == null) throw new ArgumentNullException(nameof(t));
            if (g == null) throw new ArgumentNullException(nameof(g));
            if (c == null) throw new ArgumentNullException(nameof(c));

            Count = new[] { a.Length, t.Length, g.Length, c.Length }.Min();
            this.a = new short[Count];
            this.t = new short[Count];
            this.g = new short[Count];
            this.c = new short[Count];

            for (int i = 0; i < Count; i++)
            {
                this.a[i] = (short)a[i];
                this.t[i] = (short)t[i];
                this.g[i] = (short)g[i];
                this.c[i] = (short)c[i];
            }
        }

        /// <summary>
        /// インデックスに対応する要素を取得します。
        /// </summary>
        /// <param name="index">インデックス</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/>が0未満または<see cref="Count"/>以上</exception>
        /// <returns><paramref name="index"/>に対応する要素</returns>
        public (short a, short t, short g, short c) this[int index]
        {
            get
            {
                if (index < 0) throw new ArgumentOutOfRangeException(nameof(index), "インデックスが0未満です");
                if (Count <= index) throw new ArgumentOutOfRangeException(nameof(index), "インデックスが要素数以上です");
                return (a[index], t[index], g[index], c[index]);
            }
        }

        /// <summary>
        /// 列挙をサポートするオブジェクトを生成します。
        /// </summary>
        /// <returns>列挙をサポートするオブジェクト</returns>
        public IEnumerator<(short a, short t, short g, short c)> GetEnumerator()
        {
            for (int i = 0; i < Count; i++) yield return (a[i], t[i], g[i], c[i]);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// 塩基に対する解析データを表します。
        /// </summary>
        [Serializable]
        public sealed class SingleData : IList<short>, IReadOnlyList<short>, IList
        {
            private readonly short[] data;
            private readonly SequenceData master;

            /// <summary>
            /// 塩基名を取得します。
            /// </summary>
            public char BaseName { get; }

            /// <summary>
            /// 格納されている要素数を取得します。
            /// </summary>
            public int Count => master.Count;

            /// <summary>
            /// インデックスに対応する要素を取得します。
            /// </summary>
            /// <param name="index">インデックス</param>
            /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/>が0未満または<see cref="Count"/>以上</exception>
            /// <returns><paramref name="index"/>に対応する要素</returns>
            public short this[int index]
            {
                get
                {
                    if (index < 0) throw new ArgumentOutOfRangeException(nameof(index), "インデックスが0未満です");
                    if (Count <= index) throw new ArgumentOutOfRangeException(nameof(index), "インデックスが要素数以上です");
                    return data[index];
                }
            }

            /// <summary>
            /// <see cref="SingleData"/>の新しいインスタンスを初期化します。
            /// </summary>
            /// <param name="master">親となる<see cref="SingleData"/>のインスタンス</param>
            /// <param name="data">データ</param>
            /// <param name="baseName">塩基名</param>
            /// <exception cref="ArgumentNullException"><paramref name="master"/>または<paramref name="data"/>がnull</exception>
            internal SingleData(SequenceData master, short[] data, char baseName)
            {
                this.master = master ?? throw new ArgumentNullException(nameof(master));
                this.data = data ?? throw new ArgumentNullException(nameof(data));
                BaseName = baseName;
            }

            /// <inheritdoc/>
            public bool Contains(short item) => ((ICollection<short>)data).Contains(item);

            /// <inheritdoc/>
            public void CopyTo(short[] array, int arrayIndex) => ((ICollection<short>)data).CopyTo(array, arrayIndex);

            /// <inheritdoc/>
            public int IndexOf(short item) => ((IList<short>)data).IndexOf(item);

            /// <summary>
            /// 列挙をサポートするオブジェクトを生成します。
            /// </summary>
            /// <returns>列挙をサポートするオブジェクト</returns>
            public IEnumerator<short> GetEnumerator() => ((IEnumerable<short>)data).GetEnumerator();

            #region IEnumerable

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            #endregion IEnumerable

            #region ICollection`1

            bool ICollection<short>.IsReadOnly => true;

            void ICollection<short>.Add(short item) => throw new NotSupportedException();

            void ICollection<short>.Clear() => throw new NotSupportedException();

            bool ICollection<short>.Remove(short item) => throw new NotSupportedException();

            #endregion ICollection`1

            #region IList`1

            short IList<short>.this[int index]
            {
                get => this[index];
                set => throw new NotSupportedException();
            }

            void IList<short>.Insert(int index, short item) => throw new NotSupportedException();

            void IList<short>.RemoveAt(int index) => throw new NotSupportedException();

            #endregion IList`1

            #region ICollection

            bool ICollection.IsSynchronized => false;

            object ICollection.SyncRoot => data.SyncRoot;

            void ICollection.CopyTo(Array array, int index) => ((ICollection)data).CopyTo(array, index);

            #endregion ICollection

            #region IList

            bool IList.IsFixedSize => true;

            bool IList.IsReadOnly => true;

            object IList.this[int index]
            {
                get => this[index];
                set => throw new NotSupportedException();
            }

            int IList.Add(object value) => throw new NotSupportedException();

            void IList.Clear() => throw new NotSupportedException();

            bool IList.Contains(object value) => ((IList)data).Contains(value);

            int IList.IndexOf(object value) => ((IList)data).IndexOf(value);

            void IList.Insert(int index, object value) => throw new NotSupportedException();

            void IList.Remove(object value) => throw new NotSupportedException();

            void IList.RemoveAt(int index) => throw new NotSupportedException();

            #endregion IList
        }
    }
}