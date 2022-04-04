using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDS.ViewModel
{
    public class QuanLyDatThueKhachHangViewModel
    {
        public int MaDat { get; set; }

        public int? MaKH { get; set; }

        public int? MaBDS { get; set; }

        public DateTime? NgayDat { get; set; }

        public string GhiChu { get; set; }

        public int ThanhToan { get; set; }
    }
}