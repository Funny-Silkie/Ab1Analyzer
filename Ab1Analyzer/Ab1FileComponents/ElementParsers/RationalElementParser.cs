using System;

namespace Ab1Analyzer.ElementParsers
{
    /// <summary>
    /// <see cref="ElementTypeCode.EL_Rational"/>に対応する<see cref="ElementParser{T}"/>です。
    /// </summary>
    internal class RationalElementParser : ElementParser<IntegerFraction>
    {
        /// <inheritdoc/>
        public override short ElementSize => 8;

        /// <inheritdoc/>
        public override ElementTypeCode SupportTypeCode => ElementTypeCode.EL_Rational;

        /// <summary>
        /// <see cref="RationalElementParser"/>の新しいインスタンスを初期化します。
        /// </summary>
        public RationalElementParser()
        {
        }

        /// <inheritdoc/>
        protected override IntegerFraction ParseInternal(byte[] binary)
        {
            var result = new IntegerFraction();
            result.numerator = BitConverter.ToInt32(binary, 0);
            result.denominator = BitConverter.ToInt32(binary, 4);
            return result;
        }
    }
}
