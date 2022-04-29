namespace Ab1Analyzer.ElementParsers
{
    /// <summary>
    /// <see cref="ElementTypeCode.EL_Bool"/>に対応する<see cref="ElementParser{T}"/>です。
    /// </summary>
    internal class BoolElementParser : ElementParser<bool>
    {
        /// <inheritdoc/>
        public override short ElementSize => 1;

        /// <inheritdoc/>
        public override ElementTypeCode SupportTypeCode => ElementTypeCode.EL_Bool;

        /// <summary>
        /// <see cref="BoolElementParser"/>の新しいインスタンスを初期化します。
        /// </summary>
        public BoolElementParser()
        {
        }

        /// <inheritdoc/>
        protected override bool ParseInternal(byte[] binary)
        {
            return binary[0] > 0;
        }
    }
}
