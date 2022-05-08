using System;
using System.IO;

namespace Ab1Analyzer
{
    /// <summary>
    /// ABIFファイルを読み込むコマンドです。
    /// </summary>
    public class ReadFileCommand : CommandBase
    {
        /// <inheritdoc/>
        public override string Name => "load";

        /// <summary>
        /// <see cref="ReadFileCommand"/>の新しいインスタンスを初期化します。
        /// </summary>
        public ReadFileCommand()
        {
        }

        /// <inheritdoc/>
        public override string GetHelp(bool onlyAbstract)
        {
            return CreateHelpMessage("ファイルを読み込みます。", new[] { ("ABIF file path", "読み込むファイルのパス") }, null, onlyAbstract);
        }

        /// <inheritdoc/>
        public override void Execute(ProcessData data, string[] args)
        {
            if (!CheckLength(args, 1)) return;
            string path = args[0];
            if (!File.Exists(path))
            {
                Console.WriteLine($"パス：\"{path}\"が存在しません。");
                return;
            }
            Ab1Data ab1;
            try
            {
                ab1 = Ab1Data.Create(path);
            }
            catch (Exception e)
            {
                ShowError("ファイルの読み込みに失敗しました。", e);
                return;
            }
            data.FilePath = path;
            data.Data = ab1;
            data.Wrapper = new Ab1Wrapper(ab1);
        }
    }
}
