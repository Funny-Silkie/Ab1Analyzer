namespace Ab1Analyzer.ElementParsers
{
    /// <summary>
    /// <see cref="ElementTypeCode.EL_Rational"/>に対応する<see cref="ElementParser{T}"/>です。
    /// </summary>
    internal class RationalElementParser : ElementParser<IntegerFraction>
    {
        /// <inheritdoc/>
        public override short ElementSize => 8;

        /// <inheritdoc/>
        public override ElementTypeCode SupportTypeCode => ElementTypeCode.EL_Rational;

        /// <summary>
        /// <see cref="RationalElementParser"/>の新しいインスタンスを初期化します。
        /// </summary>
        public RationalElementParser()
        {
        }

        /// <inheritdoc/>
        protected override IntegerFraction ParseInternal(BitInfo bytes)
        {
            int numerator = bytes.ToInt32(0);
            int denominator = bytes.ToInt32(4);
            var result = new IntegerFraction(numerator, denominator);
            return result;
        }
    }
}
