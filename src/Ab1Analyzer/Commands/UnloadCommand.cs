namespace Ab1Analyzer
{
    /// <summary>
    /// ファイルをアンロードするコマンドです。
    /// </summary>
    public class UnloadCommand : CommandBase
    {
        /// <summary>
        /// <see cref="UnloadCommand"/>の新しいインスタンスを初期化します。
        /// </summary>
        public UnloadCommand()
        {
        }

        /// <inheritdoc/>
        public override string GetHelp(bool onlyAbstract)
        {
            return CreateHelpMessage("ファイルをアンロードします。", null, null, onlyAbstract);
        }

        /// <inheritdoc/>
        public override bool Execute(ProcessData data, string[] args)
        {
            if (base.Execute(data, args)) return true;

            data.FilePath = null;
            data.Data = null;
            data.Wrapper = null;
            return true;
        }
    }
}
