using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private readonly List<Ab1Directory> children = new();

        /// <summary>
        /// バージョン番号を取得します。
        /// </summary>
        public short Version { get; private set; }

        /// <summary>
        /// ヘッダーを取得します。
        /// </summary>
        internal Ab1DirectoryEntry Header { get; private set; }

        /// <summary>
        /// 格納されているデータを取得します。
        /// </summary>
        public ReadOnlyCollection<Ab1Directory> Data => _Data ??= new(children);

        private ReadOnlyCollection<Ab1Directory> _Data;

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
            result.Version = reader.ReadAsInt16();
            Common.OutputProperty(result, nameof(Version));
            result.Header = Ab1DirectoryEntry.Create(reader);
            Console.WriteLine();

            inputStream.Position = result.Header.DataOffset;
            for (int i = 0; i < result.Header.ElementCount; i++)
            {
                Ab1Directory directory = Ab1Directory.Create(reader);
                result.children.Add(directory);
            }

            return result;
        }
    }
}