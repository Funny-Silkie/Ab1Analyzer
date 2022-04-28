using System;

namespace Ab1Analyzer
{
    /// <summary>
    /// データの種類を表します。
    /// </summary>
    [Serializable]
    public enum ElementTypeCode
    {
        /// <summary>
        /// 未知の値。エラー
        /// </summary>
        UNKNOWN = 0,

        /// <summary>
        /// 8bit符号無し整数
        /// ElementSize: 1byte
        /// </summary>
        EL_Byte = 1,

        /// <summary>
        /// 8bit ASCII文字 -or- 8bit符号あり整数
        /// ElementSize: 1byte
        /// </summary>
        EL_Char = 2,

        /// <summary>
        /// 16bit符号なし整数
        /// ElementSize: 2byte
        /// </summary>
        EL_Word = 3,

        /// <summary>
        /// 16bit符号あり整数
        /// ElementSize: 2byte
        /// </summary>
        EL_Short = 4,

        /// <summary>
        /// 32bit符号あり整数
        /// ElementSize: 4byte
        /// </summary>
        EL_Long = 5,

        /// <summary>
        /// 32bit浮動小数点数
        /// ElementSize: 4byte
        /// </summary>
        EL_Float = 7,

        /// <summary>
        /// 32bit浮動小数点数
        /// ElementSize: 8byte
        /// </summary>
        EL_Double = 8,

        /// <summary>
        /// <see cref="Date"/>
        /// ElementSize: 4byte
        /// </summary>
        EL_Date = 10,

        /// <summary>
        /// <see cref="Time"/>
        /// ElementSize: 4byte
        /// </summary>
        EL_Time = 11,

        /// <summary>
        /// パスカル文字列
        /// ElementSize: 1byte
        /// </summary>
        /// <remarks>
        /// 最初のバイト：文字数(0-255)<br/>
        /// 次以降：8bit ASCII文字<br/>
        /// アイテムの要素数=文字数+1
        /// </remarks>
        EL_PString = 18,

        /// <summary>
        /// C-type(null終端)文字列
        /// ElementSize: byte
        /// </summary>
        /// <remarks>
        /// 8bit ASCII文字列+最後にnull文字<br/>
        /// 要素数=文字数+1
        /// </remarks>
        EL_CString = 19,

        /// <summary>
        /// <see cref="ThumbPrint"/>
        /// ElementSize: 10byte
        /// </summary>
        EL_Thumb = 12,

        /// <summary>
        /// 真偽値
        /// ElementSize: 1byte
        /// </summary>
        /// <remarks>0がfalse，それ以外でtrue</remarks>
        EL_Bool = 13,

        /// <summary>
        /// ユーザー定義の構造体
        /// ElementSize: 1byte
        /// </summary>
        /// <remarks>
        /// 値としては1024以上<br/>
        /// numelements * elementsize != datasizeの場合がある
        /// </remarks>
        EL_User = 1024,

        /// <summary>
        /// <see cref="IntegerFraction"/>
        /// ElementSize: 8byte
        /// </summary>
        EL_Rational = 6,

        /// <summary>
        /// Binary-coded decimal value(未知のフォーマット)
        /// ElementSize: unknown
        /// </summary>
        EL_BCD = 9,

        /// <summary>
        /// <see cref="Point"/>
        /// ElementSize: 4byte
        /// </summary>
        EL_Point = 14,

        /// <summary>
        /// <see cref="Rect"/>
        /// ElementSize: 8byte
        /// </summary>
        EL_Rect = 15,

        /// <summary>
        /// <see cref="VPoint"/>
        /// ElementSize: 8byte
        /// </summary>
        EL_VPoint = 16,

        /// <summary>
        /// <see cref="VRect"/>
        /// ElementSize: 16byte
        /// </summary>
        EL_VRect = 17,

        /// <summary>
        /// <see cref="Tag"/>
        /// ElementSize: 8byte
        /// </summary>
        EL_Tag = 20,

        /// <summary>
        /// 圧縮データ
        /// </summary>
        EL_DeltaComp = 128,

        /// <summary>
        /// 圧縮データ
        /// </summary>
        EL_LZWComp = 256,

        /// <summary>
        /// 圧縮データ
        /// </summary>
        EL_DeltaLZW = 384,

        /// <summary>
        /// ヘッダーのElementTypeCode
        /// </summary>
        Header = 1023,
    }
}