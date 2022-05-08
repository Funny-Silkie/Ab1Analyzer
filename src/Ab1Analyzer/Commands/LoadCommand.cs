using System;
using System.IO;

namespace Ab1Analyzer
{
    /// <summary>
    /// ABIFファイルを読み込むコマンドです。
    /// </summary>
    public class LoadCommand : CommandBase
    {
        /// <summary>
        /// <see cref="LoadCommand"/>の新しいインスタンスを初期化します。
        /// </summary>
        public LoadCommand()
        {
        }

        /// <inheritdoc/>
        public override string GetHelp(bool onlyAbstract)
        {
            return CreateHelpMessage("ファイルを読み込みます。", new[] { ("ABIF file path", "読み込むファイルのパス") }, null, onlyAbstract);
        }

        /// <inheritdoc/>
        public override bool Execute(ProcessData data, string[] args)
        {
            if (base.Execute(data, args)) return true;

            if (!CheckLength(args, 1)) return true;
            string path = args[0];
            if (!File.Exists(path))
            {
                Console.WriteLine($"パス：\"{path}\"が存在しません。");
                return true;
            }
            path = Path.GetFullPath(path);
            Ab1Data ab1;
            try
            {
                ab1 = Ab1Data.Create(path);
            }
            catch (Exception e)
            {
                ShowError("ファイルの読み込みに失敗しました。", e);
                return true;
            }
            data.FilePath = path;
            data.Data = ab1;
            data.Wrapper = new Ab1Wrapper(ab1);
            return true;
        }
    }
}
