using System;

namespace Ab1Analyzer.ElementParsers
{
    /// <summary>
    /// <see cref="ElementTypeCode.EL_VPoint"/>に対応する<see cref="ElementParser{T}"/>です。
    /// </summary>
    internal class VPointElementParser : ElementParser<VPoint>
    {
        /// <inheritdoc/>
        public override short ElementSize => 8;

        /// <inheritdoc/>
        public override ElementTypeCode SupportTypeCode => ElementTypeCode.EL_VPoint;

        /// <summary>
        /// <see cref="VPointElementParser"/>の新しいインスタンスを初期化します。
        /// </summary>
        public VPointElementParser()
        {
        }

        /// <inheritdoc/>
        protected override VPoint ParseInternal(byte[] binary)
        {
            var result = new VPoint();
            result.v = BitConverter.ToInt32(binary, 0);
            result.h = BitConverter.ToInt32(binary, 4);
            return result;
        }
    }
}
