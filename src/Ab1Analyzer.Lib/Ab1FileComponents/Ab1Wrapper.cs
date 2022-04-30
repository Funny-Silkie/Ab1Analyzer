using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Ab1Analyzer
{
    /// <summary>
    /// プロパティの取得に用いるAb1のラッパークラスです。
    /// </summary>
    [Serializable]
    public class Ab1Wrapper
    {
        private const string Tag_CTNM = "CTNM";
        private const string Tag_DATA = "DATA";
        private const string Tag_PBAS = "PBAS";
        private const string Tag_PLOC = "PLOC";

        private readonly Ab1Data file;

        /// <summary>
        /// コンテナ名を取得します。
        /// </summary>
        public string ContainerName => (string)GetOrThrow(Tag_CTNM, 1).Elements[0];

        /// <summary>
        /// 生データを取得します。
        /// </summary>
        public SequenceData RawData
        {
            get
            {
                if (_rawData == null)
                {
                    Ab1Directory a = GetOrThrow(Tag_DATA, 1);
                    Ab1Directory g = GetOrThrow(Tag_DATA, 2);
                    Ab1Directory c = GetOrThrow(Tag_DATA, 3);
                    Ab1Directory t = GetOrThrow(Tag_DATA, 4);

                    _rawData = new SequenceData(a.Elements, t.Elements, g.Elements, c.Elements);
                }
                return _rawData;
            }
        }

        private SequenceData _rawData;

        /// <summary>
        /// 修正後データを取得します。
        /// </summary>
        public SequenceData AnalyzedData
        {
            get
            {
                if (_analyzedData == null)
                {
                    Ab1Directory a = GetOrThrow(Tag_DATA, 9);
                    Ab1Directory g = GetOrThrow(Tag_DATA, 10);
                    Ab1Directory c = GetOrThrow(Tag_DATA, 11);
                    Ab1Directory t = GetOrThrow(Tag_DATA, 12);

                    _analyzedData = new SequenceData(a.Elements, t.Elements, g.Elements, c.Elements);
                }
                return _analyzedData;
            }
        }

        private SequenceData _analyzedData;

        /// <summary>
        /// <see cref="AnalyzedData"/>におけるピークのインデックスを取得します。
        /// </summary>
        public short[] Peaks
        {
            get
            {
                if (_peaks == null)
                {
                    Ab1Directory ploc = GetOrThrow(Tag_PLOC, 2);
                    _peaks = Array.ConvertAll(ploc.Elements, x => (short)x);
                }
                return _peaks;
            }
        }

        private short[] _peaks;

        /// <summary>
        /// 推測した配列データを取得します。
        /// </summary>
        public string Sequence
        {
            get
            {
                if (_sequence == null)
                {
                    Ab1Directory pbas = GetOrThrow(Tag_PBAS, 2);
                    _sequence = Encoding.ASCII.GetString(Array.ConvertAll(pbas.Elements, x => (byte)(sbyte)x));
                }
                return _sequence;
            }
        }

        private string _sequence;

        /// <summary>
        /// <see cref="Ab1Wrapper"/>の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="file">使用する<see cref="Ab1Data"/>のインスタンス</param>
        /// <exception cref="ArgumentNullException"><paramref name="file"/>がnull</exception>
        public Ab1Wrapper(Ab1Data file)
        {
            this.file = file ?? throw new ArgumentNullException(nameof(file));
        }

        /// <summary>
        /// 指定した名前と番号を持つ要素を取得します。
        /// </summary>
        /// <param name="tagName">検索する名前</param>
        /// <param name="tagNumber">検索する番号</param>
        /// <exception cref="InvalidOperationException"><paramref name="tagName"/>と<paramref name="tagNumber"/>に対応する要素が見つからない</exception>
        /// <returns><paramref name="tagName"/>と<paramref name="tagNumber"/>に対応する要素</returns>
        private Ab1Directory GetOrThrow(string tagName, int tagNumber)
        {
            if (file.Data.TryGetValue(tagName, tagNumber, out Ab1Directory result)) return result;
            throw new InvalidOperationException($"データの取得が出来ませんでした\nTagName: {tagName}, TagNumber: {tagNumber}");
        }

        /// <summary>
        /// FASTA形式でエクスポートします。
        /// </summary>
        /// <param name="path">エクスポート先のパス</param>
        public void ExportFasta(string path)
        {
            using var writer = new StreamWriter(path, false);
            writer.Write("> ");
            writer.WriteLine(ContainerName);
            string[] array = Regex.Split(Sequence, $"({new string('.', 50)})");
            for (int i = 0; i < array.Length; i++)
            {
                string current = array[i].Trim();
                if (string.IsNullOrEmpty(current)) continue;
                writer.WriteLine(current);
            }
        }
    }
}