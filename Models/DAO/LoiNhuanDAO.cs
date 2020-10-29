using System;
using System.Collections.Generic;
using System.Linq;
using AppQL.Models.EF;
using System.Data.SQLite;
using System.Globalization;

namespace AppQL.Models.DAO
{
    public class LoiNhuanDAO
    {
        public string conString = "Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "KhoHoaQua.db" + ";Version=3;New=False;";
        public LoiNhuanDAO()
        {

        }
        public List<LoiNhuan> GetLoiNhuanLoaiQua()
        {

            //Lấy danh sách phiếu xuất
            List<PhieuXuatHang> lix = new PhieuXuatHangDAO().GetListPhieuXuatGroupByIDLoai().ToList();
            //Lấy danh sách phiếu nhập
            List<PhieuNhapHang> lin = new PhieuNhapHangDAO().GetListPhieuNhapGroupByIDLoai().ToList();
            //Tính lợi nhuận
            List<LoiNhuan> liln = new List<LoiNhuan>();
            foreach (PhieuXuatHang px in lix)
            {

                foreach (PhieuNhapHang pn in lin)
                {
                    if (Int32.Parse(px.so_luong) <= Int32.Parse(pn.so_luong) && px.id_loai == pn.id_loai)
                    {
                        LoiNhuan lnn = new LoiNhuan();
                        lnn.tien = (Int32.Parse(px.so_luong) * (Int32.Parse(px.gia_xuat) - Int32.Parse(pn.gia_nhap)));
                        lnn.so_luong = px.so_luong;
                        lnn.id_loai = px.id_loai;
                        lnn.ten_loai = px.ten_loai;
                        lnn.gia_nhap = pn.gia_nhap;
                        lnn.gia_xuat = px.gia_xuat;
                        lnn.ngay_xuat = px.ngay_xuat;
                        lnn.tong_tien_nhap = (Int32.Parse(lnn.gia_nhap) * Int32.Parse(lnn.so_luong)).ToString();
                        lnn.tong_tien_xuat = (Int32.Parse(lnn.gia_xuat) * Int32.Parse(lnn.so_luong)).ToString();
                        pn.so_luong = (Int32.Parse(pn.so_luong) - Int32.Parse(px.so_luong)).ToString();
                        lnn.loi_nhuan = (Int32.Parse(lnn.tong_tien_xuat) - Int32.Parse(lnn.tong_tien_nhap)).ToString();
                        liln.Add(lnn);
                        break;
                    }
                    else if (Int32.Parse(px.so_luong) > Int32.Parse(pn.so_luong) && px.id_loai == pn.id_loai)
                    {
                        LoiNhuan lnx = new LoiNhuan();
                        lnx.tien += Int32.Parse(pn.so_luong) * (Int32.Parse(px.gia_xuat) - Int32.Parse(pn.gia_nhap));
                        lnx.so_luong += pn.so_luong;
                        lnx.id_loai = px.id_loai;
                        lnx.ten_loai = px.ten_loai;
                        lnx.gia_nhap = pn.gia_nhap;
                        lnx.gia_xuat = px.gia_xuat;
                        lnx.ngay_xuat = px.ngay_xuat;
                        lnx.tong_tien_nhap = (Int32.Parse(lnx.gia_nhap) * Int32.Parse(lnx.so_luong)).ToString();
                        lnx.tong_tien_xuat = (Int32.Parse(lnx.gia_xuat) * Int32.Parse(lnx.so_luong)).ToString();
                        px.so_luong = (Int32.Parse(px.so_luong) - Int32.Parse(pn.so_luong)).ToString();
                        lnx.loi_nhuan = (Int32.Parse(lnx.tong_tien_xuat) - Int32.Parse(lnx.tong_tien_nhap)).ToString();
                        liln.Add(lnx);
                    }
                }
            }
            return liln;
        }
    }
}