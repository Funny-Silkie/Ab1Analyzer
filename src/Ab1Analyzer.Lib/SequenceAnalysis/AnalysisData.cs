using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ab1Analyzer
{
    /// <summary>
    /// 解析済み配列データのクラスです。
    /// </summary>
    [Serializable]
    public class AnalysisData
    {
        private List<short> peaks;

        /// <summary>
        /// ソースのコレクションにおけるピーク一覧を取得します。
        /// </summary>
        public ReadOnlyCollection<short> Peaks => _Peaks ??= new ReadOnlyCollection<short>(peaks);

        private ReadOnlyCollection<short> _Peaks;

        /// <summary>
        /// 配列データを取得します。
        /// </summary>
        public DNASequence Sequence { get; private set; }

        /// <summary>
        /// <see cref="AnalysisData"/>の新しいインスタンスを初期化します。
        /// </summary>
        private AnalysisData()
        {
        }

        /// <summary>
        /// <see cref="AnalysisData"/>の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="analyzedData">解析された整形済みデータ</param>
        /// <param name="start"><paramref name="analyzedData"/>における開始インデックス</param>
        /// <param name="end"><paramref name="analyzedData"/>における終了インデックス -1で全領域</param>
        /// <exception cref="ArgumentException"><paramref name="end"/>が<paramref name="start"/>未満</exception>
        /// <exception cref="ArgumentNullException"><paramref name="analyzedData"/>がnull</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="start"/>が0未満 -または- <paramref name="end"/>が-2以下か<paramref name="analyzedData"/>の要素数以上</exception>
        /// <returns><see cref="AnalysisData"/>の新しいインスタンス</returns>
        public static AnalysisData Create(SequenceData analyzedData, int start, int end)
        {
            if (analyzedData == null) throw new ArgumentNullException(nameof(analyzedData));
            if (start < 0) throw new ArgumentOutOfRangeException(nameof(start), "引数が0未満です");
            if (end == -1) end += analyzedData.Count;
            if (end < start) throw new ArgumentException("終了インデックスが開始インデックスより前にあります", nameof(end));
            if (end >= analyzedData.Count) throw new ArgumentOutOfRangeException(nameof(end), "終了インデックスが要素数を超えています");
            var result = new AnalysisData();
            var peaks = new List<short>();
            var sequence = new List<DNABase>();

            short prevMax = 0;
            int prevDeviation = 1;

            for (int i = start; i < analyzedData.Count; i++)
            {
                (short a, short t, short g, short c) = analyzedData[i];
                short max = Common.Max(a, t, g, c);
                int deviation = max - prevMax;
                if (deviation <= 0 && prevDeviation >= 0)
                {
                    peaks.Add((short)i);
                    var dna = new DNABase();
                    if (max == a) dna += DNABase.A;
                    if (max == t) dna += DNABase.T;
                    if (max == g) dna += DNABase.G;
                    if (max == c) dna += DNABase.C;
                    sequence.Add(dna);
                }
                prevDeviation = deviation;
                prevMax = max;
            }

            result.peaks = peaks;
            result.Sequence = new DNASequence(sequence.ToArray());

            return result;
        }
    }
}
