using System;

namespace Ab1Analyzer.ElementParsers
{
    /// <summary>
    /// <see cref="ElementTypeCode.EL_Point"/>に対応する<see cref="ElementParser{T}"/>です。
    /// </summary>
    internal class PointElementParser : ElementParser<Point>
    {
        /// <inheritdoc/>
        public override short ElementSize => 4;

        /// <inheritdoc/>
        public override ElementTypeCode SupportTypeCode => ElementTypeCode.EL_Point;

        /// <summary>
        /// <see cref="PointElementParser"/>の新しいインスタンスを初期化します。
        /// </summary>
        public PointElementParser()
        {
        }

        /// <inheritdoc/>
        protected override Point ParseInternal(byte[] binary)
        {
            var result = new Point();
            result.v = BitConverter.ToInt16(binary, 0);
            result.h = BitConverter.ToInt16(binary, 2);
            return result;
        }
    }
}
