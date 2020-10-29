using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;
using AppQL.Models.EF;

namespace AppQL.Models.DAO
{
    public class PhieuXuatHangDAO
    {
        public string conString = "Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "KhoHoaQua.db" + ";Version=3;New=False;";
        public PhieuXuatHangDAO()
        {

        }
        public IEnumerable<LoaiQua> GetLoaiQuaList()
        {
            SQLiteConnection conn = new SQLiteConnection(conString);
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(conn);
            cmd.CommandText = "select * from Loai_Qua";
            SQLiteDataReader rd = cmd.ExecuteReader();

            List<LoaiQua> li = new List<LoaiQua>();
            while (rd.Read())
            {
                LoaiQua lq = new LoaiQua();
                lq.id_loai = rd["id_loai"].ToString();
                lq.ten_loai = rd["ten_loai"].ToString();
                li.Add(lq);
            }
            conn.Close();
            return li;
        }
        public void InsertPhieuXuat(PhieuXuatHang px)
        {
            SQLiteConnection conn = new SQLiteConnection(conString);
            conn.Open();
            SQLiteCommand insertSQL = new SQLiteCommand("INSERT INTO Phieu_Xuat_Hang (id_cua_hang, id_loai, so_luong, gia_xuat, ngay_xuat_hang) VALUES (@id_cua_hang,@id_loai,@so_luong,@gia_xuat,@ngay_xuat_hang)", conn);
            insertSQL.Parameters.AddWithValue("@id_cua_hang", px.id_cua_hang);
            insertSQL.Parameters.AddWithValue("@id_loai", px.id_loai);
            insertSQL.Parameters.AddWithValue("@so_luong", px.so_luong);
            insertSQL.Parameters.AddWithValue("@gia_xuat", px.gia_xuat);
            insertSQL.Parameters.AddWithValue("@ngay_xuat_hang", px.ngay_xuat.Substring(0, 10));
            insertSQL.ExecuteNonQuery();
            conn.Close();
        }
        public void UpdatePhieuXuat(PhieuXuatHang px)
        {
            SQLiteConnection conn = new SQLiteConnection(conString);
            conn.Open();
            SQLiteCommand insertSQL = new SQLiteCommand("Update Phieu_Xuat_Hang set id_cua_hang = @id_cua_hang, id_loai = @id_loai, so_luong = @so_luong, gia_xuat = @gia_xuat, ngay_xuat = @ngay_xuat where id_phieu_xuat_hang = @idphieu", conn);
            insertSQL.Parameters.AddWithValue("@id_cua_hang", px.id_cua_hang);
            insertSQL.Parameters.AddWithValue("@id_loai", px.id_loai);
            insertSQL.Parameters.AddWithValue("@so_luong", px.so_luong);
            insertSQL.Parameters.AddWithValue("@gia_xuat", px.gia_xuat);
            insertSQL.Parameters.AddWithValue("@ngay_xuat", px.ngay_xuat.Substring(0,10));
            insertSQL.Parameters.AddWithValue("@idphieu", px.id_phieu_xuat_hang);
            insertSQL.ExecuteNonQuery();
            conn.Close();
        }
        public IEnumerable<PhieuXuatHang> GetListPhieuXuat()
        {
            SQLiteConnection conn = new SQLiteConnection(conString);
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(conn);
            cmd.CommandText = "select id_phieu_xuat_hang,id_cua_hang,Phieu_Xuat_Hang.id_loai,ten_loai,ngay_xuat_hang,so_luong,gia_xuat " +
                "from Phieu_Xuat_Hang join Loai_Qua on Phieu_Xuat_Hang.id_loai = Loai_Qua.id_loai";
            SQLiteDataReader rd = cmd.ExecuteReader();
            List<PhieuXuatHang> li = new List<PhieuXuatHang>();
            while (rd.Read())
            {
                PhieuXuatHang px = new PhieuXuatHang();
                px.id_phieu_xuat_hang = rd["id_phieu_xuat_hang"].ToString();
                px.id_cua_hang = rd["id_cua_hang"].ToString();
                px.id_loai = rd["id_loai"].ToString();
                px.ten_loai = rd["ten_loai"].ToString();
                px.ngay_xuat = rd["ngay_xuat_hang"].ToString();
                px.so_luong = rd["so_luong"].ToString();
                px.gia_xuat = rd["gia_xuat"].ToString();
                li.Add(px);
            }
            conn.Close();
            return li;
        }
        public IEnumerable<PhieuXuatHang> GetListPhieuXuatGroupByIDLoai()
        {
            SQLiteConnection conn = new SQLiteConnection(conString);
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(conn);
            cmd.CommandText = "select Phieu_Xuat_Hang.id_loai,ten_loai,gia_xuat,ngay_xuat_hang,sum(so_luong) as sl " +
                "from Phieu_Xuat_Hang join Loai_Qua on Phieu_Xuat_Hang.id_loai = Loai_Qua.id_loai " +
                "group by Phieu_Xuat_Hang.id_loai,ten_loai,gia_xuat,ngay_xuat_hang " +
                "order by ngay_xuat_hang asc";
            SQLiteDataReader rd = cmd.ExecuteReader();
            List<PhieuXuatHang> li = new List<PhieuXuatHang>();
            while (rd.Read())
            {
                PhieuXuatHang px = new PhieuXuatHang();
                px.id_loai = rd["id_loai"].ToString();
                px.ten_loai = rd["ten_loai"].ToString();
                px.gia_xuat = rd["gia_xuat"].ToString();
                px.ngay_xuat = rd["ngay_xuat_hang"].ToString();
                px.so_luong = rd["sl"].ToString();
                
                li.Add(px);
            }
            conn.Close();
            return li;
        }
        public void DeleteFromQua(PhieuXuatHang px)
        {
            try
            {
                SQLiteConnection conn = new SQLiteConnection(conString);
                conn.Open();
                for(int i = 0; i < Int32.Parse(px.so_luong); i++)
                {
                    SQLiteCommand cmd = new SQLiteCommand("DELETE FROM Qua WHERE id_qua =(SELECT MIN(id_qua) " +
                        "FROM Qua WHERE id_loai = @id_loai)", conn);
                    cmd.Parameters.AddWithValue("@id_loai", px.id_loai);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            catch(Exception e)
            {
                
            }
        }
        public PhieuXuatHang GetPhieuXuatByID(string id_phieu)
        {
            SQLiteConnection conn = new SQLiteConnection(conString);
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(conn);
            cmd.CommandText = "select * from Phieu_Xuat_Hang join Loai_Qua on Phieu_Xuat_Hang.id_loai = Loai_Qua.id_loai " +
                                "where id_phieu_xuat_hang = @id_phieu";
            cmd.Parameters.AddWithValue("@id_phieu", id_phieu);
            SQLiteDataReader rd = cmd.ExecuteReader();
            PhieuXuatHang px = new PhieuXuatHang();
            while (rd.Read())
            {
                px.id_loai = rd["id_loai"].ToString();
                px.ngay_xuat = rd["ngay_xuat_hang"].ToString();
                px.so_luong = rd["so_luong"].ToString();
                px.gia_xuat = rd["gia_xuat"].ToString();
                px.id_cua_hang = rd["id_cua_hang"].ToString();
                px.ten_loai = rd["ten_loai"].ToString();
            }
            conn.Close();
            return px;
        }
    }
}