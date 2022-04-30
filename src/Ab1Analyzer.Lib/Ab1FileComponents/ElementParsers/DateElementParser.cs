namespace Ab1Analyzer.ElementParsers
{
    /// <summary>
    /// <see cref="ElementTypeCode.EL_Date"/>に対応する<see cref="ElementParser{T}"/>です。
    /// </summary>
    internal class DateElementParser : ElementParser<Date>
    {
        /// <inheritdoc/>
        public override short ElementSize => 4;

        /// <inheritdoc/>
        public override ElementTypeCode SupportTypeCode => ElementTypeCode.EL_Date;

        /// <summary>
        /// <see cref="DateElementParser"/>の新しいインスタンスを初期化します。
        /// </summary>
        public DateElementParser()
        {
        }

        /// <inheritdoc/>
        protected override Date ParseInternal(BitInfo bytes)
        {
            var result = new Date();
            result.year = bytes.ToInt16(0);
            result.month = bytes.ToByte(2);
            result.day = bytes.ToByte(3);
            return result;
        }
    }
}
