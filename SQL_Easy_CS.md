# SQL_Easy.cs

## 事前に
「NuGetパッケージの管理」からみて、「System.Data.SqlClient」が「4.8.6」になってたらOK。最新版だと多分使えないよ。

## 使い方

 1. クラスを用意する。
 2. クラスに「Sql_Easy.cs」のコードをコピペする。
 3. 次の「構文」にある初期化構文を入力する
 4. 準備は終了。あとは使い方次第で構文を使う。

## 構文

### 初期化構文
初期化構文は接続と関数使用に必要なので、必須な文章です。最初にこの変数に定義してから使用してね。要するに、頭に書けって事。
#### 構文例
###### ※このMDファイル内では「samp」を名前にしてますが、任意の名前でいいです！<br> ※下記のダブルクォーテーションの中に記述されているものは例というかダミーです。<br>　ダブルクォーテーション内は変えて使用してね！
```test.cs
    Sql_Easy samp = New Sql_Easy("Source", "UserID", "password", "DataBaseName");
   ```

### 実態構文

実は、VB.NETとはちがいこちらのクラス、すべてのコマンドが集約されています。（わー使いづらーい。）
では説明します。
#### とりま構文から
```test.cs
    Sql_Easy samp = New Sql_Easy("Source", "UserID", "password", "DataBaseName");
    String[] Column = New String(){"id","name","no"};
    samp.SqlCmd("SELECT","testTbl",Column);
 ```
 
 簡単かも…？と思ったそこのあなた。元のコードを見てください。
 ```test.cs
    public DataTable? SqlCmd
        (
        string sqlType,
        string TableName,
        List<string> Column,
        string eMessage = "default",
        List<string>? newValue = default,
        List<string>? whereColumn = default,
        List<string>? whereValue = default
        )
   ```
となっています。わー。
順番に説明します。
#### 引数１：sqlType
##### \[必須、文字列、決まった形\]
これは「INSERT」「UPDATE」「SELECT」のどれですかー？って意味。
これ以外の値だとnullが返ってきます。
#### 引数２：TableName
##### \[必須、文字列\]
読んで字のごとくテーブル名。わかりやすいね。
#### 引数３：Column
##### \[必須、文字列、リスト\]
これは例えば、「INSERT INTO TestTbl(id,name,no) VALUES (3,"Kodama",21)」とするときの「id,name,no」の列指定。
同様に「SELECT」の時の「SELECT id,name,no From TestTbl」とかもそう。
#### 引数４：eMessage = "default"
##### \[省略可能、文字列\]
これはエラーメッセージを表示するときにカスタムできるよってもの。
省略可能で、省略するとエラーが読みづらくなる。
#### 引数５：newValue = dafault(null)
##### \[省略可能、オブジェクト型、リスト\]
これは「INSERT」や「UPDATE」で使う。
変更後の新しくなった値を入れると使える。というか、値を入れないとバグる。
#### 引数６：whereColumn = default(null)
##### \[省略可能、文字列、リスト\]
もし「WHERE」文を使うなら必要になってくる。「WHERE」の際に使用する列を指定できる。
#### 引数７：whereValue = default(null)
##### \[省略可能、オブジェクト型、リスト\]
ここに「WHERE」の条件を入力する。つまり、「whereColumn[0]がwhereValue[0]の値を抽出」とか、そういう意味になる。

## 難しいよ！がんば！
でも使えばなれるよ、たぶん。ちなみに、名前付き変数ゆえに
```test.cs
    Sql_Easy samp = New Sql_Easy("Source", "UserID", "password", "DataBaseName");
    String[] Column = New String(){"id","name","no"};
    Object[] NewValue = New Object(){3,"Kodama",21};
    samp.SqlCmd("INSERT","testTbl",Column,newValue:NewValue);
 ```
 といった風に、「引数名:値」とするとその引数に値を入れられるよ。めちゃべんりー！

おしまい
