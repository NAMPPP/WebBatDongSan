using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDS.ViewModel
{
    public class BinhLuanViewModel
    {
        public int MaBL { get; set; }

        public string TenBL { get; set; }

        public string NoiDungBL { get; set; }

        public int? MaBDS { get; set; }

        public DateTime? NgayBL { get; set; }

        public int? Duyet { get; set; }
    }
}