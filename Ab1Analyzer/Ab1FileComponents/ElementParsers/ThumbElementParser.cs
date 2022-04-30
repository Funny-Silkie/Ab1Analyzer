namespace Ab1Analyzer.ElementParsers
{
    /// <summary>
    /// <see cref="ElementTypeCode.EL_Thumb"/>に対応する<see cref="ElementParser{T}"/>です。
    /// </summary>
    internal class ThumbElementParser : ElementParser<ThumbPrint>
    {
        /// <inheritdoc/>
        public override short ElementSize => 10;

        /// <inheritdoc/>
        public override ElementTypeCode SupportTypeCode => ElementTypeCode.EL_Thumb;

        /// <summary>
        /// <see cref="ThumbElementParser"/>の新しいインスタンスを初期化します。
        /// </summary>
        public ThumbElementParser()
        {
        }

        /// <inheritdoc/>
        protected override ThumbPrint ParseInternal(BitInfo bytes)
        {
            var result = new ThumbPrint();
            result.d = bytes.ToInt32(0);
            result.u = bytes.ToInt32(4);
            result.c = bytes.ToByte(8);
            result.n = bytes.ToByte(9);
            return result;
        }
    }
}
