using System;
using System.IO;

namespace Ab1Analyzer
{
    /// <summary>
    /// ab1ファイル内のデータコンテナを表します。
    /// </summary>
    public class Ab1Directory
    {
        /// <summary>
        /// メタデータを取得します。
        /// </summary>
        public Ab1DirectoryEntry MetaData { get; private set; }

        /// <summary>
        /// <see cref="Ab1Directory"/>の新しいインスタンスを初期化します。
        /// </summary>
        private Ab1Directory()
        {
        }

        /// <summary>
        /// <see cref="Ab1Directory"/>の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="reader">使用する<see cref="BinaryReader"/>のインスタンス</param>
        /// <exception cref="ArgumentNullException"><paramref name="reader"/>がnull</exception>
        /// <returns><see cref="Ab1Directory"/>の新しいインスタンス<returns>
        internal static Ab1Directory Create(BinaryReader reader)
        {
            var result = new Ab1Directory();
            result.MetaData = Ab1DirectoryEntry.Create(reader);
            byte[] data;
            if (result.MetaData.DataOffset <= 4) data = BitConverter.GetBytes(result.MetaData.DataOffset);
            else
            {
                long pos = reader.BaseStream.Position;
                reader.BaseStream.Position = result.MetaData.DataOffset;
                data = reader.ReadAsByteArray(result.MetaData.DataSize);
                reader.BaseStream.Position = pos;
            }

            Console.WriteLine("{1}: {0}", string.Join(',', data), nameof(data));
            Console.WriteLine();
            return result;

        }
    }
}