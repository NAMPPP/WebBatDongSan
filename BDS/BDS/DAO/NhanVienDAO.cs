using AFModels;
using BDS.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDS.DAO
{
    public class NhanVienDAO
    {
        BatDongSanContext db = new BatDongSanContext();
        public List<NhanVienViewModel> list()
        {
            List<NhanVienViewModel> kq = (from nv in db.NhanViens
                                            select new NhanVienViewModel
                                            {
                                                MaNV = nv.MaNV,
                                                HoTenNV = nv.HoTenNV,
                                                ChucVu = nv.ChucVu,
                                                DienThoai = nv.DienThoai,
                                                GioiTinh = nv.GioiTinh,
                                                DiaChi = nv.DiaChi,
                                                Email = nv.Email,
                                                UserName = nv.UserName,
                                                Facebook = nv.Facebook,
                                                Googleplus = nv.Googleplus,
                                                Twitter = nv.Twitter,
                                                Instagram = nv.Instagram
                                            }).ToList();
            return kq;
        }

        public IEnumerable<NhanVien> ListAll(int pageNumber, int pageSize)
        {
            return db.NhanViens.OrderBy(s => s.MaNV).ToPagedList(pageNumber, pageSize);
        }
        public long Insert(NhanVien nv)
        {
            db.NhanViens.Add(nv);
            db.SaveChanges();
            return nv.MaNV;
        }
        public bool Delete(int id)
        {
            try
            {
                var cate = db.NhanViens.Find(id);
                db.NhanViens.Remove(cate);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool Edit(NhanVien idnv)
        {
            try
            {
                var nv = db.NhanViens.Find(idnv.MaNV);
                nv.HoTenNV = idnv.HoTenNV;
                nv.ChucVu = idnv.ChucVu;
                nv.DienThoai = idnv.DienThoai;
                nv.GioiTinh = idnv.GioiTinh;
                nv.DiaChi = idnv.DiaChi;
                nv.Email = idnv.Email;
                nv.Facebook = idnv.Facebook;
                nv.Googleplus = idnv.Googleplus;
                nv.Twitter = idnv.Twitter;
                nv.Instagram = idnv.Instagram;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public NhanVien GetByID(int id)
        {
            return db.NhanViens.Find(id);
        }
        public IEnumerable<NhanVien> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<NhanVien> model = db.NhanViens;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.HoTenNV.Contains(searchString) || x.HoTenNV.Contains(searchString));
            }

            return model.OrderByDescending(x => x.MaNV).ToPagedList(page, pageSize);
        }
    }
}