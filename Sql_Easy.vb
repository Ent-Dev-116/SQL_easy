Public class SqlEasy
    ' SqlEasyクラスは、SQL Serverに接続してSQL文を実行するためのクラスです。
    ' 使い方は、Sqlクラスのインスタンスを作成し、Executeメソッドを呼び出すだけです。 
    
    ' 初期化は接続プロパティを設定するだけでOKです。
    ' また、本文には列の配列を用意しておいてください。引数として使用します。

    ' 例1:DBへ入力する場合
    ' sql.Set("INSERT INTO table_name (column1, column2) VALUES (value1, value2)")
    ' 例2:DBから取得する場合
    ' Dim dt As DataTable = sql.Get("SELECT column1, column2 FROM table_name", New String() {"column1", "column2"})

    ' 接続用の文字列
    Data_Source As String;
    User_ID As String;
    Password As String;
    Inital_Catalog As String;

    Sub New(String As Sourse,
            String As id,
            String As Password,
            String As Database)
        Me.Data_Source = Sourse;
        Me.User_ID = id;
        Me.Password = Password;
        Me.Inital_Catalog = Database;
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
    Function Get(String As Sql,
            String() As Columns) As DataTable
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

        Return dtData
    End Function

End class
