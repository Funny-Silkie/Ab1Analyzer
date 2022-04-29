using System;

namespace Ab1Analyzer.ElementParsers
{
    /// <summary>
    /// 要素の値を変換するクラスです。
    /// </summary>
    public abstract class ElementParser
    {
        /// <summary>
        /// 要素のバイト数を取得します。
        /// </summary>
        /// <remarks>-1は与えられたバイト配列全長を表します。</remarks>
        public abstract short ElementSize { get; }

        /// <summary>
        /// 対応するタイプコードを取得します。
        /// </summary>
        public abstract ElementTypeCode SupportTypeCode { get; }

        /// <summary>
        /// <see cref="ElementParser"/>の新しいインスタンスを初期化します。
        /// </summary>
        protected ElementParser()
        {
        }

        /// <summary>
        /// 要素の型に対応する<see cref="ElementParser"/>を生成します。
        /// </summary>
        /// <param name="code">変換したい要素の型</param>
        /// <returns><paramref name="code"/>に対応する<see cref="ElementParser"/>の新しいインスタンス</returns>
        /// <remarks><paramref name="code"/>に対応するものが無い場合，<see cref="UnknownElementParser"/>が返される</remarks>
        public static ElementParser GetParser(ElementTypeCode code)
        {
            return code switch
            {
                ElementTypeCode.EL_Byte => new ByteElementParser(),
                ElementTypeCode.EL_Char => new CharElementParser(),
                ElementTypeCode.EL_Word => new WordElementParser(),
                ElementTypeCode.EL_Short => new ShortElementParser(),
                ElementTypeCode.EL_Long => new LongElementParser(),
                ElementTypeCode.EL_Float => new FloatElementParser(),
                ElementTypeCode.EL_Double => new DoubleElementParser(),
                ElementTypeCode.EL_Date => new DateElementParser(),
                ElementTypeCode.EL_Time => new TimeElementParser(),
                ElementTypeCode.EL_PString => new PStringElementParser(),
                ElementTypeCode.EL_CString => new CStringElementParser(),
                ElementTypeCode.EL_Thumb => new ThumbElementParser(),
                ElementTypeCode.EL_Bool => new BoolElementParser(),
                ElementTypeCode.EL_Rational => new RationalElementParser(),
                ElementTypeCode.EL_Point => new PointElementParser(),
                ElementTypeCode.EL_Rect => new RectElementParser(),
                ElementTypeCode.EL_VPoint => new VPointElementParser(),
                ElementTypeCode.EL_VRect => new VRectElementParser(),
                ElementTypeCode.EL_Tag => new TagElementParser(),
                _ => new UnknownElementParser(),
            };
        }

        /// <summary>
        /// 変換可能かどうかを取得します。
        /// </summary>
        /// <param name="type">検証する型</param>
        /// <returns><paramref name="type"/>へ変換可能だったらtrue，それ以外でfalse</returns>
        public bool CanParse(ElementTypeCode type) => type == SupportTypeCode;

        /// <summary>
        /// バイト配列から値を変換します。
        /// </summary>
        /// <param name="binary">変換するバイト配列</param>
        /// <param name="elementCount">要素数</param>
        /// <exception cref="ArgumentNullException"><paramref name="binary"/>がnull</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="elementCount"/>が0以下</exception>
        /// <returns>変換後の<paramref name="binary"/>の値</returns>
        public abstract object[] Parse(byte[] binary, int elementCount);
    }

    /// <summary>
    /// 要素の値を変換するクラスです。
    /// </summary>
    /// <typeparam name="T">変換先の型</typeparam>
    public abstract class ElementParser<T> : ElementParser
    {
        /// <summary>
        /// <see cref="ElementParser{T}"/>の新しいインスタンスを初期化します。
        /// </summary>
        protected ElementParser()
        {
        }

        /// <summary>
        /// バイト配列から値を変換します。
        /// </summary>
        /// <param name="binary"><see cref="ElementParser.ElementSize"/>と同サイズのバイナリ配列</param>
        /// <returns>変換後の<paramref name="binary"/>の値</returns>
        protected abstract T ParseInternal(byte[] binary);

        /// <inheritdoc/>
        public override object[] Parse(byte[] binary, int elementCount)
        {
            if (binary == null) throw new ArgumentNullException(nameof(binary));
            if (elementCount <= 0) throw new ArgumentOutOfRangeException(nameof(elementCount), "引数が0以下です");

            if (ElementSize == -1) return new object[] { ParseInternal(binary) };
            object[] result = new object[elementCount];
            for (int i = 0; i < elementCount; i++) result[i] = ParseInternal(binary.SubArray(i * ElementSize, ElementSize));
            return result;
        }
    }
}