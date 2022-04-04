namespace AFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietBatDongSan")]
    public partial class ChiTietBatDongSan
    {
        [Key]
        public int STT { get; set; }

        public int? MaBDS { get; set; }

        public decimal? Gia { get; set; }

        [StringLength(50)]
        public string DienTich { get; set; }

        [StringLength(50)]
        public string DiaChiBDS { get; set; }

        [StringLength(1000)]
        public string Mota { get; set; }

        [StringLength(50)]
        public string KhuVuc { get; set; }

        [StringLength(255)]
        public string Anh { get; set; }

        public int? PhongTam { get; set; }

        public int? PhongNgu { get; set; }

        [StringLength(10)]
        public string Paking { get; set; }

        public virtual BatDongSan BatDongSan { get; set; }
    }
}
