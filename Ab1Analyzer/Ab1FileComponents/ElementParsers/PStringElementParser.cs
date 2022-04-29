using System.Text;

namespace Ab1Analyzer.ElementParsers
{
    /// <summary>
    /// <see cref="ElementTypeCode.EL_PString"/>に対応する<see cref="ElementParser{T}"/>です。
    /// </summary>
    internal class PStringElementParser : ElementParser<string>
    {
        /// <inheritdoc/>
        public override short ElementSize => -1;

        /// <inheritdoc/>
        public override ElementTypeCode SupportTypeCode => ElementTypeCode.EL_PString;

        /// <summary>
        /// <see cref="PStringElementParser"/>の新しいインスタンスを初期化します。
        /// </summary>
        public PStringElementParser()
        {
        }

        /// <inheritdoc/>
        protected override string ParseInternal(byte[] binary)
        {
            byte length = binary[0];
            return Encoding.ASCII.GetString(binary.SubArray(1, length));
        }
    }
}
