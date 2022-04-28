﻿using System;
using System.IO;

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
        /// エレメントタイプコードを取得します。
        /// </summary>
        public short ElementTypeCode { get; private set; }

        /// <summary>
        /// エレメントのバイト数を取得します。
        /// </summary>
        public short ElementSize { get; private set; }

        /// <summary>
        /// エレメント数を取得します。
        /// </summary>
        public int ElementCount { get; private set; }

        /// <summary>
        /// データのバイト数を取得します
        /// </summary>
        public int DataSize { get; private set; }

        /// <summary>
        /// データのオフセットを取得します。
        /// </summary>
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
        /// <param name="elementTypeCode"><see cref="ElementTypeCode"/></param>
        /// <param name="elementSize"><see cref="ElementSize"/></param>
        /// <param name="elementCount"><see cref="ElementCount"/></param>
        /// <param name="dataSize"><see cref="DataSize"/></param>
        /// <param name="dataOffset"><see cref="DataOffset"/></param>
        /// <param name="dataHandle"><see cref="DataHandle"/></param>
        /// <exception cref="ArgumentNullException"><paramref name="tagName"/>がnull</exception>
        public Ab1DirectoryEntry(string tagName, int tagNumber, short elementTypeCode, short elementSize, int elementCount, int dataSize, int dataOffset, int dataHandle)
        {
            TagName = tagName ?? throw new ArgumentNullException(nameof(tagName));
            TagNumber = tagNumber;
            ElementTypeCode = elementTypeCode;
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
            result.TagName = reader.ReadAsString(4);
            result.TagNumber = reader.ReadAsInt32();
            result.ElementTypeCode = reader.ReadAsInt16();
            result.ElementSize = reader.ReadAsInt16();
            result.ElementCount = reader.ReadAsInt32();
            result.DataSize = reader.ReadAsInt32();
            result.DataOffset = reader.ReadAsInt32();
            result.DataHandle = reader.ReadAsInt32();

            return result;
        }
    }
}