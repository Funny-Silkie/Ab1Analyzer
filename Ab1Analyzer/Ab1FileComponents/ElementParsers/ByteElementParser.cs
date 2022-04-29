using System;

namespace Ab1Analyzer.ElementParsers
{
    /// <summary>
    /// <see cref="ElementTypeCode.EL_Byte"/>に対応する<see cref="ElementParser{T}"/>です。
    /// </summary>
    internal class ByteElementParser : ElementParser<byte>
    {
        /// <inheritdoc/>
        public override short ElementSize => 1;

        /// <inheritdoc/>
        public override ElementTypeCode SupportTypeCode => ElementTypeCode.EL_Byte;

        /// <summary>
        /// <see cref="ByteElementParser"/>の新しいインスタンスを初期化します。
        /// </summary>
        public ByteElementParser()
        {
        }

        /// <inheritdoc/>
        protected override byte ParseInternal(byte[] binary)
        {
            return binary[0];
        }
    }
}
