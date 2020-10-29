using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppQL.Models.EF
{
    public class PhieuNhapHang
    {
        public PhieuNhapHang() { }
        public string id_phieu_nhap_hang { get; set; }
        public string id_cssx { get; set; }
        public string id_loai { get; set; }
        public string ten_loai { get; set; }
        public string so_luong { get; set; }
        public string gia_nhap { get; set; }
        public string ngay_nhap { get; set; }
    }
}