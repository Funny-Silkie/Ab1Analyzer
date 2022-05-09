# ElementTypeCode一覧

|   値 |              型               |                                            C#内の型                                             | サイズ | 備考                                                   |
| ---: | :---------------------------: | :---------------------------------------------------------------------------------------------: | -----: | :----------------------------------------------------- |
|    1 |       8bit 符号なし整数       |                                              byte                                               |      1 |                                                        |
|    2 | 8bit ASCII文字 / 符号あり整数 |                                              sbyte                                              |      1 | DNA配列データは文字列ではなく `sbyte` の配列として格納 |
|    3 |      16bit 符号なし整数       |                                             ushort                                              |      2 |                                                        |
|    4 |      16bit 符号あり整数       |                                              short                                              |      2 |                                                        |
|    5 |      32bit 符号あり整数       |                                               int                                               |      4 |                                                        |
|    6 |  32bit符号あり整数同士の除算  | [IntegerFraction](../../src/Ab1Analyzer.Lib/Ab1FileComponents/ElementValues/IntegerFraction.cs) |      8 |                                                        |
|    7 |      32bit 浮動小数点数       |                                              float                                              |      4 |                                                        |
|    8 |      64bit 浮動小数点数       |                                             double                                              |      8 |                                                        |
|    9 |  Binary-coded decimal value   |                                                                                                 |      ? | 未知の値。復元不可能                                   |
|   10 |             日付              |            [Date](../../src/Ab1Analyzer.Lib/Ab1FileComponents/ElementValues/Date.cs)            |      4 |                                                        |
|   11 |             時間              |            [Time](../../src/Ab1Analyzer.Lib/Ab1FileComponents/ElementValues/Time.cs)            |      4 |                                                        |
|   12 |          thumbprint           |      [ThumbPrint](../../src/Ab1Analyzer.Lib/Ab1FileComponents/ElementValues/ThumbPrint.cs)      |     10 | ローカルコンピュータ内で発行されるユニークID           |
|   13 |            真偽値             |                                              bool                                               |      1 | 0で `false` ，それ以外で `true`                        |
|   14 | 16bit 符号あり整数による座標  |           [Point](../../src/Ab1Analyzer.Lib/Ab1FileComponents/ElementValues/Point.cs)           |      4 |                                                        |
|   15 | 16bit 符号あり整数による矩形  |            [Rect](../../src/Ab1Analyzer.Lib/Ab1FileComponents/ElementValues/Rect.cs)            |      8 |                                                        |
|   16 | 32bit 符号あり整数による座標  |          [VPoint](../../src/Ab1Analyzer.Lib/Ab1FileComponents/ElementValues/VPoint.cs)          |      8 |                                                        |
|   17 | 32bit 符号あり整数による矩形  |           [VRect](../../src/Ab1Analyzer.Lib/Ab1FileComponents/ElementValues/VRect.cs)           |     16 |                                                        |
|   18 |        パスカル文字列         |                                             string                                              |      1 | 最初の1byteが文字数，以降が文字列                      |
|   19 |     null終端バイト文字列      |                                             string                                              |      1 | 最後の1byteがnull文字 ( `/0` )                         |
|   20 |          要素のタグ           |             [Tag](../../src/Ab1Analyzer.Lib/Ab1FileComponents/ElementValues/Tag.cs)             |      8 |                                                        |
|  128 |          圧縮データ           |                                                                                                 |      ? | 復元不可能                                             |
|  256 |          圧縮データ           |                                                                                                 |      ? | 復元不可能                                             |
|  384 |          圧縮データ           |                                                                                                 |      ? | 復元不可能                                             |
| 1023 |           ヘッダー            |      [Ab1DirectoryEntry](../../src/Ab1Analyzer.Lib/Ab1FileComponents/Ab1DirectoryEntry.cs)      |     28 | ファイル全体のヘッダーでのみ登場                       |
| 1024 |         ユーザー定義          |                                                                                                 |      ? | 復元不可能。1024以上の値が該当                         |
