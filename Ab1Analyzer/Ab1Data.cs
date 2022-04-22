using System;
using System.IO;

namespace Ab1Analyzer
{
    /// <summary>
    /// ab1ファイルのデータを表すクラスです。
    /// </summary>
    public class Ab1Data
    {
        /// <summary>
        /// 頭4バイト分の文字列
        /// </summary>
        public const string ABIF = "ABIF";

        /// <summary>
        /// バージョン番号を取得します。
        /// </summary>
        public short Version { get; private set; }

        /// <summary>
        /// ヘッダーを取得します。
        /// </summary>
        public Ad1DirectoryEntry Header { get; private set; }

        /// <summary>
        /// <see cref="Ab1Data"/>の新しいインスタンスを初期化します。
        /// </summary>
        private Ab1Data()
        {
        }

        /// <summary>
        /// <see cref="Ab1Data"/>の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="path">読み込むab1ファイルのパス</param>
        /// <returns><paramref name="path"/>のab1ファイルを読み込んだ<see cref="Ab1Data"/>の新しいインスタンス</returns>
        public static Ab1Data Create(string path)
        {
            using var inputStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var reader = new BinaryReader(inputStream);

            if (reader.ReadAsString(4) != ABIF) throw new ArgumentException("ファイルのフォーマットが無効です。これはab1ファイルではありません", nameof(path));

            var result = new Ab1Data();

            // ヘッダー読み込み
            result.Version = BitConverter.ToInt16(reader.ReadAsByteArray(2).AsReverse());
            result.TagName = reader.ReadAsString(4);
            result.TagNumber = reader.ReadAsInt32();
            result.ElementTypeCode = reader.ReadAsInt16();
            result.ElementSize = reader.ReadAsInt16();
            result.ElementCount = reader.ReadAsInt32();
            result.DataSize = reader.ReadAsInt32();
            result.DataOffset = reader.ReadAsInt32();
            result.DataHandle = reader.ReadAsInt32();

            Common.OutputProperty(result.Header, "Version");
            Common.OutputProperty(result.Header, "TagName");
            Common.OutputProperty(result.Header, "TagNumber");
            Common.OutputProperty(result.Header, "ElementTypeCode");
            Common.OutputProperty(result.Header, "ElementSize");
            Common.OutputProperty(result.Header, "ElementCount");
            Common.OutputProperty(result.Header, "DataSize");
            Common.OutputProperty(result.Header, "DataOffset");
            Common.OutputProperty(result.Header, "DataHandle");

            return result;
        }
    }

    /// <summary>
    /// ad1ファイルのヘッダーのクラスを表します。
    /// </summary>
    public class Ad1DirectoryEntry
    {
        /// <summary>
        /// バージョン番号を取得します。
        /// </summary>
        public short Version { get; private set; }

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
    }
}