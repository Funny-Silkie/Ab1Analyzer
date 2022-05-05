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

        protected LineSeries SeriesA { get; private set; }
        protected LineSeries SeriesT { get; private set; }
        protected LineSeries SeriesG { get; private set; }
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
            InitPlotModel(Model.Value, title);

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
        /// <see cref="PlotModel"/>を初期化します。
        /// </summary>
        /// <param name="model">初期化する<see cref="PlotModel"/>のインスタンス</param>
        /// <param name="title">設定するタイトル</param>
        protected virtual void InitPlotModel(PlotModel model, string title)
        {
            model.Title = title;
            // X軸
            model.Axes.Add(new LinearAxis()
            {
                Key = XAxisKey,
                Position = AxisPosition.Bottom,
                MajorGridlineStyle = LineStyle.Automatic,
                MinorGridlineStyle = LineStyle.Dash,
                Minimum = 0,
            });
            // Y軸
            model.Axes.Add(new LinearAxis()
            {
                Key = YAxisKey,
                Position = AxisPosition.Left,
                MajorGridlineStyle = LineStyle.Automatic,
                MinorGridlineStyle = LineStyle.Dash,
            });
            SeriesA = new LineSeries
            {
                Color = OxyColor.FromRgb(0, 255, 0),
            };
            SeriesT = new LineSeries
            {
                Color = OxyColor.FromRgb(255, 0, 0),
            };
            SeriesG = new LineSeries
            {
                Color = OxyColor.FromRgb(150, 150, 0),
            };
            SeriesC = new LineSeries
            {
                Color = OxyColor.FromRgb(0, 0, 255),
            };
            model.Series.Add(SeriesA);
            model.Series.Add(SeriesT);
            model.Series.Add(SeriesG);
            model.Series.Add(SeriesC);
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
            short min = 0;
            short max = 0;

            SeriesA.Points.Clear();
            SeriesT.Points.Clear();
            SeriesG.Points.Clear();
            SeriesC.Points.Clear();

            for (int i = 0; i < sequence.Count; i++)
            {
                (short a, short t, short g, short c) = sequence[i];
                SeriesA.Points.Add(new DataPoint(i, a));
                SeriesT.Points.Add(new DataPoint(i, t));
                SeriesG.Points.Add(new DataPoint(i, g));
                SeriesC.Points.Add(new DataPoint(i, c));

                min = new[] { min, a, t, g, c }.Min();
                max = new[] { max, a, t, g, c }.Max();
            }

            LinearAxis xAxis = (LinearAxis)Model.Value.GetAxis(XAxisKey);
            xAxis.Maximum = sequence.Count;
            LinearAxis yAxis = (LinearAxis)Model.Value.GetAxis(YAxisKey);
            yAxis.Minimum = min;
            yAxis.Maximum = max;
            Model.Value.InvalidatePlot(true);
        }
    }
}
