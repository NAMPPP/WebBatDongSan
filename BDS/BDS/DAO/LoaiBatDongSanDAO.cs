using AFModels;
using BDS.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDS.DAO
{
    public class LoaiBatDongSanDAO
    {
        BatDongSanContext db = new BatDongSanContext();
        public List<LoaiBatDongSanViewModel> list()
        {
            List<LoaiBatDongSanViewModel> kq = (from lbds in db.LoaiBatDongSans
                                            select new LoaiBatDongSanViewModel
                                            {
                                               MaLoaiBDS = lbds.MaLoaiBDS,
                                               TenLoaiBDS = lbds.TenLoaiBDS
                                            }).ToList();
            return kq;
        }

        public IEnumerable<LoaiBatDongSan> ListAll(int pageNumber, int pageSize)
        {
            return db.LoaiBatDongSans.OrderBy(s => s.MaLoaiBDS).ToPagedList(pageNumber, pageSize);
        }
        public long Insert(LoaiBatDongSan lbds)
        {
            db.LoaiBatDongSans.Add(lbds);
            db.SaveChanges();
            return lbds.MaLoaiBDS;
        }
        public bool Delete(int id)
        {
            try
            {
                var cate = db.LoaiBatDongSans.Find(id);
                db.LoaiBatDongSans.Remove(cate);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool Edit(LoaiBatDongSan idbds)
        {
            try
            {
                var tl = db.LoaiBatDongSans.Find(idbds.MaLoaiBDS);
                tl.TenLoaiBDS = idbds.TenLoaiBDS;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public LoaiBatDongSan GetByID(int id)
        {
            return db.LoaiBatDongSans.Find(id);
        }
        public IEnumerable<LoaiBatDongSan> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<LoaiBatDongSan> model = db.LoaiBatDongSans;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.TenLoaiBDS.Contains(searchString) || x.TenLoaiBDS.Contains(searchString));
            }

            return model.OrderByDescending(x => x.MaLoaiBDS).ToPagedList(page, pageSize);
        }
    }
}