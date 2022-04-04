using AFModels;
using BDS.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDS.DAO
{
    public class BatDongSanDAO
    {
        BatDongSanContext db = new BatDongSanContext();
        public List<BatDongSanViewModel> list()
        {
            List<BatDongSanViewModel> kq = (from bds in db.BatDongSans join ct in db.ChiTietBatDongSans
                                           on bds.MaBDS equals ct.MaBDS
                                            where bds.MaBDS == ct.MaBDS
                                         select new BatDongSanViewModel()
                                         {
                                             MaBDS = bds.MaBDS,
                                             TenBDS = bds.TenBDS,
                                             MaNhom = bds.MaNhom,
                                             MaLoaiBDS = bds.MaLoaiBDS,
                                             NgayDang = bds.NgayDang,
                                             UserName = bds.UserName,
                                             Duyet = bds.Duyet,
                                             Gia = ct.Gia,
                                             Mota = ct.Mota,
                                             Anh = ct.Anh,
                                             PhongNgu = ct.PhongNgu,
                                             PhongTam = ct.PhongTam
                                         }).ToList();
            return kq;
        }







        public IEnumerable<BatDongSan> ListAll(int pageNumber, int pageSize)
        {
            return db.BatDongSans.OrderByDescending(s => s.MaBDS).ToPagedList(pageNumber, pageSize);
        }
        public long Insert(BatDongSan bds)
        {
            db.BatDongSans.Add(bds);
            db.SaveChanges();
            return bds.MaBDS;
        }
        public bool Delete(int id)
        {
            try
            {
                var cate = db.BatDongSans.Find(id);
                db.BatDongSans.Remove(cate);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool Edit(BatDongSan idbds)
        {
            try
            {
                var tl = db.BatDongSans.Find(idbds.MaBDS);
                tl.TenBDS = idbds.TenBDS;
                tl.MaNhom = idbds.MaNhom;
                tl.MaLoaiBDS = idbds.MaLoaiBDS;
                tl.NgayDang = DateTime.Now;
                tl.UserName = idbds.UserName;     
                tl.Duyet = idbds.Duyet;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public ChiTietBDSViewModel Detail(int id)
        {
            ChiTietBDSViewModel p = (from bds in db.ChiTietBatDongSans
                                     where bds.MaBDS == id
                                     select new ChiTietBDSViewModel
                                     {
                                         STT = bds.STT,
                                         MaBDS = bds.MaBDS,
                                         Gia = bds.Gia,
                                         DienTich = bds.DienTich,
                                         DiaChiBDS = bds.DiaChiBDS,
                                         Mota = bds.Mota,
                                         KhuVuc = bds.KhuVuc,
                                         Anh = bds.Anh,
                                         PhongNgu = bds.PhongNgu,
                                         PhongTam = bds.PhongTam,
                                         Paking = bds.Paking

                                     }).SingleOrDefault();
            return p;
        }


        public BatDongSan GetByID(int id)
        {
            return db.BatDongSans.Find(id);
        }
        public IEnumerable<BatDongSan> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<BatDongSan> model = db.BatDongSans;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.TenBDS.Contains(searchString) || x.TenBDS.Contains(searchString));
            }

            return model.OrderByDescending(x => x.MaBDS).ToPagedList(page, pageSize);
        }
    }
}