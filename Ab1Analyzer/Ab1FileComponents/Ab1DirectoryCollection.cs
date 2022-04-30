using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ab1Analyzer
{
    /// <summary>
    /// <see cref="Ab1Directory"/>のコレクションのクラスです。
    /// </summary>
    [Serializable]
    public class Ab1DirectoryCollection : ICollection<Ab1Directory>, IReadOnlyCollection<Ab1Directory>, ICollection
    {
        private readonly SortedList<string, Dictionary<int, Ab1Directory>> items;

        /// <inheritdoc/>
        public int Count { get; private set; }

        bool ICollection<Ab1Directory>.IsReadOnly => false;

        bool ICollection.IsSynchronized => false;

        object ICollection.SyncRoot => ((ICollection)items).SyncRoot;

        /// <summary>
        /// <see cref="Ab1DirectoryCollection"/>の新しいインスタンスを初期化します。
        /// </summary>
        public Ab1DirectoryCollection()
        {
            items = new SortedList<string, Dictionary<int, Ab1Directory>>();
        }

        /// <summary>
        /// <see cref="Ab1DirectoryCollection"/>の新しいインスタンスを初期化します。
        /// </summary>
        /// <exception cref="ArgumentException"><paramref name="collection"/>の要素が重複している</exception>
        /// <exception cref="ArgumentNullException"><paramref name="collection"/>がnull</exception>
        /// <param name="collection">ソースにするコレクション</param>
        public Ab1DirectoryCollection(IEnumerable<Ab1Directory> collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            items = collection is ICollection<Ab1Directory> c
                ? new SortedList<string, Dictionary<int, Ab1Directory>>(c.Count)
                : new SortedList<string, Dictionary<int, Ab1Directory>>();
            foreach (Ab1Directory current in collection) Add(current);
        }

        /// <summary>
        /// 指定した名前と番号を持つ要素を取得または設定します。
        /// </summary>
        /// <param name="name">検索する<see cref="Ab1Directory.TagName"/></param>
        /// <param name="number">検索する<see cref="Ab1Directory.TagNumber"/></param>
        /// <exception cref="ArgumentNullException"><paramref name="name"/>または設定しようとした値がnull</exception>
        /// <exception cref="KeyNotFoundException"><paramref name="number"/>と<paramref name="name"/>の組み合わせが見つからない</exception>
        /// <returns><paramref name="number"/>と<paramref name="name"/>に対応する要素</returns>
        public Ab1Directory this[string name, int number]
        {
            get => items[name][number];
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                items[name][number] = value;
            }
        }

        private IEnumerable<Ab1Directory> GetAllItems() => items.SelectMany(x => x.Value, (y, z) => z.Value);

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="item"><inheritdoc/></param>
        /// <exception cref="ArgumentException">既に同じタグの要素が格納されている</exception>
        /// <exception cref="ArgumentNullException"><paramref name="item"/>がnull</exception>
        public void Add(Ab1Directory item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (!items.TryGetValue(item.TagName, out Dictionary<int, Ab1Directory> dictionary))
            {
                dictionary = new Dictionary<int, Ab1Directory>();
                items.Add(item.TagName, dictionary);
            }
            dictionary.Add(item.TagNumber, item);
            Count++;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void Clear()
        {
            items.Clear();
            Count = 0;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="item"><inheritdoc/></param>
        /// <exception cref="ArgumentNullException"><paramref name="item"/>がnull</exception>
        /// <returns><inheritdoc/></returns>
        public bool Contains(Ab1Directory item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            return TryGetValue(item.TagName, item.TagNumber, out Ab1Directory compared) && item == compared;
        }

        /// <summary>
        /// 指定した名前と番号の組み合わせが格納されているかどうかを検証します。
        /// </summary>
        /// <param name="name">検索する<see cref="Ab1Directory.TagName"/></param>
        /// <param name="number">検索する<see cref="Ab1Directory.TagNumber"/></param>
        /// <exception cref="ArgumentNullException"><paramref name="name"/>がnull</exception>
        /// <returns><paramref name="name"/>と<paramref name="number"/>の組み合わせが格納されていたらtrue，それ以外でfalse</returns>
        public bool Contains(string name, int number)
        {
            return items.TryGetValue(name, out Dictionary<int, Ab1Directory> dictionary) && dictionary.ContainsKey(number);
        }

        /// <inheritdoc/>
        public void CopyTo(Ab1Directory[] array, int startIndex)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (startIndex < 0) throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (array.Length < startIndex + Count) throw new ArgumentException(null, nameof(array));
            foreach (var current in GetAllItems()) array[startIndex++] = current;
        }

        void ICollection.CopyTo(Array array, int index) => ((ICollection)items).CopyTo(array, index);

        /// <inheritdoc/>
        public IEnumerator<Ab1Directory> GetEnumerator()
        {
            return GetAllItems().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// 指定した名前と番号を持つ要素を削除します。
        /// </summary>
        /// <param name="name">名前</param>
        /// <param name="number">番号</param>
        /// <exception cref="ArgumentNullException"><paramref name="name"/>がnull</exception>
        /// <returns><paramref name="name"/>と<paramref name="number"/>を持つ要素を削除できたらtrue，それ以外でfalse</returns>
        public bool Remove(string name, int number)
        {
            if (!items.TryGetValue(name, out Dictionary<int, Ab1Directory> dictionary)) return false;
            if (!dictionary.Remove(number)) return false;
            Count--;
            return true;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="item"><inheritdoc/></param>
        /// <exception cref="ArgumentNullException"><paramref name="item"/>がnull</exception>
        /// <returns><inheritdoc/></returns>
        public bool Remove(Ab1Directory item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (!items.TryGetValue(item.TagName, out Dictionary<int, Ab1Directory> dictionary)) return false;
            if (!dictionary.TryGetValue(item.TagNumber, out Ab1Directory removed)) return false;
            if (item != removed) return false;
            if (dictionary.Count == 0) items.Remove(item.TagName);
            Count--;
            return true;
        }

        /// <summary>
        /// 指定した名前と番号を持つ要素を取得または設定します。
        /// </summary>
        /// <param name="name">検索する<see cref="Ab1Directory.TagName"/></param>
        /// <param name="number">検索する<see cref="Ab1Directory.TagNumber"/></param>
        /// <param name="value"><paramref name="number"/>と<paramref name="name"/>に対応する要素 見つからなかったらnull</param>
        /// <exception cref="ArgumentNullException"><paramref name="name"/>がnull</exception>
        /// <returns><paramref name="value"/>が見つかったらtrue，それ以外でfalse</returns>
        public bool TryGetValue(string name, int number, out Ab1Directory value)
        {
            if (!items.TryGetValue(name, out Dictionary<int, Ab1Directory> dictionary))
            {
                value = default;
                return false;
            }
            return dictionary.TryGetValue(number, out value);
        }
    }
}