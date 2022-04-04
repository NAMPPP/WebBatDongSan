using AFModels;
using BDS.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDS.DAO
{
    public class QuanLyDatThueDAO
    {
        BatDongSanContext db = new BatDongSanContext();
        public List<QuanLyDatThueViewModel> list()
        {
            List<QuanLyDatThueViewModel> kq = (from qldt in db.QuanLyDatThues
                                           select new QuanLyDatThueViewModel
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


        public IEnumerable<QuanLyDatThue> ListAll(int pageNumber, int pageSize)
        {
            return db.QuanLyDatThues.OrderByDescending(s => s.MaDat).ToPagedList(pageNumber, pageSize);
        }
        public long Insert(QuanLyDatThue qldt)
        {
            db.QuanLyDatThues.Add(qldt);
            db.SaveChanges();
            return qldt.MaDat;
        }
        public bool Delete(int id)
        {
            try
            {
                var cate = db.QuanLyDatThues.Find(id);
                db.QuanLyDatThues.Remove(cate);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool Edit(QuanLyDatThue idql)
        {
            try
            {
                var tv = db.QuanLyDatThues.Find(idql.MaDat);
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
        public QuanLyDatThue GetByID(int id)
        {
            return db.QuanLyDatThues.Find(id);
        }
        public IEnumerable<QuanLyDatThue> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<QuanLyDatThue> model = db.QuanLyDatThues;
            if (!string.IsNullOrEmpty(searchString))
            {
                //model = model.Where(x => x.MaDat.Contains(searchString) || x.MaDat.Contains(searchString));
            }

            return model.OrderByDescending(x => x.MaDat).ToPagedList(page, pageSize);
        }
    }
}