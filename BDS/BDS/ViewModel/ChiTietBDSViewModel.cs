using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDS.ViewModel
{
    public class ChiTietBDSViewModel
    {
        public int STT { get; set; }

        public int? MaBDS { get; set; }

        public decimal? Gia { get; set; }

        public string DienTich { get; set; }

        public string DiaChiBDS { get; set; }

        public string Mota { get; set; }

        public string KhuVuc { get; set; }

        public string Anh { get; set; }

        public int? PhongTam { get; set; }

        public int? PhongNgu { get; set; }

        public string Paking { get; set; }
    }
}