using AFModels;
using BDS.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDS.DAO
{
    public class QuanLyDatThueKhachHangDAO
    {
        BatDongSanContext db = new BatDongSanContext();
        public List<QuanLyDatThueKhachHangViewModel> list()
        {
            List<QuanLyDatThueKhachHangViewModel> kq = (from qldt in db.QuanLyDatThues
                                               select new QuanLyDatThueKhachHangViewModel
                                               {
                                                   MaDat = qldt.MaDat,
                                                   MaKH = qldt.MaKH,
                                                   MaBDS = qldt.MaBDS,
                                                   NgayDat = qldt.NgayDat,
                                                   GhiChu = qldt.GhiChu,
                                                   ThanhToan = qldt.ThanhToan

                                               }).ToList();
            return kq;
        }


        public IEnumerable<QuanLyDatThueKhachHang> ListAll(int pageNumber, int pageSize)
        {
            return db.QuanLyDatThueKhachHangs.OrderByDescending(s => s.MaDat).ToPagedList(pageNumber, pageSize);
        }
        public long Insert(QuanLyDatThueKhachHang qldt)
        {
            db.QuanLyDatThueKhachHangs.Add(qldt);
            db.SaveChanges();
            return qldt.MaDat;
        }
        public bool Delete(int id)
        {
            try
            {
                var cate = db.QuanLyDatThueKhachHangs.Find(id);
                db.QuanLyDatThueKhachHangs.Remove(cate);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool Edit(QuanLyDatThueKhachHang idql)
        {
            try
            {
                var tv = db.QuanLyDatThueKhachHangs.Find(idql.MaDat);
                tv.MaKH = idql.MaKH;
                tv.MaBDS = idql.MaBDS;
                tv.NgayDat = idql.NgayDat;
                tv.GhiChu = idql.GhiChu;
                tv.ThanhToan = idql.ThanhToan;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public QuanLyDatThueKhachHang GetByID(int id)
        {
            return db.QuanLyDatThueKhachHangs.Find(id);
        }
        public IEnumerable<QuanLyDatThueKhachHang> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<QuanLyDatThueKhachHang> model = db.QuanLyDatThueKhachHangs;
            if (!string.IsNullOrEmpty(searchString))
            {
                //model = model.Where(x => x.MaDat.Contains(searchString) || x.MaDat.Contains(searchString));
            }

            return model.OrderByDescending(x => x.MaDat).ToPagedList(page, pageSize);
        }
    }
}