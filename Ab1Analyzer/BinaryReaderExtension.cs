using System;
using System.IO;
using System.Text;

namespace Ab1Analyzer
{
    /// <summary>
    /// <see cref="BinaryReader"/>の拡張クラス
    /// </summary>
    internal static class BinaryReaderExtension
    {
        /// <summary>
        /// バイト配列を読み込みます。
        /// </summary>
        /// <param name="reader">読み込みに使用する<see cref="BinaryReader"/>のインスタンス</param>
        /// <param name="count">読み込むバイト数</param>
        /// <exception cref="ArgumentNullException"><paramref name="reader"/>がnull</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/>が0以下</exception>
        /// <returns><param name="count"/>で指定された長さのバイト配列</returns>
        internal static byte[] ReadAsByteArray(this BinaryReader reader, int count)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));
            if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count), "引数が0以下です");
            var result = new byte[count];
            reader.Read(result, 0, count);
            return result;
        }

        /// <summary>
        /// バイト配列を読み込みます。
        /// </summary>
        /// <param name="reader">読み込みに使用する<see cref="BinaryReader"/>のインスタンス</param>
        /// <param name="count">読み込むバイト数</param>
        /// <exception cref="ArgumentNullException"><paramref name="reader"/>がnull</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/>が0以下</exception>
        /// <returns><param name="count"/>で指定された長さのバイト数の文字列</returns>
        internal static string ReadAsString(this BinaryReader reader, int count)
        {
            return ReadAsString(reader, count, Encoding.UTF8);
        }

        /// <summary>
        /// バイト配列を読み込みます。
        /// </summary>
        /// <param name="reader">読み込みに使用する<see cref="BinaryReader"/>のインスタンス</param>
        /// <param name="count">読み込むバイト数</param>
        /// <param name="encoding">使用するエンコード</param>
        /// <exception cref="ArgumentNullException"><paramref name="reader"/>または<paramref name="encoding"/>がnull</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/>が0以下</exception>
        /// <returns><param name="count"/>で指定された長さのバイト数の文字列</returns>
        internal static string ReadAsString(this BinaryReader reader, int count, Encoding encoding)
        {
            if (reader == null) throw new ArgumentNullException(nameof(encoding));
            if (reader == null) throw new ArgumentNullException(nameof(reader));
            if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count), "引数が0以下です");
            var buffer = new byte[count];
            reader.Read(buffer, 0, count);
            return encoding.GetString(buffer);
        }

        /// <summary>
        /// 16ビット符号付き整数を読み込みます。
        /// </summary> 
        /// <exception cref="ArgumentNullException"><paramref name="reader"/>がnull</exception>
        /// <returns>16ビット符号付き整数</returns>
        internal static short ReadAsInt16(this BinaryReader reader)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));
            var buffer = reader.ReadAsByteArray(2).AsReverse();
            return BitConverter.ToInt16(buffer);
        }

        /// <summary>
        /// 32ビット符号付き整数を読み込みます。
        /// </summary> 
        /// <exception cref="ArgumentNullException"><paramref name="reader"/>がnull</exception>
        /// <returns>32ビット符号付き整数</returns>
        internal static int ReadAsInt32(this BinaryReader reader)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));
            var buffer = reader.ReadAsByteArray(4).AsReverse();
            return BitConverter.ToInt32(buffer);
        }
    }
}