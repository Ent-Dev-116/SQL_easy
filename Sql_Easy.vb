Public class Sql
    ' このクラスは、SQL文を簡単に実行するための物です。
    ' 使い方は、Sqlクラスのインスタンスを作成し、Executeメソッドを呼び出すだけです。 
    
    ' 初期化は接続プロパティを設定するだけでOKです。
    ' また、本文には列の配列を用意しておいてください。引数として使用します。

    ' 例1:DBへ入力する場合
    ' sql.Set("INSERT INTO table_name (column1, column2) VALUES (value1, value2)")
    ' 例2:DBから取得する場合
    ' sql.Get("SELECT * FROM table_name",Columns（列を指定する配列）)

    ' 接続用の文字列
    Data_Source As String;
    User_ID As String;
    Password As String;
    Inital_Catalog As String;

    Sub New(String As Sourse,
            String As id,
            String As Password,
            String As Database)
        this.Data_Source = Sourse;
        this.User_ID = id;
        this.Password = Password;
        this.Inital_Catalog = Database;
    End Sub

    ' SQL文を実行するメソッド(set)
    Sub Set(String As Sql)
        Dim cn As New SqlClient.SqlConnection
        cn.ConnectionString = "Data Source=" & this.Data_Source & ";Initial Catalog=" & this.Inital_Catalog & ";User ID=" & this.User_ID & ";Password=" & this.Password & ";"
        
        cn.Open()

        Dim cmd As New SqlClient.SqlCommand
        cmd.CommandText = Sql
        cmd.Connection = cn
        cmd.ExecuteNonQuery()

        cn.Close()
        cn.Dispose()
    End Sub

    ' SQL文を実行するメソッド(get)
    Sub Get(String As Sql,
            String() As Columns)
        Dim cn As New SqlClient.SqlConnection
        cn.ConnectionString = "Data Source=" & this.Data_Source & ";Initial Catalog=" & this.Inital_Catalog & ";User ID=" & this.User_ID & ";Password=" & this.Password & ";"
        
        cn.Open()

        Dim cmd As New SqlClient.SqlCommand
        cmd.CommandText = Sql
        cmd.Connection = cn

        Dim dr As SqlClient.SqlDataReader = cmd.ExecuteReader()

        Dim dtData As New DataTable
        For i As Integer = 0 To Columns.Length - 1
            dtData.Columns.Add(Columns(i)) 
        Next i

        While dr.Read()
            Dim drData As DataRow = dtData.NewRow()
            For i As Integer = 0 To Columns.Length - 1
                drData(i) = dr(Columns(i))
            Next i
            dtData.Rows.Add(drData)
        End While

        dr.Close()
        cn.Close()
        cn.Dispose()
    End Sub
End class