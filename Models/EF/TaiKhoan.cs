using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppQL.Models.EF
{
    public class TaiKhoan
    {
        public TaiKhoan() { }
        public string Id { get; set; }

        public string userName { get; set; }

        public string passWord { get; set; }
    }
}