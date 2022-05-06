using System;
using System.Linq;

namespace Ab1Analyzer.Visualizer.Models
{
    /// <summary>
    /// 共通処理を実装します。
    /// </summary>
    internal static class Common
    {
        /// <summary>
        /// コマンドライン引数を取得または設定します。
        /// </summary>
        public static string[] Args { get; set; }

        /// <summary>
        /// 最大値を求めます。
        /// </summary>
        /// <param name="values">入力値</param>
        /// <returns><paramref name="values"/>のうち最小の値</returns>
        public static T Max<T>(params T[] values) where T : IComparable<T>
        {
            return values.Max();
        }

        /// <summary>
        /// 最小値を求めます。
        /// </summary>
        /// <param name="values">入力値</param>
        /// <returns><paramref name="values"/>のうち最小の値</returns>
        public static T Min<T>(params T[] values) where T : IComparable<T>
        {
            return values.Min();
        }
    }
}
