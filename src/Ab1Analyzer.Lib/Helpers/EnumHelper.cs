using System;

namespace Ab1Analyzer
{
    /// <summary>
    /// 列挙型のヘルパーのクラスです。
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// <see cref="short"/>と<typeparamref name="TEnum"/>を変換します。
        /// </summary>
        /// <param name="value">変換する値</param>
        public static TEnum Convert<TEnum>(short value) where TEnum : struct, Enum
        {
            return (TEnum)Enum.ToObject(typeof(TEnum), value);
        }

        /// <summary>
        /// <see cref="int"/>と<typeparamref name="TEnum"/>を変換します。
        /// </summary>
        /// <param name="value">変換する値</param>
        public static TEnum Convert<TEnum>(int value) where TEnum : struct, Enum
        {
            return (TEnum)Enum.ToObject(typeof(TEnum), value);
        }

        /// <summary>
        /// <see cref="short"/>から<see cref="ElementTypeCode"/>を生成します。
        /// </summary>
        /// <param name="value">変換する値</param>
        /// <returns><paramref name="value"/>に対応する<see cref="ElementTypeCode"/>の値</returns>
        public static ElementTypeCode ToElementTypeCode(short value)
        {
            if (value > (short)ElementTypeCode.EL_User) return ElementTypeCode.EL_User;
            return Convert<ElementTypeCode>(value);
        }
    }
}