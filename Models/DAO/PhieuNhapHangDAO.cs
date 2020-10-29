using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppQL.Models.EF;
using System.Data.SQLite;

namespace AppQL.Models.DAO
{
    public class PhieuNhapHangDAO
    {
        public string conString = "Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "KhoHoaQua.db" + ";Version=3;New=False;";
        public PhieuNhapHangDAO()
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
        public void InsertPhieuNhap(PhieuNhapHang pn)
        {
            SQLiteConnection conn = new SQLiteConnection(conString);
            conn.Open();
            SQLiteCommand insertSQL = new SQLiteCommand("INSERT INTO Phieu_Nhap_Hang (id_cssx, id_loai, so_luong, gia_nhap, ngay_nhap) VALUES (@id_cssx,@id_loai,@so_luong,@gia_nhap,@ngay_nhap)", conn);
            insertSQL.Parameters.AddWithValue("@id_cssx",pn.id_cssx);
            insertSQL.Parameters.AddWithValue("@id_loai",pn.id_loai);
            insertSQL.Parameters.AddWithValue("@so_luong",pn.so_luong);
            insertSQL.Parameters.AddWithValue("@gia_nhap",pn.gia_nhap);
            insertSQL.Parameters.AddWithValue("@ngay_nhap",pn.ngay_nhap.Substring(0, 10));
            insertSQL.ExecuteNonQuery();
            conn.Close();
        }
        public void UpdatePhieuNhap(PhieuNhapHang pn)
        {
            SQLiteConnection conn = new SQLiteConnection(conString);
            conn.Open();
            SQLiteCommand insertSQL = new SQLiteCommand("Update Phieu_Nhap_Hang set id_cssx = @id_cssx, id_loai = @id_loai, so_luong = @so_luong, gia_nhap = @gia_nhap, ngay_nhap = @ngay_nhap where id_phieu_nhap_hang = @idphieu", conn);
            insertSQL.Parameters.AddWithValue("@id_cssx", pn.id_cssx);
            insertSQL.Parameters.AddWithValue("@id_loai", pn.id_loai);
            insertSQL.Parameters.AddWithValue("@so_luong", pn.so_luong);
            insertSQL.Parameters.AddWithValue("@gia_nhap", pn.gia_nhap);
            insertSQL.Parameters.AddWithValue("@ngay_nhap", pn.ngay_nhap.Substring(0,10));
            insertSQL.Parameters.AddWithValue("@idphieu", pn.id_phieu_nhap_hang);
            insertSQL.ExecuteNonQuery();
            conn.Close();
        }
        public IEnumerable<PhieuNhapHang> GetListPhieuNhap()
        {
            SQLiteConnection conn = new SQLiteConnection(conString);
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(conn);
            cmd.CommandText = "select id_phieu_nhap_hang,id_cssx,Phieu_Nhap_Hang.id_loai,ten_loai,ngay_nhap,so_luong,gia_nhap " +
                                "from Phieu_Nhap_Hang join Loai_Qua on Phieu_Nhap_Hang.id_loai = Loai_Qua.id_loai";
            SQLiteDataReader rd = cmd.ExecuteReader();
            List<PhieuNhapHang> li = new List<PhieuNhapHang>();
            while (rd.Read())
            {
                PhieuNhapHang pn = new PhieuNhapHang();
                pn.id_phieu_nhap_hang = rd["id_phieu_nhap_hang"].ToString();
                pn.id_cssx = rd["id_cssx"].ToString();
                pn.id_loai = rd["id_loai"].ToString();
                pn.ten_loai = rd["ten_loai"].ToString();
                pn.ngay_nhap = rd["ngay_nhap"].ToString();
                pn.so_luong = rd["so_luong"].ToString();
                pn.gia_nhap = rd["gia_nhap"].ToString();
                li.Add(pn);
            }
            conn.Close();
            return li;
        }
        public IEnumerable<PhieuNhapHang> GetListPhieuNhapGroupByIDLoai()
        {
            SQLiteConnection conn = new SQLiteConnection(conString);
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(conn);
            cmd.CommandText = "select Phieu_Nhap_Hang.id_loai,ten_loai,gia_nhap,ngay_nhap,sum(so_luong) as sl " +
                                "from Phieu_Nhap_Hang join Loai_Qua on Phieu_Nhap_Hang.id_loai = Loai_Qua.id_loai " +
                                "group by Phieu_Nhap_Hang.id_loai,ten_loai,gia_nhap,ngay_nhap " +
                                "order by ngay_nhap asc";
            SQLiteDataReader rd = cmd.ExecuteReader();
            List<PhieuNhapHang> li = new List<PhieuNhapHang>();
            while (rd.Read())
            {
                PhieuNhapHang pn = new PhieuNhapHang();
                pn.id_loai = rd["id_loai"].ToString();
                pn.ten_loai = rd["ten_loai"].ToString();
                pn.ngay_nhap = rd["ngay_nhap"].ToString();
                pn.so_luong = rd["sl"].ToString();
                pn.gia_nhap = rd["gia_nhap"].ToString();
                li.Add(pn);
            }
            conn.Close();
            return li;
        }
        public void InsertToQua(PhieuNhapHang pn)
        {
            SQLiteConnection conn = new SQLiteConnection(conString);
            conn.Open();
            for(int i = 0; i < Int32.Parse(pn.so_luong); i++)
            {
                SQLiteCommand cmd = new SQLiteCommand("insert into Qua(id_loai,gia_tien,id_nsx,ngay_sx,han_sd) values(@id_loai,@gia_tien,@id_nsx,@ngay_sx,@han_sd)", conn);
                cmd.Parameters.AddWithValue("@id_loai",pn.id_loai);
                cmd.Parameters.AddWithValue("@gia_tien",pn.gia_nhap);
                cmd.Parameters.AddWithValue("@id_nsx",pn.id_cssx);
                cmd.Parameters.AddWithValue("@ngay_sx",pn.ngay_nhap.Substring(0, 10));
                cmd.Parameters.AddWithValue("@han_sd", pn.ngay_nhap.Substring(0, 10));
                cmd.ExecuteNonQuery();
                
            }
            conn.Close();
        }
        public PhieuNhapHang GetPhieuNhapByID(string id_phieu)
        {
            SQLiteConnection conn = new SQLiteConnection(conString);
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(conn);
            cmd.CommandText = "select * from Phieu_Nhap_Hang join Loai_Qua on Phieu_Nhap_Hang.id_loai = Loai_Qua.id_loai " +
                                "where id_phieu_nhap_hang = @id_phieu";
            cmd.Parameters.AddWithValue("@id_phieu", id_phieu);
            SQLiteDataReader rd = cmd.ExecuteReader();
            PhieuNhapHang pn = new PhieuNhapHang();
            while (rd.Read())
            {
                pn.id_loai = rd["id_loai"].ToString();
                pn.ngay_nhap = rd["ngay_nhap"].ToString();
                pn.so_luong = rd["so_luong"].ToString();
                pn.gia_nhap = rd["gia_nhap"].ToString();
                pn.id_cssx = rd["id_cssx"].ToString();
                pn.ten_loai = rd["ten_loai"].ToString();
            }
            conn.Close();
            return pn;
        }
    }
}