using System;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Ab1Analyzer
{
    /// <summary>
    /// ab1ファイルのデータを表すクラスです。
    /// </summary>
    [Serializable]
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
        internal Ab1DirectoryEntry Header { get; private set; }

        /// <summary>
        /// 格納されているデータを取得します。
        /// </summary>
        public Ab1DirectoryCollection Data { get; } = new Ab1DirectoryCollection();

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
            //Common.OutputProperty(result, nameof(Version));
            result.Header = Ab1DirectoryEntry.Create(reader);
            Console.WriteLine();

            inputStream.Position = result.Header.DataOffset;
            for (int i = 0; i < result.Header.ElementCount; i++)
            {
                Ab1Directory directory = Ab1Directory.Create(reader);
                result.Data.Add(directory);
            }

            return result;
        }

        /// <summary>
        /// 各要素のメタデータをcsv形式で出力します。
        /// </summary>
        /// <param name="path">出力先のパス</param>
        public void ExportBinaryMetaData(string path)
        {
            using var writer = new StreamWriter(path, false);
            writer.WriteLine("TagName,TagNumber,ElementType,ElementSize,DataSize,ElementSize*ElementCount");
            foreach (var current in Data)
            {
                var metaData = current.MetaData;
                string elementType = metaData.ElementType.ToString().Replace("EL_", string.Empty);
                if (current.IsArray) elementType = $"{elementType}[{metaData.ElementCount}]";
                writer.WriteLine($"{metaData.TagName},{metaData.TagNumber},{elementType},{metaData.ElementSize},{metaData.DataSize},{metaData.ElementSize * metaData.ElementCount}");
            }
        }

        /// <summary>
        /// 各要素をjson形式で出力します。
        /// </summary>
        /// <param name="path">出力先のパス</param>
        public void ExportElementData(string path)
        {
            var serializedValue = Data
                .Select(x =>
                {
                    string type = x.ElementType.ToString().Replace("EL_", string.Empty);
                    if (x.IsArray) type += $"[{x.MetaData.ElementCount}]";
                    return new
                    {
                        name = x.TagName,
                        number = x.TagNumber,
                        type,
                        elements = x.IsArray ? x.Elements : x.Elements[0],
                    };
                })
                .ToList();
            string json = JsonSerializer.Serialize(serializedValue, new JsonSerializerOptions
            {
                WriteIndented = true,
                IncludeFields = true,
            });
            File.WriteAllText(path, json);
        }
    }
}