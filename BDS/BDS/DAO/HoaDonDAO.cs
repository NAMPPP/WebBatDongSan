using AFModels;
using BDS.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDS.DAO
{
    public class HoaDonDAO
    {
        BatDongSanContext db = new BatDongSanContext();
        public List<HoaDonViewModel> list()
        {
            List<HoaDonViewModel> kq = (from hd in db.HoaDons
                                               select new HoaDonViewModel
                                               {
                                                   MaHD = hd.MaHD,
                                                   MaKH = hd.MaKH,
                                                   MaNV = hd.MaNV,
                                                   MaBDS = hd.MaBDS,
                                                   TongTien = hd.TongTien,
                                                   NgayThanhToan = hd.NgayThanhToan
                                               }).ToList();
            return kq;
        }


        public IEnumerable<HoaDon> ListAll(int pageNumber, int pageSize)
        {
            return db.HoaDons.OrderByDescending(s => s.MaHD).ToPagedList(pageNumber, pageSize);
        }
        public long Insert(HoaDon hd)
        {
            db.HoaDons.Add(hd);
            db.SaveChanges();
            return hd.MaHD;
        }
        public bool Delete(int id)
        {
            try
            {
                var cate = db.HoaDons.Find(id);
                db.HoaDons.Remove(cate);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool Edit(HoaDon idhd)
        {
            try
            {
                var tv = db.HoaDons.Find(idhd);
                tv.MaKH = idhd.MaKH;
                tv.MaNV = idhd.MaNV;
                tv.MaBDS = idhd.MaBDS;
                tv.TongTien = idhd.TongTien;
                tv.NgayThanhToan = idhd.NgayThanhToan;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public HoaDon GetByID(int id)
        {
            return db.HoaDons.Find(id);
        }
        public IEnumerable<HoaDon> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<HoaDon> model = db.HoaDons;
            if (!string.IsNullOrEmpty(searchString))
            {
                //model = model.Where(x => x.MaDat.Contains(searchString) || x.MaDat.Contains(searchString));
            }

            return model.OrderByDescending(x => x.MaHD).ToPagedList(page, pageSize);
        }
    }
}