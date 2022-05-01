using Microsoft.Win32;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Reactive.Bindings;
using System;
using System.Linq;
using System.Windows;

namespace Ab1Analyzer.Visualizer.ViewModels
{
    /// <summary>
    /// <see cref="Views.MainWindow"/>のViewModelのクラスです。
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        private const string XAxisKey = "X-Axis";
        private const string YAxisKey = "Y-Axis";

        private Ab1Data data;
        private Ab1Wrapper wrapper;

        /// <summary>
        /// 開いているファイルのパスを取得します。
        /// </summary>
        public ReadOnlyReactiveProperty<string> OpenedFilePath { get; }

        private ReactiveProperty<string> _OpenedFilePath = new(default, ReactivePropertyMode.Default ^ ReactivePropertyMode.RaiseLatestValueOnSubscribe);

        /// <summary>
        /// RawDataのプロットモデルを取得します。
        /// </summary>
        public ReadOnlyReactiveProperty<PlotModel> RawDataPlotModel { get; }

        /// <summary>
        /// RawDataのプロットモデルを取得します。
        /// </summary>
        public ReadOnlyReactiveProperty<PlotModel> AnalyzedDataPlotModel { get; }

        /// <summary>
        /// <see cref="MainWindowViewModel"/>の新しいインスタンスを初期化します。
        /// </summary>
        public MainWindowViewModel()
        {
            OpenedFilePath = _OpenedFilePath.ToReadOnlyReactiveProperty();
            RawDataPlotModel = new ReactiveProperty<PlotModel>(new PlotModel()).ToReadOnlyReactiveProperty();
            AnalyzedDataPlotModel = new ReactiveProperty<PlotModel>(new PlotModel()).ToReadOnlyReactiveProperty();

            InitPlotModel(RawDataPlotModel.Value, "RawData");
            InitPlotModel(AnalyzedDataPlotModel.Value, "AnalyzedData");

            _OpenedFilePath.Subscribe(OnOpenedFilePathChanged);
        }

        /// <summary>
        /// <see cref="OpenedFilePath"/>が変更されたときに実行されます。
        /// </summary>
        /// <param name="value">設定された値</param>
        private void OnOpenedFilePathChanged(string value)
        {
            if (value == null)
            {
                ClearGraph(RawDataPlotModel.Value);
                ClearGraph(AnalyzedDataPlotModel.Value);
                return;
            }
            try
            {
                data = Ab1Data.Create(value);
                wrapper = new Ab1Wrapper(data);
                UpdateGraph(RawDataPlotModel.Value, wrapper.RawData);
                UpdateGraph(AnalyzedDataPlotModel.Value, wrapper.AnalyzedData);
            }
            catch (Exception e)
            {
                ClearGraph(RawDataPlotModel.Value);
                MessageBox.Show($"ファイルの読み込みに失敗しました\n{e.GetType().FullName}: {e.Message}");
            }
        }

        /// <summary>
        /// <see cref="PlotModel"/>を初期化します。
        /// </summary>
        /// <param name="model">初期化する<see cref="PlotModel"/>のインスタンス</param>
        /// <param name="title">設定するタイトル</param>
        private void InitPlotModel(PlotModel model, string title)
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
            var seriesA = new LineSeries
            {
                Color = OxyColor.FromRgb(0, 255, 0),
            };
            var seriesT = new LineSeries
            {
                Color = OxyColor.FromRgb(255, 0, 0),
            };
            var seriesG = new LineSeries
            {
                Color = OxyColor.FromRgb(150, 150, 0),
            };
            var seriesC = new LineSeries
            {
                Color = OxyColor.FromRgb(0, 0, 255),
            };
            model.Series.Add(seriesA);
            model.Series.Add(seriesT);
            model.Series.Add(seriesG);
            model.Series.Add(seriesC);
        }

        /// <summary>
        /// グラフのデータを削除します。
        /// </summary>
        /// <param name="model">データを削除する<see cref="PlotModel"/>のインスタンス</param>
        private static void ClearGraph(PlotModel model)
        {
            foreach (var series in model.Series) ((LineSeries)series).Points.Clear();
            model.InvalidatePlot(true);
        }

        /// <summary>
        /// グラフを更新します。
        /// </summary>
        /// <param name="model">更新する<see cref="PlotModel"/>のインスタンス</param>
        /// <param name="sequence">表示するシーケンスデータ</param>
        private void UpdateGraph(PlotModel model, SequenceData sequence)
        {
            short min = 0;
            short max = 0;
            LineSeries seriesA = (LineSeries)model.Series[0];
            LineSeries seriesT = (LineSeries)model.Series[1];
            LineSeries seriesG = (LineSeries)model.Series[2];
            LineSeries seriesC = (LineSeries)model.Series[3];

            seriesA.Points.Clear();
            seriesT.Points.Clear();
            seriesG.Points.Clear();
            seriesC.Points.Clear();

            for (int i = 0; i < sequence.Count; i++)
            {
                (short a, short t, short g, short c) = sequence[i];
                seriesA.Points.Add(new DataPoint(i, a));
                seriesT.Points.Add(new DataPoint(i, t));
                seriesG.Points.Add(new DataPoint(i, g));
                seriesC.Points.Add(new DataPoint(i, c));

                min = new[] { min, a, t, g, c }.Min();
                max = new[] { max, a, t, g, c }.Max();
            }

            LinearAxis xAxis = (LinearAxis)model.GetAxis(XAxisKey);
            xAxis.Maximum = sequence.Count;
            LinearAxis yAxis = (LinearAxis)model.GetAxis(YAxisKey);
            yAxis.Minimum = min;
            yAxis.Maximum = max;
            model.InvalidatePlot(true);
        }

        #region Commands

        /// <inheritdoc/>
        protected override void InitializeCommands()
        {
            base.InitializeCommands();

            OpenFile.Subscribe(CommandOpenFile);
        }

        #region File

        /// <summary>
        /// <see cref="CommandOpenFile"/>を実行するコマンドです。
        /// </summary>
        public ReactiveCommand OpenFile { get; } = new();

        private void CommandOpenFile()
        {
            var dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = ".ab1",
                Filter = "ab1ファイル|*.ab1",
                Multiselect = false,
            };
            if (dialog.ShowDialog() != true) return;
            _OpenedFilePath.Value = dialog.FileName;
        }

        #endregion File

        #endregion Commands
    }
}
