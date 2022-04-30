namespace Ab1Analyzer.ElementParsers
{
    /// <summary>
    /// <see cref="ElementTypeCode.EL_Char"/>に対応する<see cref="ElementParser{T}"/>です。
    /// </summary>
    internal class CharElementParser : ElementParser<sbyte>
    {
        /// <inheritdoc/>
        public override short ElementSize => 1;

        /// <inheritdoc/>
        public override ElementTypeCode SupportTypeCode => ElementTypeCode.EL_Char;

        /// <summary>
        /// <see cref="CharElementParser"/>の新しいインスタンスを初期化します。
        /// </summary>
        public CharElementParser()
        {
        }

        /// <inheritdoc/>
        protected override sbyte ParseInternal(BitInfo bytes)
        {
            return bytes.ToSByte();
        }
    }
}
