using System;
using System.Reflection;

namespace Ab1Analyzer
{
    /// <summary>
    ///  共通処理
    /// </summary>
    internal static class Common
    {
        internal static void OutputProperty<T>(T value, string propertyName)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetProperty;
            Console.WriteLine("{0}: {1}", propertyName, typeof(T).GetProperty(propertyName, flags)?.GetValue(value));
        }
    }
}