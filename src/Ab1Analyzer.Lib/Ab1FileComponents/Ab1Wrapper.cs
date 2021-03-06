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
                    _peaks = ploc.Elements.ConvertAll(x => (short)x);
                }
                return _peaks;
            }
        }

        private short[] _peaks;

        /// <summary>
        /// 推測した配列データを取得します。
        /// </summary>
        public DNASequence Sequence
        {
            get
            {
                if (_sequence == null)
                {
                    Ab1Directory pbas = GetOrThrow(Tag_PBAS, 2);
                    if (!DNASequence.TryParse(Encoding.ASCII.GetString(pbas.Elements.ConvertAll(x => (byte)(sbyte)x)), out _sequence)) _sequence = new DNASequence();
                }
                return _sequence;
            }
        }

        private DNASequence _sequence;

        /// <summary>
        /// 追解析データを取得します。
        /// </summary>
        public AnalysisData AdvancedAnalysisData { get; private set; }

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
            string[] array;
            array = Regex.Split(Sequence.ToString(), $"({new string('.', 50)})");
            for (int i = 0; i < array.Length; i++)
            {
                string current = array[i].Trim();
                if (string.IsNullOrEmpty(current)) continue;
                writer.WriteLine(current);
            }
            if (AdvancedAnalysisData != null)
            {
                writer.Write("> ");
                writer.Write(ContainerName);
                writer.WriteLine("_ReAnalyzed");
                array = Regex.Split(AdvancedAnalysisData.Sequence.ToString(), $"({new string('.', 50)})");
                for (int i = 0; i < array.Length; i++)
                {
                    string current = array[i].Trim();
                    if (string.IsNullOrEmpty(current)) continue;
                    writer.WriteLine(current);
                }
            }
        }

        /// <summary>
        /// 生データをエクスポートします。
        /// </summary>
        /// <param name="path">エクスポート先のパス</param>
        public void ExportRawData(string path)
        {
            ExportSequence(RawData, path);
        }

        /// <summary>
        /// 解析済みデータをエクスポートします。
        /// </summary>
        /// <param name="path">エクスポート先のパス</param>
        public void ExportAnalyzedData(string path)
        {
            ExportSequence(AnalyzedData, path);
        }

        /// <summary>
        /// 配列データをエクスポートします。
        /// </summary>
        /// <param name="data">エクスポートする配列データ</param>
        /// <param name="path">エクスポート先のパス</param>
        /// <exception cref="ArgumentNullException"><paramref name="data"/>がnull</exception>
        private static void ExportSequence(SequenceData data, string path)
        {
            using var writer = new StreamWriter(path, false);
            writer.WriteLine("A,T,G,C");
            foreach ((short a, short t, short g, short c) in data)
            {
                writer.Write(a);
                writer.Write(',');
                writer.Write(t);
                writer.Write(',');
                writer.Write(g);
                writer.Write(',');
                writer.Write(c);
                writer.WriteLine();
            }
        }

        /// <summary>
        /// 塩基配列の読み取りを行い，<see cref="AdvancedAnalysisData"/>に格納します。
        /// </summary>
        /// <param name="start">開始インデックス</param>
        /// <param name="end">終了インデックス -1で全域</param>
        /// <param name="overWrite">既に<see cref="AdvancedAnalysisData"/>を上書きする</param>
        /// <exception cref="ArgumentException"><paramref name="end"/>が<paramref name="start"/>未満</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="start"/>が0未満 -または- <paramref name="end"/>が-2以下か<see cref="AnalyzedData"/>の要素数以上</exception>
        /// <exception cref="InvalidOperationException"><paramref name="overWrite"/>がfalseであり<see cref="AdvancedAnalysisData"/>が既に存在する</exception>
        public void AnalyzeSequence(int start = 0, int end = -1, bool overWrite = true)
        {
            if (!overWrite && AdvancedAnalysisData != null) throw new InvalidOperationException("既に解析データが存在します");
            AdvancedAnalysisData = AnalysisData.Create(AnalyzedData, start, end);
        }
    }
}
