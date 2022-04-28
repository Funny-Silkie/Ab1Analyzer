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
            var result = (T[])array.Clone();
            Array.Reverse(result);
            return result;
        }
    }
}