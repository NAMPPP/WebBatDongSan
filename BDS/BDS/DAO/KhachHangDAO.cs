using AFModels;
using BDS.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDS.DAO
{
    public class KhachHangDAO
    {
        BatDongSanContext db = new BatDongSanContext();
        public List<KhachHangViewModel> list()
        {
            List<KhachHangViewModel> kq = (from kh in db.KhachHangs
                                            select new KhachHangViewModel()
                                            {
                                            MaKH = kh.MaKH,
                                            HoTenKH = kh.HoTenKH,
                                            DienThoai = kh.DienThoai,
                                            DiaChi = kh.DiaChi,
                                            Email = kh.Email
                                            }).ToList();
            return kq;
        }







        public IEnumerable<KhachHang> ListAll(int pageNumber, int pageSize)
        {
            return db.KhachHangs.OrderBy(s => s.MaKH).ToPagedList(pageNumber, pageSize);
        }
        public long Insert(KhachHang kh)
        {
            db.KhachHangs.Add(kh);
            db.SaveChanges();
            return kh.MaKH;
        }
        public bool Delete(int id)
        {
            try
            {
                var cate = db.KhachHangs.Find(id);
                db.KhachHangs.Remove(cate);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool Edit(KhachHang id)
        {
            try
            {
                var kh = db.KhachHangs.Find(id.MaKH);
                kh.MaKH = id.MaKH;
                kh.HoTenKH = id.HoTenKH;
                kh.DienThoai = id.DienThoai;
                kh.DiaChi = id.DiaChi;
                kh.Email = id.Email;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public KhachHang GetByID(int id)
        {
            return db.KhachHangs.Find(id);
        }
        public IEnumerable<KhachHang> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<KhachHang> model = db.KhachHangs;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.HoTenKH.Contains(searchString) || x.HoTenKH.Contains(searchString));
            }

            return model.OrderByDescending(x => x.MaKH).ToPagedList(page, pageSize);
        }
    }
}