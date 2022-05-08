using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ab1Analyzer
{
    /// <summary>
    /// コマンドの基底クラスです。
    /// </summary>
    public abstract class CommandBase
    {
        /// <summary>
        /// コマンド一覧を取得します。
        /// </summary>
        public static CommandCollection Commands { get; }

        static CommandBase()
        {
            Commands = new CommandCollection(Comparer<Type>.Create((x, y) => string.Compare(x.FullName, y.FullName)));
            foreach (Type current in GetAllCommandTypes()) Commands.Add(current);
        }

        /// <summary>
        /// コマンド名を取得します。
        /// </summary>
        public virtual string Name
        {
            get
            {
                if (_name == null)
                {
                    string typeName = GetType().Name;
                    if (typeName.EndsWith("Command")) typeName = typeName.Substring(0, typeName.Length - "Command".Length);
                    _name = typeName.ToLower();
                }
                return _name;
            }
        }

        private string _name;

        /// <summary>
        /// <see cref="CommandBase"/>の新しいインスタンスを初期化します。
        /// </summary>
        protected CommandBase()
        {
        }

        /// <summary>
        /// 定義されているコマンドの型を取得します。
        /// </summary>
        /// <returns>定義されているコマンドの型</returns>
        private static IEnumerable<Type> GetAllCommandTypes()
        {
            return Assembly.GetExecutingAssembly()
                .DefinedTypes
                .Where(x =>
                {
                    if (x.IsAbstract) return false;
                    Type type = x;
                    while (type != null)
                    {
                        if (type == typeof(CommandBase)) return true;
                        type = type.BaseType;
                    }
                    return false;
                });
        }

        /// <summary>
        /// ヘルプのテキストを取得します。
        /// </summary>
        /// <param name="onlyAbstract">概要だけを表示するかどうか</param>
        public abstract string GetHelp(bool onlyAbstract);

        /// <summary>
        /// コマンドを実行します。
        /// </summary>
        /// <param name="data">データ</param>
        /// <param name="args">コマンド引数</param>
        /// <returns>実行が終了したらtrue，それ以外でfalse</returns>
        public virtual bool Execute(ProcessData data, string[] args)
        {
            if (args.Length > 0)
            {
                string help = IsOptionName(args[0]);
                if (help == "h" || help == "help")
                {
                    Console.WriteLine(GetHelp(false));
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// オプション名を表すかどうか検証します。
        /// </summary>
        /// <param name="value">検証する値</param>
        /// <returns><paramref name="value"/>がオプション名であったらオプション名，それ以外でnull</returns>
        protected static string IsOptionName(string value)
        {
            if (string.IsNullOrEmpty(value)) return null;
            if (value.StartsWith("-") && value.Length == 2) return value[1].ToString();
            if (value.StartsWith("--")) return value[2..];
            return null;
        }

        /// <summary>
        /// ヘルプメッセージを生成します。
        /// </summary>
        /// <param name="abst">コマンドの概要</param>
        /// <param name="args">引数</param>
        /// <param name="options">オプション</param>
        /// <param name="onlyAbstract">概要だけを表示するかどうか</param>
        /// <returns>ヘルプメッセージ</returns>
        protected string CreateHelpMessage(string abst,
            (string name, string description)[] args,
            (string name, string description)[] options,
            bool onlyAbstract)
        {
            var builder = new StringBuilder();
            builder.Append("--");
            builder.Append(Name);
            builder.AppendLine("--");
            builder.AppendLine();
            builder.Append(Name);
            if (args != null)
                for (int i = 0; i < args.Length; i++)
                {
                    builder.Append(" <");
                    builder.Append(args[i].name);
                    builder.Append('>');
                }
            builder.AppendLine();
            builder.AppendLine(abst);
            if (!onlyAbstract)
            {
                if (args != null && args.Length > 0)
                {
                    builder.AppendLine("--Arguments--");
                    for (int i = 0; i < args.Length; i++)
                    {
                        builder.Append('<');
                        builder.Append(args[i].name);
                        builder.Append('>');
                        builder.AppendLine();
                        builder.AppendLine(args[i].description);
                        builder.AppendLine();
                    }
                }
                if (options != null && options.Length > 0)
                {
                    builder.AppendLine();
                    builder.AppendLine("--Options--");
                    IEnumerable<(string, string)> opt = options
                        .Append(("-h, --help", "ヘルプを表示します。"))
                        .OrderBy(x => x.Item1);
                    foreach ((string name, string description) in options)
                    {
                        builder.Append(name);
                        builder.Append(':');
                        builder.AppendLine();
                        builder.AppendLine(description);
                        builder.AppendLine();
                    }
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// 引数の長さを検証します。
        /// </summary>
        /// <param name="args">引数</param>
        /// <param name="length">最低の長さ</param>
        /// <returns>引数の長さが適切ならtrue，それ以外でfalse</returns>
        protected static bool CheckLength(string[] args, int length)
        {
            if (args.Length < length)
            {
                Console.WriteLine("コマンド引数が足りません。詳しくは-hオプションで確認ください。");
                return false;
            }
            return true;
        }

        /// <summary>
        /// エラーを表示します。
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <param name="e">表示する例外</param>
        protected static void ShowError(string message, Exception e)
        {
            if (e == null) return;
            Console.WriteLine(message);
            Console.WriteLine($"{e.GetType().FullName}: {e.Message}");
            Console.WriteLine($"StackTrace:  {e.StackTrace}");
        }

        /// <summary>
        /// エラーを表示します。
        /// </summary>
        /// <param name="e">表示する例外</param>
        protected static void ShowError(Exception e)
        {
            ShowError("エラーが発生しました", e);
        }
    }
}
