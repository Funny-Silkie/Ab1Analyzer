using System;
using System.IO;

namespace Ab1Analyzer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                string path = args[i];
                Console.WriteLine(path);
                if (!File.Exists(path))
                {
                    Console.WriteLine("指定されたパスのファイルが存在しません。");
                    continue;
                }
                try
                {
                    Ab1Data data = Ab1Data.Create(path);
                    using var writer = new StreamWriter($"{Path.GetFileName(path)}.csv", false);
                    writer.WriteLine("TagName,TagNumber,ElementType,ElementSize,DataSize,ElementSize*ElementCount");
                    foreach (var current in data.Data)
                    {
                        var metaData = current.MetaData;
                        string elementType = metaData.ElementType.ToString().Replace("EL_", string.Empty);
                        if (metaData.ElementCount > 1) elementType = $"{elementType}[{metaData.ElementCount}]";
                        writer.WriteLine($"{metaData.TagName},{metaData.TagNumber},{elementType},{metaData.ElementSize},{metaData.DataSize},{metaData.ElementSize * metaData.ElementCount}");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"エラーが発生しました：{e.GetType().FullName}{Environment.NewLine}{e.Message}");
                    continue;
                }
            }
        }
    }
}