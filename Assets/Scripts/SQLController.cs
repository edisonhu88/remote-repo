using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HutongGames.PlayMaker;
using MySql.Data.MySqlClient;
using UnityEngine;

public class SQLController:MonoBehaviour
{
    public static MySqlConnection conn;
    public static MySqlCommand cmd;
    private static object temp;
    public static void OpenSQL()
    {
        try
        {
            string connStr = "Database=pingtu; Data Source=101.43.75.39; port=3306; User Id=pingtu; Password=pingtu;";
            conn = new MySqlConnection(connStr);
            conn.Open();
            print("链接成功");
        }
        catch (System.Exception)
        {
            print("链接失败");

        }

    }

    public static void CloseSql()
    {
        if (conn != null)
        {
            conn.Close();
        }

    }

    public static void InsertSQL(string name, string pass)
    {
        OpenSQL();
        string sqlQuary = "insert into  bird(username,password) values (@username, @pass)";
        cmd = new MySqlCommand(sqlQuary, conn);
        cmd.Parameters.AddWithValue("@username", name);
        cmd.Parameters.AddWithValue("@pass", pass);
        cmd.ExecuteNonQuery();
        conn.Close();
        CloseSql();
    }

    public static T InquireMysql<T>(string username, string table)
    {
        OpenSQL();
        string sqlQuary = "SELECT* FROM " + table + " WHERE  username ='" + username + "'";
        cmd = new MySqlCommand(sqlQuary, conn);
        MySqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            if (table == "bird")
            {
                temp = (T)Convert.ChangeType(reader.GetString("password"), typeof(T));

            }
            else if (table == "coins")
            {
                temp = (T)Convert.ChangeType(reader.GetFloat("money"), typeof(T));

            }
        }
        conn.Close();
        CloseSql();
        return (T)temp;
    }


    public  static void UpdateSQL(string username,float  money)
    {
        OpenSQL();
        string sql = "UPDATE coins SET money =money- @Value1 WHERE username = @Value2";
        cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Value1", money);
        cmd.Parameters.AddWithValue("@Value2", username);
        cmd.ExecuteNonQuery();
        conn.Close();
        CloseSql();
    }


}
