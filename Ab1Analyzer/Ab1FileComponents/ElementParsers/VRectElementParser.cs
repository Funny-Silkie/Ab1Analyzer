using System;

namespace Ab1Analyzer.ElementParsers
{
    /// <summary>
    /// <see cref="ElementTypeCode.EL_VRect"/>に対応する<see cref="ElementParser{T}"/>です。
    /// </summary>
    internal class VRectElementParser : ElementParser<VRect>
    {
        /// <inheritdoc/>
        public override short ElementSize => 16;

        /// <inheritdoc/>
        public override ElementTypeCode SupportTypeCode => ElementTypeCode.EL_VRect;

        /// <summary>
        /// <see cref="VRectElementParser"/>の新しいインスタンスを初期化します。
        /// </summary>
        public VRectElementParser()
        {
        }

        /// <inheritdoc/>
        protected override VRect ParseInternal(byte[] binary)
        {
            var result = new VRect();
            result.top = BitConverter.ToInt32(binary, 0);
            result.left = BitConverter.ToInt32(binary, 4);
            result.bottom = BitConverter.ToInt32(binary, 8);
            result.right = BitConverter.ToInt32(binary, 12);
            return result;
        }
    }
}
