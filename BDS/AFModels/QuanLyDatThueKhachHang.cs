namespace AFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QuanLyDatThueKhachHang")]
    public partial class QuanLyDatThueKhachHang
    {
        [Key]
        public int MaDat { get; set; }

        public int? MaKH { get; set; }

        public int? MaBDS { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayDat { get; set; }

        [StringLength(50)]
        public string GhiChu { get; set; }

        public int ThanhToan { get; set; }

        public virtual BatDongSan BatDongSan { get; set; }

        public virtual KhachHang KhachHang { get; set; }
    }
}
