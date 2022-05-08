using System;

namespace Ab1Analyzer
{
    internal class Program
    {
        private static ProcessData data = new();

        private static void Main(string[] args)
        {
            if (args.Length > 0) CommandBase.Commands.GetCommand<LoadCommand>().Execute(data, args);
            while (!data.Exit)
            {
                Console.Write($"{data.FilePath}> ");
                string[] commands = Console.ReadLine().Split(' ');
                DoCommand(commands);
            }
        }

        /// <summary>
        /// コマンドを実行します。
        /// </summary>
        /// <param name="commands">コマンド</param>
        private static void DoCommand(string[] commands)
        {
            if (commands == null || commands.Length == 0 || string.IsNullOrEmpty(commands[0])) return;
            CommandBase command = CommandBase.Commands.FromName(commands[0]);
            if (command != null) command.Execute(data, commands[1..]);
            else Console.WriteLine($"コマンド\"{commands[0]}\"は存在しません。");
        }
    }
}
