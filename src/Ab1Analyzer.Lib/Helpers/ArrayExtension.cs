using System;

namespace Ab1Analyzer
{
    /// <summary>
    /// 配列の拡張クラス
    /// </summary>
    internal static class ArrayExtension
    {
        /// <summary>
        /// 逆向きの配列を取得します。
        /// このメソッドによって元配列が変更されることはありません。
        /// </summary>
        /// <param name="array">逆転させる配列のインスタンス</param>
        /// <typeparam name="T">配列の要素の型</typeparam>
        /// <exception cref="ArgumentNullException"><paramref name="array"/>がnull</exception>
        /// <returns><paramref name="array"/>の要素が逆転された配列</returns>
        internal static T[] AsReverse<T>(this T[] array)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            var result = (T[])array.Clone();
            Array.Reverse(result);
            return result;
        }

        /// <summary>
        /// 配列の要素を変換します。
        /// </summary>
        /// <typeparam name="TInput">変換元の型</typeparam>
        /// <typeparam name="TOutput">変換先の型</typeparam>
        /// <param name="array">ソース配列</param>
        /// <param name="converter">変換を行う関数</param>
        /// <exception cref="ArgumentNullException"><paramref name="array"/>または<paramref name="converter"/>がnull</exception>
        /// <returns>変換後の配列</returns>
        internal static TOutput[] ConvertAll<TInput, TOutput>(this TInput[] array, Converter<TInput, TOutput> converter)
            => Array.ConvertAll(array, converter);

        /// <summary>
        /// 要素のうち先頭のもののインデックスを検索します。
        /// </summary>
        /// <typeparam name="T">配列の要素の型</typeparam>
        /// <param name="array">検索対象の配列</param>
        /// <param name="item">検索する要素</param>
        /// <exception cref="ArgumentNullException"><paramref name="array"/>がnull</exception>
        /// <returns><paramref name="item"/>のインデックス 見つからなかったら-1</returns>
        internal static int IndexOf<T>(this T[] array, T item) => Array.IndexOf(array, item);

        /// <summary>
        /// 部分配列を生成します。
        /// </summary>
        /// <param name="array">使用する配列</param>
        /// <param name="startIndex">抜き出し開始インデックス</param>
        /// <param name="length">抜き出す要素数</param>
        /// <exception cref="ArgumentException"><paramref name="startIndex"/>と<paramref name="length"/>を加味した際のインデックスが<paramref name="array"/>のサイズを上回る</exception>
        /// <exception cref="ArgumentNullException"><paramref name="array"/>がnull</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="startIndex"/>または<paramref name="length"/>が0未満</exception>
        /// <returns><paramref name="array"/>の部分配列</returns>
        internal static byte[] SubArray(this byte[] array, int startIndex, int length)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (length == 0) return Array.Empty<byte>();
            byte[] result = new byte[length];
            Buffer.BlockCopy(array, startIndex, result, 0, length);
            return result;
        }

        /// <summary>
        /// 部分配列を生成します。
        /// </summary>
        /// <param name="array">使用する配列</param>
        /// <param name="startIndex">抜き出し開始インデックス</param>
        /// <param name="length">抜き出す要素数</param>
        /// <typeparam name="T">配列の要素の型</typeparam>
        /// <exception cref="ArgumentException"><paramref name="startIndex"/>と<paramref name="length"/>を加味した際のインデックスが<paramref name="array"/>のサイズを上回る</exception>
        /// <exception cref="ArgumentNullException"><paramref name="array"/>がnull</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="startIndex"/>または<paramref name="length"/>が0未満</exception>
        /// <returns><paramref name="array"/>の部分配列</returns>
        internal static T[] SubArray<T>(this T[] array, int startIndex, int length)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (length == 0) return Array.Empty<T>();
            var result = new T[length];
            Array.Copy(array, startIndex, result, 0, length);
            return result;
        }
    }
}