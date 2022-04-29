using System.Text;

namespace Ab1Analyzer.ElementParsers
{
    /// <summary>
    /// <see cref="ElementTypeCode.EL_CString"/>に対応する<see cref="ElementParser{T}"/>です。
    /// </summary>
    internal class CStringElementParser : ElementParser<string>
    {
        private const char NULL_CHARACTER = (char)0;

        /// <inheritdoc/>
        public override short ElementSize => -1;

        /// <inheritdoc/>
        public override ElementTypeCode SupportTypeCode => ElementTypeCode.EL_CString;

        /// <summary>
        /// <see cref="CStringElementParser"/>の新しいインスタンスを初期化します。
        /// </summary>
        public CStringElementParser()
        {
        }

        /// <inheritdoc/>
        protected override string ParseInternal(byte[] binary)
        {
            string value = Encoding.ASCII.GetString(binary);
            int length = value.IndexOf(NULL_CHARACTER);
            return value.Substring(0, length);
        }
    }
}
