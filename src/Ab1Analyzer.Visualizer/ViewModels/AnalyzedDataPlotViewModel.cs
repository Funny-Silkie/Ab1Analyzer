using OxyPlot;
using OxyPlot.Series;
using Reactive.Bindings;
using System;

namespace Ab1Analyzer.Visualizer.ViewModels
{
    /// <summary>
    /// 解析済みデータのViewModelのクラスです。
    /// </summary>
    public class AnalyzedDataPlotViewModel : PlotViewModel
    {
        private ScatterSeries seriesPeaks;
        private ScatterSeries seriesPeaksAdv;

        /// <summary>
        /// ピークのグラフを見せるかどうかを表す値を取得または設定します。
        /// </summary>
        public ReactiveProperty<bool> ShowPeaks { get; } = CreateReactiveProperty(false);

        /// <summary>
        /// 再解析データのピークのグラフを見せるかどうかを表す値を取得または設定します。
        /// </summary>
        public ReactiveProperty<bool> ShowPeaksAdv { get; } = CreateReactiveProperty(false);

        /// <summary>
        /// <see cref="AnalyzedDataPlotViewModel"/>の新しいインスタンスを初期化します。
        /// </summary>
        public AnalyzedDataPlotViewModel() : base("AnalyzedData")
        {
            ShowPeaks.Subscribe(OnShowPeaksChanged);
            ShowPeaksAdv.Subscribe(OnShowPeaksAdvChanged);
        }

        /// <summary>
        /// <see cref="ShowPeaks"/>が変更されたときに実行されます。
        /// </summary>
        /// <param name="value">設定された値</param>
        private void OnShowPeaksChanged(bool value)
        {
            seriesPeaks.IsVisible = value;
            Model.Value.InvalidatePlot(true);
        }

        /// <summary>
        /// <see cref="ShowPeaksAdv"/>が変更されたときに実行されます。
        /// </summary>
        /// <param name="value">設定された値</param>
        private void OnShowPeaksAdvChanged(bool value)
        {
            seriesPeaksAdv.IsVisible = value;
            Model.Value.InvalidatePlot(true);
        }

        /// <inheritdoc/>
        protected override void InitPlotModel(string title)
        {
            base.InitPlotModel(title);

            seriesPeaks = new ScatterSeries
            {
                IsVisible = ShowPeaks.Value,
                MarkerFill = OxyColor.FromRgb(0, 0, 0),
                MarkerType = MarkerType.Custom,
                MarkerOutline = new[]
                {
                    new ScreenPoint(-1, -2),
                    new ScreenPoint(1, -2),
                    new ScreenPoint(0, 0),
                }
            };
            seriesPeaksAdv = new ScatterSeries
            {
                IsVisible = ShowPeaks.Value,
                MarkerFill = OxyColor.FromRgb(255, 100, 255),
                MarkerType = MarkerType.Custom,
                MarkerOutline = new[]
                {
                    new ScreenPoint(-1, -2),
                    new ScreenPoint(1, -2),
                    new ScreenPoint(0, 0),
                }
            };
            Model.Value.Series.Add(seriesPeaks);
            Model.Value.Series.Add(seriesPeaksAdv);
        }

        /// <inheritdoc/>
        public override void UpdateGraph(SequenceData sequence)
        {
            seriesPeaks.Points.Clear();
            seriesPeaksAdv.Points.Clear();

            base.UpdateGraph(sequence);
        }

        /// <inheritdoc/>
        protected override void AddPoint(short i, short a, short t, short g, short c, short imax, short imin)
        {
            base.AddPoint(i, a, t, g, c, imax, imin);

            if (Data == null) return;
            int index;
            index = Array.IndexOf(Wrapper.Peaks, i);
            if (index >= 0) seriesPeaks.Points.Add(new ScatterPoint(i, imax));
            if (Wrapper.AdvancedAnalysisData != null)
            {
                index = Wrapper.AdvancedAnalysisData.Peaks.IndexOf(i);
                if (index >= 0) seriesPeaksAdv.Points.Add(new ScatterPoint(i, imax));
            }
        }
    }
}
