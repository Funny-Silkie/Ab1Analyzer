using Livet;
using Livet.Messaging;
using Livet.Messaging.IO;
using Reactive.Bindings;
using System;
using System.Windows;

namespace Ab1Analyzer.Visualizer.ViewModels
{
    /// <summary>
    /// ViewModelの基底クラスです。
    /// </summary>
    public abstract class ViewModelBase : ViewModel
    {
        /// <summary>
        /// メッセージボックス呼び出し用のMessageKey
        /// </summary>
        protected const string MessageKey_MessageBox = "Message";

        /// <summary>
        /// ウィンドウクローズ用のMessageKey
        /// </summary>
        protected const string MessageKey_Close = "Close";

        /// <summary>
        /// OpenFileDialog用のMessageKey
        /// </summary>
        protected const string MessageKey_OpenFile = "OpenFile";

        /// <summary>
        /// SaveFileDialog用のMessageKey
        /// </summary>
        protected const string MessageKey_SaveFile = "SaveFile";

        /// <summary>
        /// <see cref="ViewModelBase"/>の新しいインスタンスを初期化します。
        /// </summary>
        protected ViewModelBase()
        {
            InitializeCommands();
        }

        #region MessageBox

        /// <summary>
        /// メッセージを表示します。
        /// </summary>
        /// <param name="message">表示するメッセージ内容</param>
        /// <param name="title">ウィンドウのタイトル</param>
        protected void ShowMessage(string message, string title)
        {
            Messenger.Raise(new InformationMessage(message, title, MessageBoxImage.None, MessageKey_MessageBox));
        }

        /// <summary>
        /// 情報を表示します。
        /// </summary>
        /// <param name="message">表示するメッセージ内容</param>
        /// <param name="title">ウィンドウのタイトル</param>
        protected void ShowInformation(string message, string title)
        {
            Messenger.Raise(new InformationMessage(message, title, MessageBoxImage.Information, MessageKey_MessageBox));
        }

        /// <summary>
        /// 注意を表示します。
        /// </summary>
        /// <param name="message">表示するメッセージ内容</param>
        /// <param name="title">ウィンドウのタイトル</param>
        protected void ShowExclamation(string message, string title)
        {
            Messenger.Raise(new InformationMessage(message, title, MessageBoxImage.Exclamation, MessageKey_MessageBox));
        }

        /// <summary>
        /// 警告メッセージを表示します。
        /// </summary>
        /// <param name="message">表示するメッセージ内容</param>
        /// <param name="title">ウィンドウのタイトル</param>
        protected void ShowWarning(string message, string title)
        {
            Messenger.Raise(new InformationMessage(message, title, MessageBoxImage.Warning, MessageKey_MessageBox));
        }

        /// <summary>
        /// エラーメッセージを表示します。
        /// </summary>
        /// <param name="message">表示するメッセージ内容</param>
        /// <param name="title">ウィンドウのタイトル</param>
        protected void ShowError(string message, string title)
        {
            Messenger.Raise(new InformationMessage(message, title, MessageBoxImage.Error, MessageKey_MessageBox));
        }

        /// <summary>
        /// エラーメッセージを表示します。
        /// </summary>
        /// <param name="exception">例外</param>
        /// <remarks><paramref name="exception"/>の<see cref="Exception.Message"/>，<see cref="Exception.StackTrace"/>が表示に含まれます。</remarks>
        protected void ShowError(Exception exception)
        {
            if (exception == null) return;
            ShowError("エラーが発生しました。", exception);
        }

        /// <summary>
        /// エラーメッセージを表示します。
        /// </summary>
        /// <param name="message">エラーメッセージ</param>
        /// <param name="exception">例外</param>
        /// <remarks><paramref name="exception"/>の<see cref="Exception.Message"/>，<see cref="Exception.StackTrace"/>が表示に含まれます。</remarks>
        protected void ShowError(string message, Exception exception)
        {
            if (exception == null) return;
            ShowError($"{message}\n{exception.GetType().FullName}: {exception.Message}\nStackTrace: {exception.StackTrace}", "エラー");
        }

        #endregion MessageBox

        #region FileDialog

        /// <summary>
        /// 一つのファイルを開くダイアログを開きます。
        /// </summary>
        /// <param name="filter">フィルター</param>
        /// <returns>決定されたファイルパス 選ばれていない場合はnull</returns>
        protected string OpenSingleFile(string filter)
        {
            OpeningFileSelectionMessage message = Messenger.GetResponse(new OpeningFileSelectionMessage
            {
                Filter = filter,
                MessageKey = MessageKey_OpenFile,
                MultiSelect = false,
            });
            if (message.IsEmpty()) return null;
            return message.Response[0];
        }

        /// <summary>
        /// 複数のファイルを開くダイアログを開きます。
        /// </summary>
        /// <param name="filter">フィルター</param>
        /// <returns>決定されたファイルパス 選ばれていない場合はnull</returns>
        protected string[] OpenMultipleFiles(string filter)
        {
            OpeningFileSelectionMessage message = Messenger.GetResponse(new OpeningFileSelectionMessage
            {
                Filter = filter,
                MessageKey = MessageKey_OpenFile,
                MultiSelect = true,
            });
            if (message.IsEmpty()) return null;
            return message.Response;
        }

        /// <summary>
        /// ファイルを保存するダイアログを開きます。
        /// </summary>
        /// <param name="filter">フィルター</param>
        /// <param name="fileName">初期ファイル名</param>
        /// <returns>決定されたファイルパス 選ばれていない場合はnull</returns>
        protected string SaveFile(string filter, string fileName)
        {
            SavingFileSelectionMessage message = Messenger.GetResponse(new SavingFileSelectionMessage
            {
                Filter = filter,
                FileName = fileName,
                MessageKey = MessageKey_SaveFile,
            });
            if (message.IsEmpty()) return null;
            return message.Response[0];
        }

        #endregion FileDialog

        #region Commands

        /// <summary>
        /// コマンドを初期化します。
        /// </summary>
        protected virtual void InitializeCommands()
        {
            CloseWindow.Subscribe(CommandCloseWindow);
        }

        /// <summary>
        /// <see cref="CommandCloseWindow"/>を実行します。
        /// </summary>
        public ReactiveCommand CloseWindow { get; } = new();

        /// <summary>
        /// ウィンドウを閉じます。
        /// </summary>
        private void CommandCloseWindow()
        {
            Messenger.Raise(new InteractionMessage
            {
                MessageKey = MessageKey_Close,
            });
        }

        #endregion Commands
    }
}
