namespace Ab1Analyzer.ElementParsers
{
    /// <summary>
    /// <see cref="ElementTypeCode.EL_Tag"/>に対応する<see cref="ElementParser{T}"/>です。
    /// </summary>
    internal class TagElementParser : ElementParser<Tag>
    {
        /// <inheritdoc/>
        public override short ElementSize => 8;

        /// <inheritdoc/>
        public override ElementTypeCode SupportTypeCode => ElementTypeCode.EL_Tag;

        /// <summary>
        /// <see cref="TagElementParser"/>の新しいインスタンスを初期化します。
        /// </summary>
        public TagElementParser()
        {
        }

        /// <inheritdoc/>
        protected override Tag ParseInternal(BitInfo bytes)
        {
            var result = new Tag();
            result.name = bytes.ToASCIIString(0, 4);
            result.number = bytes.ToInt32(4);
            return result;
        }
    }
}
