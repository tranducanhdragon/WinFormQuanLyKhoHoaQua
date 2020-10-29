using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppQL.Models.EF;
using System.Data.SQLite;

namespace AppQL.Models.DAO
{
    public class TongKetKhoDAO
    {
        public string conString = "Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "KhoHoaQua.db" + ";Version=3;New=False;";
        public TongKetKhoDAO()
        {

        }
        public IEnumerable<TongKetKho> GetTongKetLoaiQua()
        {
            SQLiteConnection conn = new SQLiteConnection(conString);
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(conn);
            cmd.CommandText = "select Qua.id_loai, ten_loai, count(*) as sl,gia_tien, ngay_sx " +
                "from Qua join Loai_Qua on Qua.id_loai = Loai_Qua.id_loai group by Qua.id_loai, ten_loai, gia_tien, ngay_sx";
            SQLiteDataReader rd = cmd.ExecuteReader();

            List<TongKetKho> li = new List<TongKetKho>();
            while (rd.Read())
            {
                TongKetKho tk = new TongKetKho();
                tk.id_loai = rd["id_loai"].ToString();
                tk.ten_loai = rd["ten_loai"].ToString();
                tk.tong_so_luong = rd["sl"].ToString();
                tk.gia_nhap = rd["gia_tien"].ToString();
                tk.ngay_nhap = rd["ngay_sx"].ToString();
                tk.tong_tien = (Int32.Parse(tk.tong_so_luong) * Int32.Parse(tk.gia_nhap)).ToString();
                li.Add(tk);
            }
            conn.Close();
            return li;
        }
    }
}