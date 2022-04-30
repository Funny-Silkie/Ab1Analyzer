using Ab1Analyzer.Visualizer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Ab1Analyzer.Visualizer.ViewModels
{
    /// <summary>
    /// <see cref="Views.MainWindow"/>のViewModelのクラスです。
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        /// <summary>
        /// <see cref="MainWindowViewModel"/>の新しいインスタンスを初期化します。
        /// </summary>
        public MainWindowViewModel()
        {
        }

        #region Commands

        /// <inheritdoc/>
        protected override void InitializeCommands()
        {
            base.InitializeCommands();
        }

        #endregion Commands
    }
}
