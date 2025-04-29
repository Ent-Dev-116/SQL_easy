# SQL_Easy.vb

## 使い方

 1. クラスを用意する。
 2. クラスに「Sql_Easy.vb」のコードをコピペする。
 3. 次の「構文」にある初期化構文を入力する
 4. 準備は終了。あとは使い方次第で構文を使う。

## 構文

### 初期化構文
初期化構文は接続と関数使用に必要なので、必須な文章です。最初にこの変数に定義してから使用してね。要するに、頭に書けって事。
#### 構文例
###### ※このMDファイル内では「samp」を名前にしてますが、任意の名前でいいです！<br> ※下記のダブルクォーテーションの中に記述されているものは例というかダミーです。<br>　ダブルクォーテーション内は変えて使用してね！
```test.vb
    Dim samp As New SqlEasy("Source", "UserID", "password", "DataBaseName");
   ```

### データベース干渉構文
「INSERT」「UPDATE」などのデータベースに干渉して結果はどうでもいいタイプの構文は「SetDBを用いれば使用可能です。」

**関数名：** SetDB
**引数　：** String\[Sql\]（SQL文）
**返り値：** Boolen（成功、失敗）
#### 構文例(\[-\-\]で囲まれているものが説明。それ以外が必須。)
```test.vb
    samp.SetDB( "--SQL文--" )
   ```
#### 使用例
##### ※sampはそのまま使ってるだけで、自分で定義したのを使ってね！<br>※SetDB内のテキストを自分の\[INSERT\]\[UPDATE\]のコードに変えると動くようになってるよ！
```test.vb
    Dim samp As New SqlEasy("Source", "UserID", "password", "DataBaseName");
    samp.SetDB("INSERT INTO TestTbl(Name,Pow,Rarity) VALUES ('saikyousan1gou',100,'UR')")
   ```

### データベース抽出文
「SELECT」のみ。この関数は返り値を持ち、SELECTに特化している。

**関数名：** GetDB
**引数　：** String\[Sql\]（SQL文）、String()\[Columns\]（列名配列）
**返り値：** DataTable（成功で抽出したテーブル、失敗で**Nothing**）
#### 構文例(\[-\-\]で囲まれているものが説明。それ以外は必須。)
##### ※構文の中にある「・・・」は「複数個あるよー」って意味です。
```test.vb
    samp.GetDB( "--SQL文--", New string(){"-- 列名１--","--列名2--",・・・,"列名n"} )
   ```
#### 使用例
##### ※sampはそのまま使ってるだけで、自分で定義したのを使ってね！<br>※GetDB内のテキストを自分の\[SELECT\]のコードに変えると動くようになってるよ！
```test.vb
    Dim samp As New SqlEasy("Source", "UserID", "password", "DataBaseName");
    samp.GetDB("SELECT column1, column2 FROM testTbl",New String(){"column1","column2"})
   ```

## ちょっとした応用

### エラーメッセージ自作
このコードでは、エラーメッセージがコードで「わー！！」ってでてきます。わかりづらい！
そこで、このプログラムには「オーバーロード」が施されています。
**最後の引数にエラーメッセージを入れる**と、そのエラーメッセージが表示されるようになります！（かんたん！）
#### 使用例
```test.vb
    samp.SetDB( "--SQL文--","--表示したいエラーメッセージ--" )
   ```
ね？簡単でしょう？
ちなみにGetDBでも同じようにできるよ！

おしまい
