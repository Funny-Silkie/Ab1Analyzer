using System;

namespace Ab1Analyzer.ElementParsers
{
    /// <summary>
    /// <see cref="ElementTypeCode.EL_Word"/>に対応する<see cref="ElementParser{T}"/>です。
    /// </summary>
    internal class WordElementParser : ElementParser<ushort>
    {
        /// <inheritdoc/>
        public override short ElementSize => 2;

        /// <inheritdoc/>
        public override ElementTypeCode SupportTypeCode => ElementTypeCode.EL_Word;

        /// <summary>
        /// <see cref="WordElementParser"/>の新しいインスタンスを初期化します。
        /// </summary>
        public WordElementParser()
        {
        }

        /// <inheritdoc/>
        protected override ushort ParseInternal(byte[] binary)
        {
            return BitConverter.ToUInt16(binary);
        }
    }
}
