using System;
using System.Text;

namespace Ab1Analyzer
{
    /// <summary>
    /// バイト配列とその変換を扱います。
    /// </summary>
    internal class BitInfo : ICloneable
    {
        private readonly byte[] buffer;

        /// <summary>
        /// <see cref="BitInfo"/>の新しいインスタンスを初期化します。
        /// </summary>
        /// <exception cref="ArgumentNullException"><see cref="buffer"/>がnull</exception>
        /// <param name="buffer">使用する配列</param>
        internal BitInfo(params byte[] buffer)
        {
            this.buffer = buffer ?? throw new ArgumentNullException(nameof(buffer));
        }

        /// <summary>
        /// 指定したインデックスの要素を取得します。
        /// </summary>
        /// <param name="index">インデックス</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/>が0未満またはサイズを超える</exception>
        /// <returns><paramref name="index"/>に対応した要素</returns>
        internal byte this[int index]
        {
            get
            {
                if (index < 0) throw new ArgumentOutOfRangeException(nameof(index), "インデックスが0未満です");
                if (buffer.Length <= index) throw new ArgumentOutOfRangeException(nameof(index), "インデックスがサイズを超えます");
                return buffer[index];
            }
        }

        internal byte this[Index index] => buffer[index];

        internal BitInfo this[Range range] => new BitInfo(buffer[range]);

        /// <summary>
        /// 開始インデックスと使用要素数が<see cref="buffer"/>において適切かどうかを検証します。
        /// </summary>
        /// <param name="start">開始インデックス</param>
        /// <param name="length">使用要素数</param>
        /// <exception cref="ArgumentException">最終インデックスが<see cref="buffer"/>をはみ出す</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="start"/>が0未満</exception>
        private void CheckCompatibleIndex(int start, int length)
        {
            if (start < 0) throw new ArgumentOutOfRangeException(nameof(start), "引数が0未満です");
            if (buffer.Length < start + length) throw new ArgumentException("最終インデックスが配列サイズを超えます");
        }

        /// <summary>
        /// インスタンスの複製を作成します。
        /// </summary>
        /// <returns>このインスタンスの複製</returns>
        public BitInfo Clone() => new BitInfo((byte[])buffer.Clone());

        object ICloneable.Clone() => Clone();

        /// <summary>
        /// 16bit分の要素を取得します。
        /// </summary>
        /// <param name="start">開始インデックス</param>
        /// <exception cref="ArgumentException">最終インデックスが内部配列をはみ出す</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="start"/>が0未満</exception>
        /// <returns>16bit分の要素</returns>
        private byte[] Get16Bit(int start)
        {
            CheckCompatibleIndex(start, 2);
            return new[]
            {
                buffer[start + 1],
                buffer[start],
            };
        }

        /// <summary>
        /// 32bit分の要素を取得します。
        /// </summary>
        /// <param name="start">開始インデックス</param>
        /// <exception cref="ArgumentException">最終インデックスが内部配列をはみ出す</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="start"/>が0未満</exception>
        /// <returns>32bit分の要素</returns>
        private byte[] Get32Bit(int start)
        {
            CheckCompatibleIndex(start, 4);
            return new[]
            {
                buffer[start + 3],
                buffer[start + 2],
                buffer[start + 1],
                buffer[start],
            };
        }

        /// <summary>
        /// 64bit分の要素を取得します。
        /// </summary>
        /// <param name="start">開始インデックス</param>
        /// <exception cref="ArgumentException">最終インデックスが内部配列をはみ出す</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="start"/>が0未満</exception>
        /// <returns>64bit分の要素</returns>
        private byte[] Get64Bit(int start)
        {
            CheckCompatibleIndex(start, 8);
            return new[]
            {
                buffer[start + 7],
                buffer[start + 6],
                buffer[start + 5],
                buffer[start + 4],
                buffer[start + 3],
                buffer[start + 2],
                buffer[start + 1],
                buffer[start],
            };
        }

        /// <summary>
        /// 部分的なインスタンスを取得します。
        /// </summary>
        /// <param name="start">開始インデックス</param>
        /// <param name="length">配列長</param>
        /// <exception cref="ArgumentException">最終インデックスが内部配列をはみ出す</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="start"/>が0未満</exception>
        /// <returns>部分配列を格納した<see cref="BitInfo"/>の新しいインスタンス</returns>
        internal BitInfo Slice(int start, int length)
        {
            CheckCompatibleIndex(start, length);
            return new BitInfo(buffer.SubArray(start, length));
        }

        /// <summary>
        /// <see cref="sbyte"/>に変換します。
        /// </summary>
        /// <param name="start">開始インデックス</param>
        /// <exception cref="ArgumentException">最終インデックスが内部配列をはみ出す</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="start"/>が0未満</exception>
        /// <returns>変換後の値</returns>
        internal sbyte ToSByte(int start = 0)
        {
            CheckCompatibleIndex(start, 1);
            return (sbyte)buffer[start];
        }

        /// <summary>
        /// <see cref="byte"/>に変換します。
        /// </summary>
        /// <param name="start">開始インデックス</param>
        /// <exception cref="ArgumentException">最終インデックスが内部配列をはみ出す</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="start"/>が0未満</exception>
        /// <returns>変換後の値</returns>
        internal byte ToByte(int start = 0)
        {
            CheckCompatibleIndex(start, 1);
            return buffer[start];
        }

        /// <summary>
        /// <see cref="short"/>に変換します。
        /// </summary>
        /// <param name="start">開始インデックス</param>
        /// <exception cref="ArgumentException">最終インデックスが内部配列をはみ出す</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="start"/>が0未満</exception>
        /// <returns>変換後の値</returns>
        internal short ToInt16(int start = 0)
        {
            return BitConverter.ToInt16(Get16Bit(start));
        }

        /// <summary>
        /// <see cref="ushort"/>に変換します。
        /// </summary>
        /// <param name="start">開始インデックス</param>
        /// <exception cref="ArgumentException">最終インデックスが内部配列をはみ出す</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="start"/>が0未満</exception>
        /// <returns>変換後の値</returns>
        internal ushort ToUInt16(int start = 0)
        {
            return BitConverter.ToUInt16(Get16Bit(start));
        }

        /// <summary>
        /// <see cref="int"/>に変換します。
        /// </summary>
        /// <param name="start">開始インデックス</param>
        /// <exception cref="ArgumentException">最終インデックスが内部配列をはみ出す</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="start"/>が0未満</exception>
        /// <returns>変換後の値</returns>
        internal int ToInt32(int start = 0)
        {
            return BitConverter.ToInt32(Get32Bit(start));
        }

        /// <summary>
        /// <see cref="uint"/>に変換します。
        /// </summary>
        /// <param name="start">開始インデックス</param>
        /// <exception cref="ArgumentException">最終インデックスが内部配列をはみ出す</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="start"/>が0未満</exception>
        /// <returns>変換後の値</returns>
        internal uint ToUInt32(int start = 0)
        {
            return BitConverter.ToUInt32(Get32Bit(start));
        }

        /// <summary>
        /// <see cref="long"/>に変換します。
        /// </summary>
        /// <param name="start">開始インデックス</param>
        /// <exception cref="ArgumentException">最終インデックスが内部配列をはみ出す</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="start"/>が0未満</exception>
        /// <returns>変換後の値</returns>
        internal long ToInt64(int start = 0)
        {
            return BitConverter.ToInt64(Get64Bit(start));
        }

        /// <summary>
        /// <see cref="ulong"/>に変換します。
        /// </summary>
        /// <param name="start">開始インデックス</param>
        /// <exception cref="ArgumentException">最終インデックスが内部配列をはみ出す</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="start"/>が0未満</exception>
        /// <returns>変換後の値</returns>
        internal ulong ToUInt64(int start = 0)
        {
            return BitConverter.ToUInt64(Get64Bit(start));
        }

        /// <summary>
        /// <see cref="float"/>に変換します。
        /// </summary>
        /// <param name="start">開始インデックス</param>
        /// <exception cref="ArgumentException">最終インデックスが内部配列をはみ出す</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="start"/>が0未満</exception>
        /// <returns>変換後の値</returns>
        internal float ToSingle(int start = 0)
        {
            return BitConverter.ToSingle(Get32Bit(start));
        }

        /// <summary>
        /// <see cref="double"/>に変換します。
        /// </summary>
        /// <param name="start">開始インデックス</param>
        /// <exception cref="ArgumentException">最終インデックスが内部配列をはみ出す</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="start"/>が0未満</exception>
        /// <returns>変換後の値</returns>
        internal double ToDouble(int start = 0)
        {
            return BitConverter.ToDouble(Get64Bit(start));
        }

        /// <summary>
        /// <see cref="string"/>に変換します。
        /// </summary>
        /// <param name="start">開始インデックス</param>
        /// <param name="length">文字列長 -1で全長</param>
        /// <exception cref="ArgumentException">最終インデックスが内部配列をはみ出す</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="start"/>が0未満</exception>
        /// <returns>変換後の値</returns>
        internal string ToASCIIString(int start = 0, int length = -1)
        {
            if (length == -1) length = buffer.Length - start;
            return Encoding.ASCII.GetString(buffer.SubArray(start, length));
        }

        /// <summary>
        /// 配列に変換します。
        /// </summary>
        /// <returns>内部配列のコピー</returns>
        internal byte[] ToArray() => (byte[])buffer.Clone();
    }
}