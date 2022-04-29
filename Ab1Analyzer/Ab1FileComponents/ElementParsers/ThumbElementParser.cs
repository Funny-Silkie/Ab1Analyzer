using System;

namespace Ab1Analyzer.ElementParsers
{
    /// <summary>
    /// <see cref="ElementTypeCode.EL_Thumb"/>に対応する<see cref="ElementParser{T}"/>です。
    /// </summary>
    internal class ThumbElementParser : ElementParser<ThumbPrint>
    {
        /// <inheritdoc/>
        public override short ElementSize => 10;

        /// <inheritdoc/>
        public override ElementTypeCode SupportTypeCode => ElementTypeCode.EL_Thumb;

        /// <summary>
        /// <see cref="ThumbElementParser"/>の新しいインスタンスを初期化します。
        /// </summary>
        public ThumbElementParser()
        {
        }

        /// <inheritdoc/>
        protected override ThumbPrint ParseInternal(byte[] binary)
        {
            var result = new ThumbPrint();
            result.d = BitConverter.ToInt32(binary, 0);
            result.u = BitConverter.ToInt32(binary, 4);
            result.c = binary[8];
            result.n = binary[9];
            return result;
        }
    }
}
