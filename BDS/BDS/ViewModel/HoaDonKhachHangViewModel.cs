using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDS.ViewModel
{
    public class HoaDonKhachHangViewModel
    {
        public int MaHD { get; set; }

        public int? MaKH { get; set; }

        public int? MaNV { get; set; }

        public int? MaBDS { get; set; }

        public decimal? TongTien { get; set; }

        public DateTime? NgayThanhToan { get; set; }

        public double TongTienVAT
        {
            get
            {
                return (double)TongTien * 0.1 + (double)TongTien;
            }

        }

        public double VAT
        {
            get
            {
                return (double)TongTien * 0.1;
            }

        }


        public string HoTenKH { get; set; }

        public string DienThoai { get; set; }

        public string DiaChi { get; set; }

        public string Email { get; set; }

        public string HoTenNV { get; set; }

        public string TenBDS { get; set; }

        public string TenLoaiBDS { get; set; }
    }
}