using AFModels;
using BDS.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDS.DAO
{
    public class QuanTriDAO
    {
        BatDongSanContext db = new BatDongSanContext();
        public List<QuanTriViewModel> list()
        {
            List<QuanTriViewModel> kq = (from qt in db.QuanTris
                                         select new QuanTriViewModel
                                         {
                                             UserName = qt.UserName,
                                             Pw = qt.Pw,
                                             MaPQ = qt.MaPQ
                                        
                                                }).ToList();
            return kq;
        }

        public IEnumerable<QuanTri> ListAll(int pageNumber, int pageSize)
        {
            return db.QuanTris.OrderBy(s => s.UserName).ToPagedList(pageNumber, pageSize);
        }
        public string Insert(QuanTri qt)
        {
            db.QuanTris.Add(qt);
            db.SaveChanges();
            return qt.UserName;
        }

        public string Delete(String id)
        {
            try
            {
                var cate = db.QuanTris.Find(id);
                db.QuanTris.Remove(cate);
                db.SaveChanges();
                return id;
            }
            catch (Exception)
            {
                return id;
            }

        }

        public bool ChangePw(QuanTri qt,String pw)
        {
                var q = db.QuanTris.Find(qt.UserName);
                q.Pw = pw;
                db.SaveChanges();
                return true;
        }


        public bool Edit(QuanTri qt)
        {
            try
            {
                var q = db.QuanTris.Find(qt.UserName);
                q.Pw = qt.Pw;
                q.MaPQ = qt.MaPQ;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public QuanTri GetByID(String id)
        {
            return db.QuanTris.Find(id);
        }
        public IEnumerable<QuanTri> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<QuanTri> model = db.QuanTris;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserName.Contains(searchString) || x.UserName.Contains(searchString));
            }

            return model.OrderByDescending(x => x.UserName).ToPagedList(page, pageSize);
        }
    }
}