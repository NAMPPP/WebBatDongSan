namespace AFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDonKhachHang")]
    public partial class HoaDonKhachHang
    {
        [Key]
        public int MaHD { get; set; }

        public int? MaKH { get; set; }

        public int? MaNV { get; set; }

        public int? MaBDS { get; set; }

        public decimal? TongTien { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayThanhToan { get; set; }

        public virtual BatDongSan BatDongSan { get; set; }

        public virtual KhachHang KhachHang { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
