using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppQL.Models.EF
{
    public class TongKetKho
    {
        public TongKetKho() { }
        public string id_loai { get; set; }
        public string ten_loai { get; set; }
        public string ngay_nhap { get; set; }
        public string gia_nhap { get; set; }
        public string tong_so_luong { get; set; }
        public string tong_tien { get; set; }
    }
}