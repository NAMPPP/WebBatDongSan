using AFModels;
using BDS.DAO;
using BDS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BDS.Areas.User.Controllers
{
    public class BinhLuanController : Controller
    {
        // GET: User/BinhLuan
        public ActionResult Index()
        {
            return View();
        }

        BinhLuanDAO binhLuanDAO = new BinhLuanDAO();
        BatDongSanContext db = new BatDongSanContext();

        public ActionResult DanhGia(int? bds)
        {
            List<BinhLuanViewModel> kq = (from bl in db.BinhLuans
                                          where bl.Duyet == 1 && bl.MaBDS == bds
                                          select new BinhLuanViewModel()
                                          {
                                              TenBL = bl.TenBL,
                                              NoiDungBL = bl.NoiDungBL,
                                              NgayBL = bl.NgayBL
                                            }).ToList();
            return View(kq);
        }


        [HttpGet]
        public ActionResult Create(int id)
        {
            Session["BL"] = null;
            Session["BL"] = id;
            return View();
        }

        [HttpPost]
        public ActionResult Create(BinhLuan model)
        {
            if (ModelState.IsValid)
            {
                BinhLuan bl = new BinhLuan();
                bl.TenBL = model.TenBL;
                bl.NoiDungBL = model.NoiDungBL;
                bl.MaBDS = model.MaBDS;
                bl.NgayBL= DateTime.Now;
                bl.Duyet = 0;
                db.BinhLuans.Add(bl);
                db.SaveChanges();
                return RedirectToAction("Index", "User");
            }
            return RedirectToAction("BatDongSan"); ;
        }

        public ActionResult Delete(int id)
        {
            new BinhLuanDAO().Delete(id);
            return RedirectToAction("BinhLuan");
        }

        //[HttpGet]
        //public ActionResult Edit(int id)
        //{
        //    var idbl = new BinhLuanDAO().GetByID(id);
        //    return View(idbl);
        //}

        //[HttpPost]
        //public ActionResult Edit(BinhLuan idnv)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var dao = new BinhLuanDAO();
        //        if (dao.Edit(idnv))
        //        {
        //            return RedirectToAction("BinhLuan", "BinhLuan");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Cập nhật không thành công");
        //        }
        //    }
        //    return View("BinhLuan");
        //}
    }
}