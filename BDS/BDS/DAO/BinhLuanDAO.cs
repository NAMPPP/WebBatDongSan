using AFModels;
using BDS.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDS.DAO
{
    public class BinhLuanDAO
    {
        BatDongSanContext db = new BatDongSanContext();
        public List<BinhLuanViewModel> list()
        {
            List<BinhLuanViewModel> kq = (from bl in db.BinhLuans
                                           select new BinhLuanViewModel
                                           {
                                               MaBL = bl.MaBL,
                                               TenBL = bl.TenBL,
                                               NoiDungBL = bl.NoiDungBL,
                                               MaBDS = bl.MaBDS,
                                               NgayBL = bl.NgayBL,
                                               Duyet = bl.Duyet

                                           }).ToList();
            return kq;
        }


        public IEnumerable<BinhLuan> ListAll(int pageNumber, int pageSize)
        {
            return db.BinhLuans.OrderByDescending(s => s.MaBL).ToPagedList(pageNumber, pageSize);
        }
        public long Insert(BinhLuan bl)
        {
            db.BinhLuans.Add(bl);
            db.SaveChanges();
            return bl.MaBL;
        }
        public bool Delete(int id)
        {
            try
            {
                var cate = db.BinhLuans.Find(id);
                db.BinhLuans.Remove(cate);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool Edit(BinhLuan idbl)
        {
            try
            {
                var tv = db.BinhLuans.Find(idbl.MaBL);
                tv.TenBL = idbl.TenBL;
                tv.NoiDungBL= idbl.NoiDungBL;
                tv.MaBDS = idbl.MaBDS;
                tv.Duyet = idbl.Duyet;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public BinhLuan GetByID(int id)
        {
            return db.BinhLuans.Find(id);
        }
        public IEnumerable<BinhLuan> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<BinhLuan> model = db.BinhLuans;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.TenBL.Contains(searchString) || x.TenBL.Contains(searchString));
            }

            return model.OrderByDescending(x => x.MaBL).ToPagedList(page, pageSize);
        }
    }
}