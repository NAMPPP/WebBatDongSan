namespace AFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BatDongSan")]
    public partial class BatDongSan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BatDongSan()
        {
            BinhLuans = new HashSet<BinhLuan>();
            ChiTietBatDongSans = new HashSet<ChiTietBatDongSan>();
            HoaDons = new HashSet<HoaDon>();
            HoaDonKhachHangs = new HashSet<HoaDonKhachHang>();
            QuanLyDatThues = new HashSet<QuanLyDatThue>();
            QuanLyDatThueKhachHangs = new HashSet<QuanLyDatThueKhachHang>();
        }

        [Key]
        public int MaBDS { get; set; }

        [Required]
        [StringLength(1000)]
        public string TenBDS { get; set; }

        public int? MaNhom { get; set; }

        public int? MaLoaiBDS { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayDang { get; set; }

        public int? Duyet { get; set; }

        public virtual LoaiBatDongSan LoaiBatDongSan { get; set; }

        public virtual NhomLoai NhomLoai { get; set; }

        public virtual QuanTri QuanTri { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BinhLuan> BinhLuans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietBatDongSan> ChiTietBatDongSans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDon> HoaDons { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDonKhachHang> HoaDonKhachHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuanLyDatThue> QuanLyDatThues { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuanLyDatThueKhachHang> QuanLyDatThueKhachHangs { get; set; }
    }
}
