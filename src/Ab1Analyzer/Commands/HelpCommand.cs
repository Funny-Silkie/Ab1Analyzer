using System;

namespace Ab1Analyzer
{
    /// <summary>
    /// ヘルプコマンドです。
    /// </summary>
    public class HelpCommand : CommandBase
    {
        /// <summary>
        /// <see cref="HelpCommand"/>の新しいインスタンスを初期化します。
        /// </summary>
        public HelpCommand()
        {
        }

        /// <inheritdoc/>
        public override string GetHelp(bool onlyAbstract)
        {
            return CreateHelpMessage("ヘルプを表示します。", null, null, onlyAbstract);
        }

        /// <inheritdoc/>
        public override bool Execute(ProcessData data, string[] args)
        {
            if (base.Execute(data, args)) return true;
            foreach (CommandBase command in Commands.Values) Console.WriteLine(command.GetHelp(false));
            return true;
        }
    }
}
