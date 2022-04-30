using System.ComponentModel;

namespace Ab1Analyzer.Visualizer.ViewModels
{
    /// <summary>
    /// ViewModelの基底クラスです。
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// <see cref="ViewModelBase"/>の新しいインスタンスを初期化します。
        /// </summary>
        protected ViewModelBase()
        {
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// <see cref="PropertyChanged"/>を実行します。
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        #region Commands

        /// <summary>
        /// オーバーライドしてコマンドを初期化します。
        /// </summary>
        protected virtual void InitializeCommands()
        {
        }

        #endregion Commands
    }
}
