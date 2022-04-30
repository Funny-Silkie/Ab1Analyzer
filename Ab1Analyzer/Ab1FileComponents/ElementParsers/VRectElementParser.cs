namespace Ab1Analyzer.ElementParsers
{
    /// <summary>
    /// <see cref="ElementTypeCode.EL_VRect"/>に対応する<see cref="ElementParser{T}"/>です。
    /// </summary>
    internal class VRectElementParser : ElementParser<VRect>
    {
        /// <inheritdoc/>
        public override short ElementSize => 16;

        /// <inheritdoc/>
        public override ElementTypeCode SupportTypeCode => ElementTypeCode.EL_VRect;

        /// <summary>
        /// <see cref="VRectElementParser"/>の新しいインスタンスを初期化します。
        /// </summary>
        public VRectElementParser()
        {
        }

        /// <inheritdoc/>
        protected override VRect ParseInternal(BitInfo bytes)
        {
            var result = new VRect();
            result.top = bytes.ToInt32(0);
            result.left = bytes.ToInt32(4);
            result.bottom = bytes.ToInt32(8);
            result.right = bytes.ToInt32(12);
            return result;
        }
    }
}
