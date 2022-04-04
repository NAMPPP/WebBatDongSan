namespace AFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhachHang")]
    public partial class KhachHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhachHang()
        {
            HoaDonKhachHangs = new HashSet<HoaDonKhachHang>();
            QuanLyDatThueKhachHangs = new HashSet<QuanLyDatThueKhachHang>();
        }

        [Key]
        public int MaKH { get; set; }

        [Required]
        [StringLength(50)]
        public string HoTenKH { get; set; }

        [StringLength(12)]
        public string DienThoai { get; set; }

        [StringLength(50)]
        public string DiaChi { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDonKhachHang> HoaDonKhachHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuanLyDatThueKhachHang> QuanLyDatThueKhachHangs { get; set; }
    }
}
