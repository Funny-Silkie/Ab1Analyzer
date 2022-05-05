using Ab1Analyzer.Visualizer.Models;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Reactive.Bindings;
using System;
using System.Linq;
using System.Reactive.Linq;

namespace Ab1Analyzer.Visualizer.ViewModels
{
    /// <summary>
    /// プロットを管理するViewModelのクラスです。
    /// </summary>
    public class PlotViewModel : ViewModelBase
    {
        /// <summary>
        /// X軸のキー
        /// </summary>
        protected const string XAxisKey = "X-Axis";

        /// <summary>
        /// Y軸のキー
        /// </summary>
        protected const string YAxisKey = "Y-Axis";

        /// <summary>
        /// ab1ファイルのデータを取得します。
        /// </summary>
        protected Ab1Data Data { get; private set; }

        /// <summary>
        /// <see cref="Data"/>のラッパーを取得します。
        /// </summary>
        protected Ab1Wrapper Wrapper { get; private set; }

        /// <summary>
        /// Aの折れ線グラフを取得します。
        /// </summary>
        protected LineSeries SeriesA { get; private set; }

        /// <summary>
        /// Tの折れ線グラフを取得します。
        /// </summary>
        protected LineSeries SeriesT { get; private set; }

        /// <summary>
        /// Gの折れ線グラフを取得します。
        /// </summary>
        protected LineSeries SeriesG { get; private set; }

        /// <summary>
        /// Cの折れ線グラフを取得します。
        /// </summary>
        protected LineSeries SeriesC { get; private set; }

        /// <summary>
        /// プロットのモデルを取得します。
        /// </summary>
        public ReadOnlyReactiveProperty<PlotModel> Model { get; }

        /// <summary>
        /// Aのグラフを見せるかどうかを表す値を取得または設定します。
        /// </summary>
        public ReactiveProperty<bool> ShowA { get; } = CreateReactiveProperty(true);

        /// <summary>
        /// Tのグラフを見せるかどうかを表す値を取得または設定します。
        /// </summary>
        public ReactiveProperty<bool> ShowT { get; } = CreateReactiveProperty(true);

        /// <summary>
        /// Gのグラフを見せるかどうかを表す値を取得または設定します。
        /// </summary>
        public ReactiveProperty<bool> ShowG { get; } = CreateReactiveProperty(true);

        /// <summary>
        /// Cのグラフを見せるかどうかを表す値を取得または設定します。
        /// </summary>
        public ReactiveProperty<bool> ShowC { get; } = CreateReactiveProperty(true);

        /// <summary>
        /// <see cref="PlotViewModel"/>の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="title">表題</param>
        public PlotViewModel(string title)
        {
            Model = CreateReadOnlyProperty(new PlotModel());
            InitPlotModel(title);

            ShowA.Subscribe(OnShowAChanged);
            ShowT.Subscribe(OnShowTChanged);
            ShowG.Subscribe(OnShowGChanged);
            ShowC.Subscribe(OnShowCChanged);
        }

        /// <summary>
        /// <see cref="ShowA"/>が変更されたときに実行されます。
        /// </summary>
        /// <param name="value">設定された値</param>
        private void OnShowAChanged(bool value)
        {
            SeriesA.IsVisible = value;
            Model.Value.InvalidatePlot(true);
        }

        /// <summary>
        /// <see cref="ShowT"/>が変更されたときに実行されます。
        /// </summary>
        /// <param name="value">設定された値</param>
        private void OnShowTChanged(bool value)
        {
            SeriesT.IsVisible = value;
            Model.Value.InvalidatePlot(true);
        }

        /// <summary>
        /// <see cref="ShowG"/>が変更されたときに実行されます。
        /// </summary>
        /// <param name="value">設定された値</param>
        private void OnShowGChanged(bool value)
        {
            SeriesG.IsVisible = value;
            Model.Value.InvalidatePlot(true);
        }

        /// <summary>
        /// <see cref="ShowC"/>が変更されたときに実行されます。
        /// </summary>
        /// <param name="value">設定された値</param>
        private void OnShowCChanged(bool value)
        {
            SeriesC.IsVisible = value;
            Model.Value.InvalidatePlot(true);
        }

        /// <summary>
        /// <see cref="Data"/>と<see cref="Wrapper"/>を設定します。
        /// </summary>
        /// <param name="data">設定する<see cref="Ab1Data"/></param>
        /// <param name="wrapper">設定する<see cref="Ab1Wrapper"/></param>
        public void SetData(Ab1Data data, Ab1Wrapper wrapper)
        {
            Data = data;
            Wrapper = wrapper;
        }

        /// <summary>
        /// <see cref="Model"/>を初期化します。
        /// </summary>
        /// <param name="title">設定するタイトル</param>
        protected virtual void InitPlotModel(string title)
        {
            Model.Value.Title = title;
            Model.Value.Axes.Add(new LinearAxis()
            {
                Key = XAxisKey,
                Position = AxisPosition.Bottom,
                MajorGridlineStyle = LineStyle.Automatic,
                MinorGridlineStyle = LineStyle.Dash,
                Minimum = 0,
            });
            Model.Value.Axes.Add(new LinearAxis()
            {
                Key = YAxisKey,
                Position = AxisPosition.Left,
                MajorGridlineStyle = LineStyle.Automatic,
                MinorGridlineStyle = LineStyle.Dash,
            });
            SeriesA = new LineSeries
            {
                Color = OxyColor.FromRgb(0, 255, 0),
                IsVisible = ShowA.Value,
            };
            SeriesT = new LineSeries
            {
                Color = OxyColor.FromRgb(255, 0, 0),
                IsVisible = ShowT.Value,
            };
            SeriesG = new LineSeries
            {
                Color = OxyColor.FromRgb(150, 150, 0),
                IsVisible = ShowG.Value,
            };
            SeriesC = new LineSeries
            {
                Color = OxyColor.FromRgb(0, 0, 255),
                IsVisible = ShowC.Value,
            };
            Model.Value.Series.Add(SeriesA);
            Model.Value.Series.Add(SeriesT);
            Model.Value.Series.Add(SeriesG);
            Model.Value.Series.Add(SeriesC);
        }

        /// <summary>
        /// グラフのデータを削除します。
        /// </summary>
        public void ClearGraph()
        {
            foreach (var series in Model.Value.Series) ((LineSeries)series).Points.Clear();
            Model.Value.InvalidatePlot(true);
        }

        /// <summary>
        /// グラフを更新します。
        /// </summary>
        /// <param name="sequence">表示するシーケンスデータ</param>
        public virtual void UpdateGraph(SequenceData sequence)
        {
            SeriesA.Points.Clear();
            SeriesT.Points.Clear();
            SeriesG.Points.Clear();
            SeriesC.Points.Clear();

            short min = 0;
            short max = 0;

            for (short i = 0; i < sequence.Count; i++)
            {
                (short a, short t, short g, short c) = sequence[i];
                short imax = Common.Max(a, t, g, c);
                short imin = Common.Min(a, t, g, c);

                AddPoint(i, a, t, g, c, imax, imin);

                max = Math.Max(imax, max);
                min = Math.Min(imin, min);
            }

            LinearAxis xAxis = (LinearAxis)Model.Value.GetAxis(XAxisKey);
            xAxis.Maximum = sequence.Count;
            LinearAxis yAxis = (LinearAxis)Model.Value.GetAxis(YAxisKey);
            yAxis.Minimum = min;
            yAxis.Maximum = max;
            Model.Value.InvalidatePlot(true);
        }

        /// <summary>
        /// グラフの点を登録します。
        /// </summary>
        /// <param name="i">データのインデックス</param>
        /// <param name="a">Aのデータ</param>
        /// <param name="t">Tのデータ</param>
        /// <param name="g">Gのデータ</param>
        /// <param name="c">Cのデータ</param>
        /// <param name="imax"><paramref name="a"/>，<paramref name="t"/>，<paramref name="g"/>，<paramref name="c"/>の中の最大値</param>
        /// <param name="imin"><paramref name="a"/>，<paramref name="t"/>，<paramref name="g"/>，<paramref name="c"/>の中の最小値</param>
        protected virtual void AddPoint(short i, short a, short t, short g, short c, short imax, short imin)
        {
            SeriesA.Points.Add(new DataPoint(i, a));
            SeriesT.Points.Add(new DataPoint(i, t));
            SeriesG.Points.Add(new DataPoint(i, g));
            SeriesC.Points.Add(new DataPoint(i, c));
        }
    }
}
