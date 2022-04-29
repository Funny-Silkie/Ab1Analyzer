using System;
using System.IO;
using System.Text;

namespace Ab1Analyzer
{
    /// <summary>
    /// ad1ファイルのヘッダーのクラスを表します。
    /// </summary>
    public class Ab1DirectoryEntry
    {
        /// <summary>
        /// タグの名前を取得します。
        /// </summary>
        public string TagName { get; private set; }

        /// <summary>
        /// タグ番号を取得します。
        /// </summary>
        public int TagNumber { get; private set; }

        /// <summary>
        /// 値の型を表すコードを取得します。
        /// </summary>
        public ElementTypeCode ElementType { get; private set; }

        /// <summary>
        /// 値のバイト数を取得します。
        /// </summary>
        public short ElementSize { get; private set; }

        /// <summary>
        /// 格納されている値の数を取得します。
        /// </summary>
        public int ElementCount { get; private set; }

        /// <summary>
        /// データのバイト数を取得します
        /// </summary>
        public int DataSize { get; private set; }

        /// <summary>
        /// データまたはそのオフセットを取得します。
        /// </summary>
        /// <remarks>
        /// データが4バイト以下の場合はデータそのものが，4バイトを超える場合はデータの位置を示します。
        /// </remarks>
        public int DataOffset { get; private set; }

        /// <summary>
        /// reserved space
        /// </summary>
        internal int DataHandle { get; private set; }

        /// <summary>
        /// <see cref="Ab1DirectoryEntry"/>の新しいインスタンスを初期化します。
        /// </summary>
        internal Ab1DirectoryEntry()
        {
        }

        /// <summary>
        /// <see cref="Ab1DirectoryEntry"/>の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="tagName"><see cref="TagName"/></param>
        /// <param name="tagNumber"><see cref="TagNumber"/></param>
        /// <param name="elementType"><see cref="ElementType"/></param>
        /// <param name="elementSize"><see cref="ElementSize"/></param>
        /// <param name="elementCount"><see cref="ElementCount"/></param>
        /// <param name="dataSize"><see cref="DataSize"/></param>
        /// <param name="dataOffset"><see cref="DataOffset"/></param>
        /// <param name="dataHandle"><see cref="DataHandle"/></param>
        /// <exception cref="ArgumentNullException"><paramref name="tagName"/>がnull</exception>
        public Ab1DirectoryEntry(string tagName, int tagNumber, ElementTypeCode elementType, short elementSize, int elementCount, int dataSize, int dataOffset, int dataHandle)
        {
            TagName = tagName ?? throw new ArgumentNullException(nameof(tagName));
            TagNumber = tagNumber;
            ElementType = elementType;
            ElementSize = elementSize;
            ElementCount = elementCount;
            DataSize = dataSize;
            DataOffset = dataOffset;
            DataHandle = dataHandle;
        }

        /// <summary>
        /// <see cref="Ab1DirectoryEntry"/>の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        internal static Ab1DirectoryEntry Create(BinaryReader reader)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));
            var result = new Ab1DirectoryEntry();
            result.TagName = reader.ReadAsString(4, Encoding.ASCII);
            result.TagNumber = reader.ReadAsInt32();
            result.ElementType = EnumHelper.ToElementTypeCode(reader.ReadAsInt16());
            result.ElementSize = reader.ReadAsInt16();
            result.ElementCount = reader.ReadAsInt32();
            result.DataSize = reader.ReadAsInt32();
            result.DataOffset = reader.ReadAsInt32();
            result.DataHandle = reader.ReadAsInt32();

            //Common.OutputProperty(result, nameof(TagName));
            //Common.OutputProperty(result, nameof(TagNumber));
            //Common.OutputProperty(result, nameof(ElementType));
            //Common.OutputProperty(result, nameof(ElementSize));
            //Common.OutputProperty(result, nameof(ElementCount));
            //Common.OutputProperty(result, nameof(DataSize));
            //if (result.DataOffset > 4) Common.OutputProperty(result, nameof(DataOffset));
            //Common.OutputProperty(result, nameof(DataHandle));

            return result;
        }
    }
}