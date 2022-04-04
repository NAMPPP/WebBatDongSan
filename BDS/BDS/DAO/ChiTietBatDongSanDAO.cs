using AFModels;
using BDS.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDS.DAO
{
    public class ChiTietBatDongSanDAO
    {
        BatDongSanContext db = new BatDongSanContext();

      
        public IEnumerable<ChiTietBatDongSan> ListAll(int pageNumber, int pageSize)
        {
            return db.ChiTietBatDongSans.OrderBy(s => s.MaBDS).ToPagedList(pageNumber, pageSize);
        }
        public int? Insert(ChiTietBatDongSan bds)
        {
            db.ChiTietBatDongSans.Add(bds);
            db.SaveChanges();
            return bds.MaBDS;
        }
        public bool Delete(int id)
        {
            try
            {
                var cate = db.ChiTietBatDongSans.Find(id);
                db.ChiTietBatDongSans.Remove(cate);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool Edit(ChiTietBatDongSan ct)
        {
            try
            {
                var bds = db.ChiTietBatDongSans.Find(ct.STT);
                bds.Gia = ct.Gia;
                bds.DienTich = ct.DienTich;
                bds.DiaChiBDS = ct.DiaChiBDS;
                bds.Mota = ct.Mota;
                bds.KhuVuc = ct.KhuVuc;
                bds.Anh = ct.Anh;
                bds.PhongNgu = ct.PhongNgu;
                bds.PhongTam = ct.PhongTam;
                bds.Paking = ct.Paking;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public ChiTietBatDongSan GetByID(int id)
        {
            return db.ChiTietBatDongSans.Find(id);
        }
        public IEnumerable<ChiTietBatDongSan> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<ChiTietBatDongSan> model = db.ChiTietBatDongSans;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.KhuVuc.Contains(searchString) || x.KhuVuc.Contains(searchString));
            }

            return model.OrderByDescending(x => x.MaBDS).ToPagedList(page, pageSize);
        }
    }
}