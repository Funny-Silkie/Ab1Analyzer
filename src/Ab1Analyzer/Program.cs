﻿using System;
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
                    data.ExportBinaryMetaData($"{Path.GetFileName(path)}.csv");
                    data.ExportElementData($"{Path.GetFileName(path)}_Data.json");
                    var wrapper = new Ab1Wrapper(data);
                    wrapper.ExportFasta($"{Path.GetFileName(path)}.fasta");
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