using AFModels;
using BDS.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDS.DAO
{
    public class ThanhVienDAO
    {
        BatDongSanContext db = new BatDongSanContext();
        public List<ThanhVienViewModel> list()
        {
            List<ThanhVienViewModel> kq = (from tv in db.ThanhViens
                                           select new ThanhVienViewModel
                                           {
                                               MaTV = tv.MaTV,
                                               HoTenTV = tv.HoTenTV,
                                               UserName = tv.UserName,
                                               Pw = tv.Pw,
                                               DienThoai = tv.DienThoai,
                                               GioiTinh = tv.GioiTinh,
                                               DiaChi = tv.DiaChi,
                                               Email = tv.Email,
                                               NgayDK = tv.NgayDK

                                           }).ToList();
            return kq;
        }


        public IEnumerable<ThanhVien> ListAll(int pageNumber, int pageSize)
        {
            return db.ThanhViens.OrderBy(s => s.MaTV).ToPagedList(pageNumber, pageSize);
        }
        public long Insert(ThanhVien nv)
        {
            nv.NgayDK = DateTime.Now;
            db.ThanhViens.Add(nv);
            db.SaveChanges();
            return nv.MaTV;
        }
        public bool Delete(int id)
        {
            try
            {
                var cate = db.ThanhViens.Find(id);
                db.ThanhViens.Remove(cate);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool Edit(ThanhVien idtv)
        {
            try
            {
                var tv = db.ThanhViens.Find(idtv.MaTV);
                tv.HoTenTV = idtv.HoTenTV;
                tv.Pw = idtv.Pw;
                tv.DienThoai = idtv.DienThoai;
                tv.GioiTinh = idtv.GioiTinh;
                tv.DiaChi = idtv.DiaChi;
                tv.Email = idtv.Email;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public ThanhVien GetByID(int id)
        {
            return db.ThanhViens.Find(id);
        }

       
        public IEnumerable<ThanhVien> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<ThanhVien> model = db.ThanhViens;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.HoTenTV.Contains(searchString) || x.HoTenTV.Contains(searchString));
            }

            return model.OrderByDescending(x => x.MaTV).ToPagedList(page, pageSize);
        }
    }
}