# Ab1Analyzer.Lib Manual

ABIFファイル（`.abi`, `.ab1`, `.fsa`）の解析を行うライブラリです。

## 使用例
```cs
using Ab1Analyzer;
using System;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read abi file
            Ab1Data abi = Ab1Data.Create("hogehoge.ab1");
            var wrapper = new Ab1Wrapper(abi);
            
            // Export into fasta file
            wrapper.ExportFasta("sequence.fasta");

            // Get Sequence
            DNASequence forward = wrapper.Sequence;
            DNASequence complement = Complement(forward);

            // Output sequence
            Console.WriteLine("Forward Sequence");
            Console.WriteLine(forward);
            
            Console.WriteLine("Complement Sequence");
            Console.WriteLine(complement);
        }

        // Create complement sequence
        static DNASequence Complement(DNASequence seq)
        {
            var ret = new DNASequence();
            foreach (DNABase b in seq)
            {
                char character = b.Value;
                switch (character)
                {
                    case 'A':
                        ret += DNABase.T;
                        break;
                    case 'T':
                        ret += DNABase.A;
                        break;
                    case 'G':
                        ret += DNABase.C;
                        break;
                    case 'C':
                        ret += DNABase.G;
                        break;
                }
            }
            return ret;
        }
    }
}

```

`Ab1Data.Create(string)` を用いてファイルを読み込み， `Ab1Wrapper` を使用して配列や解析データを取得するのがセオリーです。

`An1Data.Data` 内にファイル内の各要素（生データや解析データ，コンテナ名など）が格納されており，要素名（4文字の英数字記号からなる）と番号を用いて各要素にアクセスすることができます。

例えば，Aの生データは要素名が `DATA` ，番号が `1` であるので，

```cs
Ab1Data abi = Ab1Data.Create("hogehoge.ab1");
object[] _a_raw = abi.Data["DATA", 1].Elements;
short[] a_raw = Array.ConvertAll(_a_raw, x => (short)x);
```

のようにして取得することができます。 `abi.Data["DATA", 1]` にて取得出来るのが `Ab1Directory` と言うオブジェクトで，これの `Elements` プロパティがデータに該当します。 `Elements` プロパティは `object[]` であるため， `Array.ConvertAll(Array, Converter<TIn, TOut>)` 等の方法で変換することが推奨されます。

要素の型は `Ab1Directory.ElementType` にて取得することができます。

型情報については以下のドキュメント（外部サイト）が参考になります。

- [東邦大学 ノート/シーケンサ出力の解読](https://pepper.is.sci.toho-u.ac.jp/pepper/index.php?%A5%CE%A1%BC%A5%C8%2F%A5%B7%A1%BC%A5%B1%A5%F3%A5%B5%BD%D0%CE%CF%A4%CE%B2%F2%C6%C9)
- [Applied Biosystems Genetic Analysis Data File Format](https://projects.nfstc.org/workshops/resources/articles/ABIF_File_Format.pdf)

