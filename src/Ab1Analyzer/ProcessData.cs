namespace Ab1Analyzer
{
    /// <summary>
    /// /プログラム内データのクラスです。
    /// </summary>
    public class ProcessData
    {
        /// <summary>
        /// 読み込んでいるファイルのパスを取得または設定します。
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 読み込んでいるABIFファイルのデータを取得または設定します。
        /// </summary>
        public Ab1Data Data { get; set; }

        /// <summary>
        /// ABIFデータのラッパークラスを取得または設定します。
        /// </summary>
        public Ab1Wrapper Wrapper { get; set; }

        /// <summary>
        /// プログラムを終了するかどうかを表す値を取得または設定します。
        /// </summary>
        public bool Exit { get; set; }

        /// <summary>
        /// <see cref="ProcessData"/>の新しいインスタンスを初期化します。
        /// </summary>
        public ProcessData()
        {
        }
    }
}
