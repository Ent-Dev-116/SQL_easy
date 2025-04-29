namespace Sql_Easy
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class Sql_Easy
    {
        private string contectString;
        private List<string> selectColumn;

        public Sql_Easy(string Source, string Id, string Pass, string DataBase)
        {
            this.contectString = "Data Source=" + Source + ";Initial Catalog=" + DataBase + ";User ID=" + Id + ";Password=" + Pass + ";";
            this.selectColumn = new List<string>();
        }

        public DataTable? SqlCmd
            (
            string sqlType,
            string TableName,
            List<string> Column,
            string eMessage ="default",
            List<string>? newValue = default,
            List<string>? whereColumn=default,
            List<string>? whereValue = default
            )
        {
            try
            {
                DataTable table = new DataTable();
                using (SqlConnection connection = new SqlConnection(contectString))
                {
                    connection.Open();

                    string sql = "";
                    if (sqlType == "SELECT")
                    {
                        sql += "SELECT ";
                        if (Column == null)
                        {
                            sql += "* FROM " + TableName + "";
                        }
                        else
                        {
                            for (int i = 0; i < Column.Count; i++)
                            {
                                sql += Column[i] + ", ";
                            }
                            sql += " FROM " + TableName + "";
                        }
                    }
                    else if (sqlType == "INSERT") { 
                        sql += "INSERT INTO " + TableName + "(";
                        for (int i = 0; i < Column.Count; i++)
                        {
                            sql += Column[i];
                            if (i != Column.Count - 1)
                            {
                                sql += ", ";
                            }
                        }
                        sql += ") VALUES (";
                        for (int i = 0; i < Column.Count; i++)
                        {
                            sql += "@" + i.ToString() + "Value";
                            if (i != Column.Count - 1)
                            {
                                sql += ", ";
                            }
                        }
                        sql += ")";
                    }
                    else if (sqlType == "UPDATE")
                    {
                        sql += "UPDATE " + TableName + " SET ";
                        for (int i = 0; i < Column.Count; i++)
                        {
                            sql += Column[i] + " = @" + i.ToString() + "Value";
                            if (i != Column.Count - 1)
                            {
                                sql += ", ";
                            }
                        }
                    }else{
                        return null;
                    }
                    if (whereColumn != null)
                    {
                        sql += " WHERE ";
                        for (int i = 0; i < whereColumn.Count; i++)
                        {
                            sql += whereColumn[i] + " = @" + i.ToString() + "WhereValue";
                            if (i != whereColumn.Count - 1)
                            {
                                sql += " AND ";
                            }
                        }
                    }
                    sql += ";";

                    SqlCommand command = new SqlCommand(sql, connection);

                    if (newValue != null)
                    {
                        for (int i = 0; i < newValue.Count; i++)
                        {
                            command.Parameters.AddWithValue("@" + i.ToString() + "Value", newValue[i]);
                        }
                    }
                    if(whereValue != null)
                    {
                        for (int i = 0; i < whereValue.Count; i++)
                        {
                            command.Parameters.AddWithValue("@" + i.ToString() + "WhereValue", whereValue[i]);
                        }
                    }

                    if (sqlType == "SELECT")
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            DataRow row = table.NewRow();
                            foreach (string column in Column)
                            {
                                Type type = reader[column].GetType();
                                if (type.Name == "DateTime")
                                {
                                    DateTime time = reader.GetDateTime(reader.GetOrdinal(column));
                                    row[column] = time.ToString("yyyy/MM/dd HH:mm:ss");
                                }
                                else
                                {
                                    row[column] = Convert.ChangeType(reader[column].ToString(), type);
                                }
                            }
                            table.Rows.Add(row);
                        }
                    }
                    else if (sqlType == "INSERT" || sqlType == "UPDATE" || sqlType == "DELETE")
                    {
                        command.ExecuteNonQuery();
                        connection.Close();
                        table.Columns.Add("collected!");
                    }
                }
                return table;
            }
            catch (SqlException e)
            {
                if(eMessage == null)
                    Console.WriteLine("エラー: " + e.Message);
                else
                    Console.WriteLine(eMessage);
                return null;
            }
        }
    }
}
