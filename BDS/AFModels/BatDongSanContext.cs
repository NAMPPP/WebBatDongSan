namespace AFModels
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BatDongSanContext : DbContext
    {
        public BatDongSanContext()
            : base("name=BatDongSanContext")
        {
        }

        public virtual DbSet<BatDongSan> BatDongSans { get; set; }
        public virtual DbSet<BinhLuan> BinhLuans { get; set; }
        public virtual DbSet<ChiTietBatDongSan> ChiTietBatDongSans { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<HoaDonKhachHang> HoaDonKhachHangs { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<LienHe> LienHes { get; set; }
        public virtual DbSet<LoaiBatDongSan> LoaiBatDongSans { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<NhomLoai> NhomLoais { get; set; }
        public virtual DbSet<PhanQuyen> PhanQuyens { get; set; }
        public virtual DbSet<QuanLyDatThue> QuanLyDatThues { get; set; }
        public virtual DbSet<QuanLyDatThueKhachHang> QuanLyDatThueKhachHangs { get; set; }
        public virtual DbSet<QuanTri> QuanTris { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<ThanhVien> ThanhViens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BatDongSan>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietBatDongSan>()
                .Property(e => e.Anh)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietBatDongSan>()
                .Property(e => e.Paking)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.DienThoai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<LienHe>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.DienThoai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.Facebook)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.Googleplus)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.Twitter)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.Instagram)
                .IsUnicode(false);

            modelBuilder.Entity<QuanTri>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<QuanTri>()
                .Property(e => e.Pw)
                .IsUnicode(false);

            modelBuilder.Entity<QuanTri>()
                .HasMany(e => e.BatDongSans)
                .WithRequired(e => e.QuanTri)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<QuanTri>()
                .HasMany(e => e.NhanViens)
                .WithRequired(e => e.QuanTri)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ThanhVien>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<ThanhVien>()
                .Property(e => e.Pw)
                .IsUnicode(false);

            modelBuilder.Entity<ThanhVien>()
                .Property(e => e.DienThoai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ThanhVien>()
                .HasMany(e => e.HoaDons)
                .WithOptional(e => e.ThanhVien)
                .HasForeignKey(e => e.MaKH);

            modelBuilder.Entity<ThanhVien>()
                .HasMany(e => e.QuanLyDatThues)
                .WithOptional(e => e.ThanhVien)
                .HasForeignKey(e => e.MaKH);
        }
    }
}
