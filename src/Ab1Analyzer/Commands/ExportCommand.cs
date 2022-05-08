using System;

namespace Ab1Analyzer
{
    /// <summary>
    /// エクスポートを行うコマンドです。
    /// </summary>
    public class ExportCommand : CommandBase
    {
        /// <summary>
        /// <see cref="ExportCommand"/>の新しいインスタンスを初期化します。
        /// </summary>
        public ExportCommand()
        {
        }

        /// <inheritdoc/>
        public override string GetHelp(bool onlyAbstract)
        {
            return CreateHelpMessage("データをエクスポートします。", new[]
            {
                ("Export Type", @"エクスポートの種類です。
raw: 生データ (csv)
analyzed: 整形済みデータ (csv)
meta: 各要素のメタデータ (csv) ※バイナリ解析者向け
property: 各要素 (json) ※バイナリ解析者向け
fasta: DNA配列 (fasta)"),
                ("File Path", "書き出し先のパス"),
            }, null, onlyAbstract);
        }

        /// <inheritdoc/>
        public override bool Execute(ProcessData data, string[] args)
        {
            if (base.Execute(data, args)) return true;
            if (!CheckLength(args, 2)) return true;
            if (data.FilePath == null)
            {
                Console.WriteLine("読み込まれているABIFファイルがありません。");
                return true;
            }
            string path = args[1];
            try
            {
                switch (args[0])
                {
                    case "raw":
                        data.Wrapper.ExportRawData(path);
                        break;

                    case "analyzed":
                        data.Wrapper.ExportAnalyzedData(path);
                        break;

                    case "meta":
                        data.Data.ExportBinaryMetaData(path);
                        break;

                    case "property":
                        data.Data.ExportElementData(path);
                        break;

                    case "fasta":
                        data.Wrapper.ExportFasta(path);
                        break;

                    default:
                        Console.WriteLine($"エクスポートタイプ\"{args[0]}\"が無効です");
                        return true;
                }
            }
            catch (Exception e)
            {
                ShowError("エクスポートに失敗しました。", e);
                return true;
            }
            return true;
        }
    }
}
