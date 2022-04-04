namespace AFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BinhLuan")]
    public partial class BinhLuan
    {
        [Key]
        public int MaBL { get; set; }

        [StringLength(50)]
        public string TenBL { get; set; }

        [StringLength(1000)]
        public string NoiDungBL { get; set; }

        public int? MaBDS { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayBL { get; set; }

        public int? Duyet { get; set; }

        public virtual BatDongSan BatDongSan { get; set; }
    }
}
