namespace AFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoaiBatDongSan")]
    public partial class LoaiBatDongSan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoaiBatDongSan()
        {
            BatDongSans = new HashSet<BatDongSan>();
        }

        [Key]
        public int MaLoaiBDS { get; set; }

        [Required]
        [StringLength(50)]
        public string TenLoaiBDS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BatDongSan> BatDongSans { get; set; }
    }
}
