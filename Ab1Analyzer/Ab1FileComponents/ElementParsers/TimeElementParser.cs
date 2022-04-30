namespace Ab1Analyzer.ElementParsers
{
    /// <summary>
    /// <see cref="ElementTypeCode.EL_Time"/>に対応する<see cref="ElementParser{T}"/>です。
    /// </summary>
    internal class TimeElementParser : ElementParser<Time>
    {
        /// <inheritdoc/>
        public override short ElementSize => 4;

        /// <inheritdoc/>
        public override ElementTypeCode SupportTypeCode => ElementTypeCode.EL_Time;

        /// <summary>
        /// <see cref="TimeElementParser"/>の新しいインスタンスを初期化します。
        /// </summary>
        public TimeElementParser()
        {
        }

        /// <inheritdoc/>
        protected override Time ParseInternal(BitInfo bytes)
        {
            var result = new Time();
            result.hour = bytes.ToByte(0);
            result.minute = bytes.ToByte(1);
            result.second = bytes.ToByte(2);
            result.hsecond = bytes.ToByte(3);
            return result;
        }
    }
}
