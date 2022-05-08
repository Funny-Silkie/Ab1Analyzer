namespace Ab1Analyzer
{
    /// <summary>
    /// 終了コマンドです。
    /// </summary>
    public class ExitCommand : CommandBase
    {
        /// <inheritdoc/>
        public override string Name => "exit";

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
        public override void Execute(ProcessData data, string[] args)
        {
            data.Exit = true;
        }
    }
}
