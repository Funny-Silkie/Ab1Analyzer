using System;

namespace Ab1Analyzer.ElementParsers
{
    /// <summary>
    /// <see cref="ElementTypeCode.EL_Long"/>に対応する<see cref="ElementParser{T}"/>です。
    /// </summary>
    internal class LongElementParser : ElementParser<int>
    {
        /// <inheritdoc/>
        public override short ElementSize => 4;

        /// <inheritdoc/>
        public override ElementTypeCode SupportTypeCode => ElementTypeCode.EL_Long;

        /// <summary>
        /// <see cref="LongElementParser"/>の新しいインスタンスを初期化します。
        /// </summary>
        public LongElementParser()
        {
        }

        /// <inheritdoc/>
        protected override int ParseInternal(byte[] binary)
        {
            return BitConverter.ToInt32(binary);
        }
    }
}
