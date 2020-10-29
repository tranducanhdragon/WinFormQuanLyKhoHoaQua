using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppQL.Models.EF
{
    public class LoiNhuan
    {
        public LoiNhuan() { }
        public string id_loai { get; set; }
        public string ten_loai { get; set; }
        public string ngay_xuat { get; set; }
        public string gia_nhap { get; set; }
        public string gia_xuat { get; set; }
        public string tong_tien_nhap { get; set; }
        public string tong_tien_xuat { get; set; }
        public int tien { get; set; }
        public string so_luong { get; set; }
        public string loi_nhuan { get; set; }
    }
}