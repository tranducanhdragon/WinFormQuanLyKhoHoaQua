using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppQL.Models.EF
{
    public class PhieuXuatHang
    {
        public PhieuXuatHang() { }
        public string id_phieu_xuat_hang { get; set; }
        public string id_cua_hang { get; set; }
        public string id_loai { get; set; }
        public string ten_loai { get; set; }
        public string so_luong { get; set; }
        public string gia_xuat { get; set; }
        public string ngay_xuat { get; set; }
    }
}