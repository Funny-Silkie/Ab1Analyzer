using Livet.Messaging.IO;
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
    /// <see cref="Views.MainWindow"/>のViewModelのクラスです。
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        private Ab1Data data;
        private Ab1Wrapper wrapper;

        /// <summary>
        /// プレート名を取得します。
        /// </summary>
        public ReadOnlyReactiveProperty<string> ContainerName { get; }

        private ReactiveProperty<string> _ContainerName = new();

        /// <summary>
        /// 開いているファイルのパスを取得します。
        /// </summary>
        public ReadOnlyReactiveProperty<string> OpenedFilePath { get; }

        private ReactiveProperty<string> _OpenedFilePath = CreateReactiveProperty<string>();

        /// <summary>
        /// ファイル情報を持っているかどうかを表す値を取得します。
        /// </summary>
        public ReadOnlyReactiveProperty<bool> HasFile { get; }

        /// <summary>
        /// RawDataのプロットを取得します。
        /// </summary>
        public PlotViewModel RawDataPlot { get; }

        /// <summary>
        /// AnalyzedDataのプロットを取得します。
        /// </summary>
        public AnalyzedDataPlotViewModel AnalyzedDataPlot { get; }

        /// <summary>
        /// 配列を取得します。
        /// </summary>
        public ReadOnlyReactiveProperty<DNASequence> Sequence { get; }

        private ReactiveProperty<DNASequence> _Sequence = new();

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
        /// ピークのグラフを見せるかどうかを表す値を取得または設定します。
        /// </summary>
        public ReactiveProperty<bool> ShowPeaks { get; } = CreateReactiveProperty(false);

        /// <summary>
        /// 再解析データのピークのグラフを見せるかどうかを表す値を取得または設定します。
        /// </summary>
        public ReactiveProperty<bool> ShowPeaksAdv { get; } = CreateReactiveProperty(false);

        /// <summary>
        /// 再解析の開始インデックスを取得または設定します。
        /// </summary>
        public ReactiveProperty<int> AnalyzeStart { get; } = new ReactiveProperty<int>(0);

        /// <summary>
        /// <see cref="AnalyzeStart"/>の最大値を取得または設定します。
        /// </summary>
        public ReactiveProperty<int> AnalyzeStartMax { get; } = new ReactiveProperty<int>(0);

        /// <summary>
        /// 再解析の終了インデックスを取得または設定します。
        /// </summary>
        public ReactiveProperty<int> AnalyzeEnd { get; } = new ReactiveProperty<int>(0);

        /// <summary>
        /// <see cref="AnalyzeEnd"/>の最大値を取得または設定します。
        /// </summary>
        public ReactiveProperty<int> AnalyzeEndMax { get; } = new ReactiveProperty<int>(0);

        /// <summary>
        /// <see cref="AnalyzeEnd"/>の最小値を取得または設定します。
        /// </summary>
        public ReactiveProperty<int> AnalyzeEndMin { get; } = new ReactiveProperty<int>(0);

        /// <summary>
        /// <see cref="MainWindowViewModel"/>の新しいインスタンスを初期化します。
        /// </summary>
        public MainWindowViewModel()
        {
            OpenedFilePath = _OpenedFilePath.ToReadOnlyReactiveProperty();
            ContainerName = _ContainerName.ToReadOnlyReactiveProperty();
            HasFile = OpenedFilePath.Select(x => x != null).ToReadOnlyReactiveProperty();
            RawDataPlot = new PlotViewModel("RawData");
            AnalyzedDataPlot = new AnalyzedDataPlotViewModel();
            Sequence = _Sequence.ToReadOnlyReactiveProperty();

            _OpenedFilePath.Subscribe(OnOpenedFilePathChanged);
            AnalyzeStart.Subscribe(OnAnalyzeStartChanged);
            AnalyzeEnd.Subscribe(OnAnalyzeEndChanged);
            ShowA.Subscribe(OnShowAChanged);
            ShowT.Subscribe(OnShowTChanged);
            ShowG.Subscribe(OnShowGChanged);
            ShowC.Subscribe(OnShowCChanged);
            ShowPeaks.Subscribe(OnShowPeaksChanged);
            ShowPeaksAdv.Subscribe(OnShowPeaksAdvChanged);
        }

        /// <summary>
        /// <see cref="OpenedFilePath"/>が変更されたときに実行されます。
        /// </summary>
        /// <param name="value">設定された値</param>
        private void OnOpenedFilePathChanged(string value)
        {
            if (value == null)
            {
                Clear();
                return;
            }
            try
            {
                data = Ab1Data.Create(value);
                wrapper = new Ab1Wrapper(data);
                AnalyzeEndMax.Value = wrapper.AnalyzedData.Count - 1;
                AnalyzeStart.Value = 0;
                AnalyzeEnd.Value = wrapper.AnalyzedData.Count - 1;
                RawDataPlot.SetData(data, wrapper);
                RawDataPlot.UpdateGraph(wrapper.RawData);
                AnalyzedDataPlot.SetData(data, wrapper);
                AnalyzedDataPlot.UpdateGraph(wrapper.AnalyzedData);
                _ContainerName.Value = wrapper.ContainerName;
                _Sequence.Value = wrapper.Sequence;
            }
            catch (Exception e)
            {
                ShowError("ファイルの読み込みに失敗しました", e);
                Clear();
            }
        }

        /// <summary>
        /// <see cref="AnalyzeStart"/>が変更されたときに実行されます。
        /// </summary>
        /// <param name="value">設定された値</param>
        private void OnAnalyzeStartChanged(int value)
        {
            AnalyzeEndMin.Value = value;
        }

        /// <summary>
        /// <see cref="AnalyzeEnd"/>が変更されたときに実行されます。
        /// </summary>
        /// <param name="value">設定された値</param>
        private void OnAnalyzeEndChanged(int value)
        {
            AnalyzeStartMax.Value = value;
        }

        /// <summary>
        /// <see cref="ShowA"/>が変更されたときに実行されます。
        /// </summary>
        /// <param name="value">設定された値</param>
        private void OnShowAChanged(bool value)
        {
            RawDataPlot.ShowA.Value = value;
            AnalyzedDataPlot.ShowA.Value = value;
        }

        /// <summary>
        /// <see cref="ShowT"/>が変更されたときに実行されます。
        /// </summary>
        /// <param name="value">設定された値</param>
        private void OnShowTChanged(bool value)
        {
            RawDataPlot.ShowT.Value = value;
            AnalyzedDataPlot.ShowT.Value = value;
        }

        /// <summary>
        /// <see cref="ShowG"/>が変更されたときに実行されます。
        /// </summary>
        /// <param name="value">設定された値</param>
        private void OnShowGChanged(bool value)
        {
            RawDataPlot.ShowG.Value = value;
            AnalyzedDataPlot.ShowG.Value = value;
        }

        /// <summary>
        /// <see cref="ShowC"/>が変更されたときに実行されます。
        /// </summary>
        /// <param name="value">設定された値</param>
        private void OnShowCChanged(bool value)
        {
            RawDataPlot.ShowC.Value = value;
            AnalyzedDataPlot.ShowC.Value = value;
        }

        /// <summary>
        /// <see cref="ShowPeaks"/>が変更されたときに実行されます。
        /// </summary>
        /// <param name="value">設定された値</param>
        private void OnShowPeaksChanged(bool value)
        {
            AnalyzedDataPlot.ShowPeaks.Value = value;
        }

        /// <summary>
        /// <see cref="ShowPeaksAdv"/>が変更されたときに実行されます。
        /// </summary>
        /// <param name="value">設定された値</param>
        private void OnShowPeaksAdvChanged(bool value)
        {
            AnalyzedDataPlot.ShowPeaksAdv.Value = value;
        }

        /// <summary>
        /// ABIFファイルが読み込まれていない状態の表示にします。
        /// </summary>
        private void Clear()
        {
            data = null;
            wrapper = null;
            _ContainerName.Value = null;
            _Sequence.Value = null;
            AnalyzeStartMax.Value = 0;
            AnalyzeEndMax.Value = 0;
            AnalyzeEndMin.Value = 0;
            AnalyzeStart.Value = 0;
            AnalyzeEnd.Value = 0;
            RawDataPlot.SetData(data, wrapper);
            RawDataPlot.ClearGraph();
            AnalyzedDataPlot.SetData(data, wrapper);
            AnalyzedDataPlot.ClearGraph();
        }

        #region Commands

        /// <inheritdoc/>
        protected override void InitializeCommands()
        {
            base.InitializeCommands();

            AnalyzeAdv.Subscribe(CommandAnalyzeAdv);

            OpenFile.Subscribe(CommandOpenFile);
            ExportMetaData.Subscribe(CommandExportMetaData);
            ExportProperties.Subscribe(CommandExportProperties);
            ExportRawData.Subscribe(CommandExportRawData);
            ExportAnalyzedData.Subscribe(CommandExportAnalyzedData);
            ExportFasta.Subscribe(CommandExportFasta);
        }

        /// <summary>
        /// <see cref="CommandAnalyzeAdv"/>を実行するコマンドです。
        /// </summary>
        public ReactiveCommand AnalyzeAdv { get; } = new();

        /// <summary>
        /// 配列の再解析を行います。
        /// </summary>
        private void CommandAnalyzeAdv()
        {
            try
            {
                wrapper.AnalyzeSequence(AnalyzeStart.Value, AnalyzeEnd.Value);
                AnalyzedDataPlot.UpdateGraph(wrapper.AnalyzedData);
            }
            catch (Exception e)
            {
                ShowError(e);
            }
        }

        #region File

        /// <summary>
        /// <see cref="CommandOpenFile"/>を実行するコマンドです。
        /// </summary>
        public ReactiveCommand OpenFile { get; } = new();

        /// <summary>
        /// ABIFファイルを読み込みます。
        /// </summary>
        private void CommandOpenFile()
        {
            string path = OpenSingleFile("ABIF Files (*.ab1;*.abi;*.fsa)|*.ab1;*.abi;*.fsa|All Files (*.*)|*.*");
            if (path == null) return;
            _OpenedFilePath.Value = path;
        }

        /// <summary>
        /// <see cref="CommandExportMetaData"/>を実行するコマンドです。
        /// </summary>
        public ReactiveCommand ExportMetaData { get; } = new();

        /// <summary>
        /// 各要素のメタデータを出力します。
        /// </summary>
        private void CommandExportMetaData()
        {
            string path = SaveFile("csv Files (*.csv)|*.csv|All Files (*.*)|*.*", $"{ContainerName.Value}_MetaData.csv");
            if (path == null) return;
            try
            {
                data.ExportBinaryMetaData(path);
            }
            catch (Exception e)
            {
                ShowError("エクスポートに失敗しました", e);
            }
        }

        /// <summary>
        /// <see cref="CommandExportProperties"/>を実行するコマンドです。
        /// </summary>
        public ReactiveCommand ExportProperties { get; } = new();

        /// <summary>
        /// 各要素を出力します。
        /// </summary>
        private void CommandExportProperties()
        {
            string path = SaveFile("json Files (*.json)|*.json|All Files (*.*)|*.*", $"{ContainerName.Value}.json");
            if (path == null) return;
            try
            {
                data.ExportElementData(path);
            }
            catch (Exception e)
            {
                ShowError("エクスポートに失敗しました", e);
            }
        }

        /// <summary>
        /// <see cref="CommandExportRawData"/>を実行するコマンドです。
        /// </summary>
        public ReactiveCommand ExportRawData { get; } = new();

        /// <summary>
        /// Rawデータを出力します。
        /// </summary>
        private void CommandExportRawData()
        {
            string path = SaveFile("csv Files (*.csv)|*.csv|All Files (*.*)|*.*", $"{ContainerName.Value}_RawData.csv");
            if (path == null) return;
            try
            {
                wrapper.ExportRawData(path);
            }
            catch (Exception e)
            {
                ShowError("エクスポートに失敗しました", e);
            }
        }

        /// <summary>
        /// <see cref="CommandExportAnalyzedData"/>を実行するコマンドです。
        /// </summary>
        public ReactiveCommand ExportAnalyzedData { get; } = new();

        /// <summary>
        /// 解析済みデータを出力します。
        /// </summary>
        private void CommandExportAnalyzedData()
        {
            string path = SaveFile("csv Files (*.csv)|*.csv|All Files (*.*)|*.*", $"{ContainerName.Value}_AnalyzedData.csv");
            if (path == null) return;
            try
            {
                wrapper.ExportAnalyzedData(path);
            }
            catch (Exception e)
            {
                ShowError("エクスポートに失敗しました", e);
            }
        }

        /// <summary>
        /// <see cref="CommandExportFasta"/>を実行するコマンドです。
        /// </summary>
        public ReactiveCommand ExportFasta { get; } = new();

        /// <summary>
        /// fasta形式として配列データを出力します。
        /// </summary>
        private void CommandExportFasta()
        {
            string path = SaveFile("fasta Files (*.fasta)|*.fasta|All Files (*.*)|*.*", $"{ContainerName.Value}.fasta");
            if (path == null) return;
            try
            {
                wrapper.ExportFasta(path);
            }
            catch (Exception e)
            {
                ShowError("エクスポートに失敗しました", e);
            }
        }

        #endregion File

        #endregion Commands
    }
}
