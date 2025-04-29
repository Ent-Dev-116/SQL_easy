Public Class SqlEasy
    ' SqlEasyクラスは、SQL Serverに接続してSQL文を実行するためのクラスです。
    ' 使い方は、Sqlクラスのインスタンスを作成し、Executeメソッドを呼び出すだけです。 

    ' 初期化は接続プロパティを設定するだけでOKです。
    ' また、本文には列の配列を用意しておいてください。引数として使用します。

    ' 例1:DBへ入力する場合
    ' sql.SetDB("INSERT INTO table_name (column1, column2) VALUES (value1, value2)")
    ' 例2:DBから取得する場合
    ' Dim dt As DataTable = sql.GetDB("SELECT column1, column2 FROM table_name", New String() {"column1", "column2"})

    '＜ちょっとした小ネタ＞このまま使用できますが、この関数の末尾に文字列としてエラーコードを自分で記述すると、失敗時にその文章を表示します。
    
    ' 接続用の文字列
    Private Data_Source As String
    Private User_ID As String
    Private Password As String
    Private Inital_Catalog As String

    Sub New(Sourse As String,
            ID As String,
            Password As String,
            Database As String)
        Me.Data_Source = Sourse
        Me.User_ID = ID
        Me.Password = Password
        Me.Inital_Catalog = Database
    End Sub

    ' SQL文を実行するメソッド(set)
    Public Function SetDB(Sql As String) As Boolean
        Try
            Dim cn As New Microsoft.Data.SqlClient.SqlConnection
            cn.ConnectionString = "Data Source=" & Me.Data_Source & ";Initial Catalog=" & Me.Inital_Catalog & ";User ID=" & Me.User_ID & ";Password=" & Me.Password & ";"

            cn.Open()

            Dim cmd As New Microsoft.Data.SqlClient.SqlCommand
            cmd.CommandText = Sql
            cmd.Connection = cn
            cmd.ExecuteNonQuery()

            cn.Close()
            cn.Dispose()
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function

    ' SQL文を実行するメソッド(get)
    Public Function GetDB(Sql As String,
            Columns As String()) As DataTable
        Try

            Dim cn As New Microsoft.Data.SqlClient.SqlConnection
            cn.ConnectionString = "Data Source=" & Me.Data_Source & ";Initial Catalog=" & Me.Inital_Catalog & ";User ID=" & Me.User_ID & ";Password=" & Me.Password & ";"

            cn.Open()

            Dim cmd As New Microsoft.Data.SqlClient.SqlCommand
            cmd.CommandText = Sql
            cmd.Connection = cn

            Dim dr As Microsoft.Data.SqlClient.SqlDataReader = cmd.ExecuteReader()

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
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function

    Function SetDB(Sql As String, ErrorText As String) As Boolean
        Try
            Dim cn As New Microsoft.Data.SqlClient.SqlConnection
            cn.ConnectionString = "Data Source=" & Me.Data_Source & " Initial Catalog=" & Me.Inital_Catalog & " User ID=" & Me.User_ID & " Password=" & Me.Password & " "

            cn.Open()

            Dim cmd As New Microsoft.Data.SqlClient.SqlCommand
            cmd.CommandText = Sql
            cmd.Connection = cn
            cmd.ExecuteNonQuery()

            cn.Close()
            cn.Dispose()
            Return True
        Catch ex As Exception
            MsgBox(ErrorText)
            Return False
        End Try
    End Function

    ' SQL文を実行するメソッド(get)
    Public Function GetDB(Sql As String,
            Columns As String(),
            ErrorText As String) As DataTable
        Try

            Dim cn As New Microsoft.Data.SqlClient.SqlConnection
            cn.ConnectionString = "Data Source=" & Me.Data_Source & " Initial Catalog=" & Me.Inital_Catalog & " User ID=" & Me.User_ID & " Password=" & Me.Password & " "

            cn.Open()

            Dim cmd As New Microsoft.Data.SqlClient.SqlCommand
            cmd.CommandText = Sql
            cmd.Connection = cn

            Dim dr As Microsoft.Data.SqlClient.SqlDataReader = cmd.ExecuteReader()

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
        Catch ex As Exception
            MsgBox(ErrorText)
            Return Nothing
        End Try
    End Function
End Class
