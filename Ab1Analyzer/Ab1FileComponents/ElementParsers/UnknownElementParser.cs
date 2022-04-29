namespace Ab1Analyzer.ElementParsers
{
    /// <summary>
    /// 未知の値を変換する<see cref="ElementParser{T}"/>です。
    /// </summary>
    internal class UnknownElementParser : ElementParser<byte[]>
    {
        /// <inheritdoc/>
        public override short ElementSize => -1;

        /// <inheritdoc/>
        public override ElementTypeCode SupportTypeCode => ElementTypeCode.UNKNOWN;

        /// <summary>
        /// <see cref="UnknownElementParser"/>の新しいインスタンスを初期化します。
        /// </summary>
        public UnknownElementParser()
        {
        }

        /// <inheritdoc/>
        protected override byte[] ParseInternal(byte[] binary)
        {
            return binary;
        }
    }
}
