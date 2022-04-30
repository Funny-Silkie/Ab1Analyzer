namespace Ab1Analyzer.ElementParsers
{
    /// <summary>
    /// <see cref="ElementTypeCode.EL_Rect"/>に対応する<see cref="ElementParser{T}"/>です。
    /// </summary>
    internal class RectElementParser : ElementParser<Rect>
    {
        /// <inheritdoc/>
        public override short ElementSize => 8;

        /// <inheritdoc/>
        public override ElementTypeCode SupportTypeCode => ElementTypeCode.EL_Rect;

        /// <summary>
        /// <see cref="RectElementParser"/>の新しいインスタンスを初期化します。
        /// </summary>
        public RectElementParser()
        {
        }

        /// <inheritdoc/>
        protected override Rect ParseInternal(BitInfo bytes)
        {
            var result = new Rect();
            result.top = bytes.ToInt16(0);
            result.left = bytes.ToInt16(2);
            result.bottom = bytes.ToInt16(4);
            result.right = bytes.ToInt16(6);
            return result;
        }
    }
}
