using Livet.Messaging.IO;

namespace Ab1Analyzer.Visualizer.ViewModels
{
    /// <summary>
    /// ViewModelにおける共通処理を実装します。
    /// </summary>
    internal static class VmCommon
    {
        /// <summary>
        /// ファイルパスを指定しているかどうかを検証します。
        /// </summary>
        /// <param name="message">検証するメッセージ</param>
        /// <returns><paramref name="message"/>が少なくとも一つ以上のファイルパスを指定していたらtrue，それ以外でfalse</returns>
        internal static bool IsEmpty(this FileSelectionMessage message)
        {
            return message == null || message.Response == null || message.Response.Length == 0;
        }
    }
}
