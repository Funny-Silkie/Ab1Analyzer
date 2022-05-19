using Ab1Analyzer.ElementParsers;
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
        /// 値の型を表すコードを取得します。
        /// </summary>
        public ElementTypeCode ElementType => MetaData.ElementType;

        /// <summary>
        /// 値を取得します。
        /// </summary>
        public object[] Elements { get; private set; }

        /// <summary>
        /// メタデータを取得します。
        /// </summary>
        internal Ab1DirectoryEntry MetaData { get; private set; }

        /// <summary>
        /// 格納されている値が配列かどうかを表す値を取得します。
        /// </summary>
        public bool IsArray => Elements.Length > 1;

        /// <summary>
        /// タグの名前を取得します。
        /// </summary>
        public string TagName => MetaData.TagName;

        /// <summary>
        /// タグ番号を取得します。
        /// </summary>
        public int TagNumber => MetaData.TagNumber;

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
        /// <returns><see cref="Ab1Directory"/>の新しいインスタンス</returns>
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

            result.Elements = ElementParser.GetParser(result.MetaData.ElementType).Parse(new BitInfo(data), result.MetaData.ElementCount);
            return result;
        }
    }
}
