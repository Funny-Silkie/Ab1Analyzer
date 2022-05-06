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
