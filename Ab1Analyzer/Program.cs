using System;
using System.IO;

namespace Ab1Analyzer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            foreach (string path in args)
            {
                Console.WriteLine(path);
                if (!File.Exists(path))
                {
                    Console.WriteLine("指定されたパスのファイルが存在しません。");
                }
                try
                {
                    using BinaryReader reader = CreateBinaryReader(path);

                }
                catch (Exception e)
                {
                    Console.WriteLine($"エラーが発生しました：{e.GetType().FullName}{Environment.NewLine}{e.Message}");
                    continue;
                }
            }
        }

        /// <summary>
        /// <see cref="BinaryReader"/>の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="path"><読み込むファイルのパス/param>
        /// <returns>パスに基づく<see cref="BinaryReader"/>の新しいインスタンス</returns>
        private static BinaryReader CreateBinaryReader(string path)
        {
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            return new BinaryReader(stream);
        }
    }
}