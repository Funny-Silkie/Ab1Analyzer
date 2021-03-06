# ABIFファイルの構造

ABIFファイルはバイナリファイルです。C#における解析は `BinaryReader` と `BitConverter` を主に用います。文字列は全てASCIIでデコードすることになります。

※ `BinaryReader` で読み取ったバイト配列をそのまま `BitConverter` に突っ込むとうまく変換できません。 `BitConverter` に使う際は配列の要素を逆転させる必要があります。

ファイルは主に以下の要素からなります。

 - [ファイル全体のヘッダー](#ファイル全体のヘッダー)
 - [各要素のヘッダー](#各要素のヘッダー)
 - [各要素の値](#各要素の値)

コンテナ名や生データなどのファイル内の各要素は一つずつヘッダーを持っています。ヘッダーには各要素のメタデータが示されています。メタデータ内にはその値があるバイナリファイル上の場所が示されています。

ファイル全体のヘッダーは各要素のヘッダーを値と見做した際のヘッダーとなっています。つまり，ファイル全体のヘッダーは各要素のヘッダーの個数やバイナリファイル上の位置を表しているのです。

### イメージ
```
ファイル全体のヘッダー
├要素1のヘッダー
│└要素1の値
├要素2のヘッダー
│└要素2の値
└要素3のヘッダー
　└要素3の値
```

## ファイル全体のヘッダー

ファイル全体のヘッダーは128バイトの領域からなります。
格納されているデータは以下の通りです。
TagNameからHeader.DataHandleまでの値は [Ab1DirectoryEntry](../../src/Ab1Analyzer.Lib/Ab1FileComponents/Ab1DirectoryEntry.cs)として格納されます。

位置はバイト配列のインデックスを表し，`Stream.Position` に対応します。サイズはバイト配列換算です。

| 位置 | サイズ |         型         |               C#内の型                | 対応するプロパティ  | 説明                                                                                          |
| ---: | -----: | :----------------: | :-----------------------------------: | :-----------------: | :-------------------------------------------------------------------------------------------- |
|    0 |      4 |   文字列 (ASCII)   |                string                 |                     | マジックナンバー。 `ABIF` の4文字                                                             |
|    4 |      2 | 16bit 符号あり整数 |                 short                 |       Version       | ファイルフォーマットのバージョン。 `.ab1` なら 101のはず                                      |
|    6 |      4 |   文字列 (ASCII)   |                string                 |       TagName       | 要素名。 `tdir` の4文字                                                                       |
|   10 |      4 | 32bit 符号あり整数 |                  int                  |      TagNumber      | 要素の番号。値は1                                                                             |
|   14 |      2 | 16bit 符号あり整数 | [ElementTypeCode](ElementTypeCode.md) | Header.ElementType  | 要素の種類を表す。値は1023                                                                    |
|   16 |      2 | 16bit 符号あり整数 |                 short                 | Header.ElementSize  | 要素のサイズ (bytes)。値は28                                                                  |
|   18 |      4 | 32bit 符号あり整数 |                  int                  | Header.ElementCount | 要素の個数                                                                                    |
|   22 |      4 | 32bit 符号あり整数 |                  int                  |   Header.DataSize   | `ElementSize` と `ElementCount` を加味した際の，データ全体のサイズを表します                  |
|   26 |      4 | 32bit 符号あり整数 |                  int                  |  Header.DataOffset  | `DataSize` が4以下のときは要素の値そのものを，4を超える場合は値が格納されている場所を表します |
|   30 |      4 | 32bit 符号あり整数 |                  int                  |  Header.DataHandle  | 空の領域。値は0                                                                               |
|   34 |     94 |                    |                                       |                     | 残りの空の領域。使用しない                                                                    |

## 各要素のヘッダー
各要素のヘッダーも[Ab1DirectoryEntry](../../src/Ab1Analyzer.Lib/Ab1FileComponents/Ab1DirectoryEntry.cs)として格納されます。

データ構成は以下の通りです。

| 位置 | サイズ |         型         |               C#内の型                | 対応するプロパティ | 説明                                                                                          |
| ---: | -----: | :----------------: | :-----------------------------------: | :----------------: | :-------------------------------------------------------------------------------------------- |
|    0 |      4 |   文字列 (ASCII)   |                string                 |      TagName       | 4文字の要素名                                                                                 |
|    4 |      4 | 32bit 符号あり整数 |                  int                  |     TagNumber      | 要素の番号。1から始まる                                                                       |
|    8 |      2 | 16bit 符号あり整数 | [ElementTypeCode](ElementTypeCode.md) |    ElementType     | 要素の種類を表す                                                                              |
|   10 |      2 | 16bit 符号あり整数 |                 short                 |    ElementSize     | 要素のサイズ (bytes)。                                                                        |
|   12 |      4 | 32bit 符号あり整数 |                  int                  |    ElementCount    | 要素の個数                                                                                    |
|   16 |      4 | 32bit 符号あり整数 |                  int                  |      DataSize      | `ElementSize` と `ElementCount` を加味した際の，データ全体のサイズを表します                  |
|   20 |      4 | 32bit 符号あり整数 |                  int                  |     DataOffset     | `DataSize` が4以下のときは要素の値そのものを，4を超える場合は値が格納されている場所を表します |
|   24 |      4 | 32bit 符号あり整数 |                  int                  |     DataHandle     | 空の領域。値は0                                                                               |

## 各要素の値

各要素の値はヘッダーの `DataOffset` そのもの或いはその指定する領域によって示されます。
ライブラリ内では `Ab1Directory.Elements` 内に格納されています。 `ElementType` が `User` や `UNKNOWN` などの復元不能な値を取る場合はバイナリファイルが読み取ったデータそのものがバイト配列として格納されています。

## 参考資料

- [東邦大学 ノート/シーケンサ出力の解読](https://pepper.is.sci.toho-u.ac.jp/pepper/index.php?%A5%CE%A1%BC%A5%C8%2F%A5%B7%A1%BC%A5%B1%A5%F3%A5%B5%BD%D0%CE%CF%A4%CE%B2%F2%C6%C9)
- [Applied Biosystems Genetic Analysis Data File Format](https://projects.nfstc.org/workshops/resources/articles/ABIF_File_Format.pdf)

