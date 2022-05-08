namespace Ab1Analyzer
{
    /// <summary>
    /// 終了コマンドです。
    /// </summary>
    public class ExitCommand : CommandBase
    {
        /// <summary>
        /// <see cref="ExitCommand"/>の新しいインスタンスを初期化します。
        /// </summary>
        public ExitCommand()
        {
        }

        /// <inheritdoc/>
        public override string GetHelp(bool onlyAbstract)
        {
            return CreateHelpMessage("アプリケーションを終了します。", null, null, onlyAbstract);
        }

        /// <inheritdoc/>
        public override bool Execute(ProcessData data, string[] args)
        {
            if (base.Execute(data, args)) return true;

            data.Exit = true;
            return true;
        }
    }
}
