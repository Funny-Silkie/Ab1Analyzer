using System;

namespace Ab1Analyzer.ElementParsers
{
    /// <summary>
    /// <see cref="ElementTypeCode.EL_Short"/>に対応する<see cref="ElementParser{T}"/>です。
    /// </summary>
    internal class ShortElementParser : ElementParser<short>
    {
        /// <inheritdoc/>
        public override short ElementSize => 2;

        /// <inheritdoc/>
        public override ElementTypeCode SupportTypeCode => ElementTypeCode.EL_Short;

        /// <summary>
        /// <see cref="ShortElementParser"/>の新しいインスタンスを初期化します。
        /// </summary>
        public ShortElementParser()
        {
        }

        /// <inheritdoc/>
        protected override short ParseInternal(byte[] binary)
        {
            return BitConverter.ToInt16(binary);
        }
    }
}
