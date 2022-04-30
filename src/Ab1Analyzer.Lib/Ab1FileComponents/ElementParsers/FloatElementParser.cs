namespace Ab1Analyzer.ElementParsers
{
    /// <summary>
    /// <see cref="ElementTypeCode.EL_Float"/>に対応する<see cref="ElementParser{T}"/>です。
    /// </summary>
    internal class FloatElementParser : ElementParser<float>
    {
        /// <inheritdoc/>
        public override short ElementSize => 4;

        /// <inheritdoc/>
        public override ElementTypeCode SupportTypeCode => ElementTypeCode.EL_Float;

        /// <summary>
        /// <see cref="FloatElementParser"/>の新しいインスタンスを初期化します。
        /// </summary>
        public FloatElementParser()
        {
        }

        /// <inheritdoc/>
        protected override float ParseInternal(BitInfo bytes)
        {
            return bytes.ToSingle();
        }
    }
}
