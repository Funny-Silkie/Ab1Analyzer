namespace Ab1Analyzer.ElementParsers
{
    /// <summary>
    /// <see cref="ElementTypeCode.EL_Double"/>に対応する<see cref="ElementParser{T}"/>です。
    /// </summary>
    internal class DoubleElementParser : ElementParser<double>
    {
        /// <inheritdoc/>
        public override short ElementSize => 8;

        /// <inheritdoc/>
        public override ElementTypeCode SupportTypeCode => ElementTypeCode.EL_Double;

        /// <summary>
        /// <see cref="DoubleElementParser"/>の新しいインスタンスを初期化します。
        /// </summary>
        public DoubleElementParser()
        {
        }

        /// <inheritdoc/>
        protected override double ParseInternal(BitInfo bytes)
        {
            return bytes.ToDouble();
        }
    }
}
