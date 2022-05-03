using System;
using System.Windows;

namespace Ab1Analyzer.Visualizer.ViewModels
{
    /// <summary>
    /// ViewModelにおける共通処理を実装します。
    /// </summary>
    internal static class VmCommon
    {
        /// <summary>
        /// エラーメッセージを表示します。
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <param name="e">エラー</param>
        internal static void ShowErrorMessage(string message, Exception e)
        {
            MessageBox.Show($"{message}\n{e.GetType().FullName}: {e.Message}");
        }
    }
}
