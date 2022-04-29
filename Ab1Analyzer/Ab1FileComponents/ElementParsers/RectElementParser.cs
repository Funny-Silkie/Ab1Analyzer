using System;

namespace Ab1Analyzer.ElementParsers
{
    /// <summary>
    /// <see cref="ElementTypeCode.EL_Rect"/>に対応する<see cref="ElementParser{T}"/>です。
    /// </summary>
    internal class RectElementParser : ElementParser<Rect>
    {
        /// <inheritdoc/>
        public override short ElementSize => 8;

        /// <inheritdoc/>
        public override ElementTypeCode SupportTypeCode => ElementTypeCode.EL_Rect;

        /// <summary>
        /// <see cref="RectElementParser"/>の新しいインスタンスを初期化します。
        /// </summary>
        public RectElementParser()
        {
        }

        /// <inheritdoc/>
        protected override Rect ParseInternal(byte[] binary)
        {
            var result = new Rect();
            result.top = BitConverter.ToInt16(binary, 0);
            result.left = BitConverter.ToInt16(binary, 2);
            result.bottom = BitConverter.ToInt16(binary, 4);
            result.right = BitConverter.ToInt16(binary, 6);
            return result;
        }
    }
}
