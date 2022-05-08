# Ab1Analyzer Manual

コマンドは以下のものが実装されています。各コマンドにて，ヘルプオプション（ `-h` または `--help` ）を使用してヘルプを表示することも可能です。

## コマンド
- [help](#help)
- [load](#load)
- [unload](#unload)
- [show](#show)
- [export](#export)
- [exit]()

## help
```
help
```

ヘルプを表示します。

## load
```
load <ABIF file name>
```

ABIFファイル（`.abi`, `.ab1`, `.fsa`）を読み込みます。  
※ファイルを読み込んでいる状態でないと実行できないコマンドが多く存在します。

### Arguments
#### ABIF file name
読み込むファイル名。

## unload
```
unload
```

[load](#load)にて読み込んだファイルをアンロードします。

## show
```
show <Element name>
```

### Arguments
#### Element name
表示する要素名。

### Options
#### `-a, --all`
全ての要素を表示します。

## export
```
export <Export type> <File path>
```

### Arguments
#### Export type
エクスポートするものを表します。以下から選択する形になります。

- raw  
生データ (csv形式)
- analyzed  
整形済みデータ (csv形式)
- meta  
各要素のメタデータ (csv形式)　※バイナリ解析者向け
- property  
各要素の値 (json形式)　※バイナリ解析者向け
- fasta  
決定された配列 (fasta形式)

#### File path
書き出し先のファイルパス

## exit
```
exit
```

アプリケーションを終了します。