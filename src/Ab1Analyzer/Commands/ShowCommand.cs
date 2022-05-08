using System;

namespace Ab1Analyzer
{
    /// <summary>
    /// 値の表示を行うコマンドです。
    /// </summary>
    public class ShowCommand : CommandBase
    {
        /// <summary>
        /// <see cref="ShowCommand"/>の新しいインスタンスを初期化します。
        /// </summary>
        public ShowCommand()
        {
        }

        /// <inheritdoc/>
        public override string GetHelp(bool onlyAbstract)
        {
            return CreateHelpMessage("ファイル内データの表示を行います。", new[]
            {
                ("Element Name (variadic)", "要素名。複数指定可能"),
            }, new[]
            {
                ("-a, --all", "全ての値を表示します。"),
            }, onlyAbstract);
        }

        /// <inheritdoc/>
        public override bool Execute(ProcessData data, string[] args)
        {
            if (base.Execute(data, args)) return true;
            if (!CheckLength(args, 1)) return true;
            if (Array.IndexOf(args, "-a") >= 0 || Array.IndexOf(args, "--all") >= 0)
            {
                foreach (Ab1Directory element in data.Data.Data)
                {
                    OutputElement(element, true);
                    Console.WriteLine();
                }
            }
            else
                foreach (string name in args)
                {
                    if (!data.Data.Data.TryGetValues(name, out Ab1Directory[] elements))
                    {
                        Console.WriteLine($"要素\"{name}\"は見つかりませんでした。");
                        continue;
                    }
                    Console.WriteLine($"<{name}>");
                    foreach (Ab1Directory element in elements)
                    {
                        OutputElement(element, false);
                        Console.WriteLine();
                    }
                }
            return true;
        }

        /// <summary>
        /// 要素を出力します。
        /// </summary>
        /// <param name="element">出力する要素</param>
        /// <param name="printName">要素名を出力するかどうか</param>
        private static void OutputElement(Ab1Directory element, bool printName = true)
        {
            if (printName) Console.WriteLine($"Name: {element.TagName}");
            Console.WriteLine($"TagNumber: {element.TagNumber}");
            if (element.IsArray)
            {
                Console.WriteLine($"Type: {element.ElementType.ToString().Replace("EL_", string.Empty)}[{element.Elements.Length}]");
                Console.WriteLine("Elements: ");
                Console.Write('[');
                Console.Write(string.Join(',', element.Elements));
                Console.WriteLine(']');
            }
            else
            {
                Console.WriteLine($"Type: {element.ElementType.ToString().Replace("EL_", string.Empty)}");
                Console.Write("Element: ");
                if (element.ElementType == ElementTypeCode.EL_CString || element.ElementType == ElementTypeCode.EL_PString) Console.Write('"');
                Console.Write(element.Elements[0]);
                if (element.ElementType == ElementTypeCode.EL_CString || element.ElementType == ElementTypeCode.EL_PString) Console.Write('"');
                Console.WriteLine();
            }
        }
    }
}
