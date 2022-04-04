using AFModels;
using BDS.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDS.DAO
{
    public class HoaDonKhachHangDAO
    {
        BatDongSanContext db = new BatDongSanContext();
        public List<HoaDonKhachHangViewModel> list()
        {
            List<HoaDonKhachHangViewModel> kq = (from hd in db.HoaDons
                                        select new HoaDonKhachHangViewModel
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


        public IEnumerable<HoaDonKhachHang> ListAll(int pageNumber, int pageSize)
        {
            return db.HoaDonKhachHangs.OrderByDescending(s => s.MaHD).ToPagedList(pageNumber, pageSize);
        }
        public long Insert(HoaDonKhachHang hd)
        {
            db.HoaDonKhachHangs.Add(hd);
            db.SaveChanges();
            return hd.MaHD;
        }
        public bool Delete(int id)
        {
            try
            {
                var cate = db.HoaDonKhachHangs.Find(id);
                db.HoaDonKhachHangs.Remove(cate);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool Edit(HoaDonKhachHang idhd)
        {
            try
            {
                var tv = db.HoaDonKhachHangs.Find(idhd);
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
        public HoaDonKhachHang GetByID(int id)
        {
            return db.HoaDonKhachHangs.Find(id);
        }
        public IEnumerable<HoaDonKhachHang> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<HoaDonKhachHang> model = db.HoaDonKhachHangs;
            if (!string.IsNullOrEmpty(searchString))
            {
                //model = model.Where(x => x.MaDat.Contains(searchString) || x.MaDat.Contains(searchString));
            }

            return model.OrderByDescending(x => x.MaHD).ToPagedList(page, pageSize);
        }
    }
}