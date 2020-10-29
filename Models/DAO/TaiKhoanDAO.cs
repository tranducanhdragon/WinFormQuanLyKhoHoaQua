using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using AppQL.Models.EF;

namespace AppQL.Models.DAO
{
    public class TaiKhoanDAO
    {
        public string conString = "Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "KhoHoaQua.db"+";Version=3;New=False;";
        public TaiKhoanDAO()
        {
            
        }
        public bool Login(string username, string pass)
        {
            SQLiteConnection conn = new SQLiteConnection(conString);
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(conn);
            cmd.CommandText = "select * from Tai_Khoan where user_name = @username and pass_word = @pass";
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@pass", pass);
            var result = cmd.ExecuteScalar();
            conn.Close();
            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public TaiKhoan TaiKhoanByUserName(string userName)
        {
            SQLiteConnection conn = new SQLiteConnection(conString);
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(conn);
            cmd.CommandText = "select * from Tai_Khoan where user_name = @username";
            cmd.Parameters.AddWithValue("@username", userName);
            SQLiteDataReader rd = cmd.ExecuteReader();
            TaiKhoan tk = new TaiKhoan();
            while (rd.Read())
            {
                tk.Id = rd["id_tai_khoan"].ToString();
                tk.userName = rd["user_name"].ToString();
                tk.passWord = rd["pass_word"].ToString();
            }
            conn.Close();
            return tk;
        }
    }
}