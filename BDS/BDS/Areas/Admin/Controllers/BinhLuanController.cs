using AFModels;
using BDS.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BDS.Areas.Admin.Controllers
{
    public class BinhLuanController : Controller
    {
        // GET: Admin/BinhLuan
        public ActionResult Index()
        {
            return View();
        }

        BinhLuanDAO binhLuanDAO = new BinhLuanDAO();
        BatDongSanContext db = new BatDongSanContext();
        public ActionResult BinhLuan(int pageNumber = 1, int pageSize = 5)
        {
            if (Session["User"] != null)
            {
                var pt = binhLuanDAO.ListAll(pageNumber, pageSize);
                return View(pt);
            }
            else
            {
                return RedirectToAction("Login","Login");
            }
            
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(BinhLuan model)
        {
            if (ModelState.IsValid)
            {
                var id = new BinhLuanDAO().Insert(model);
                if (id > 0)
                {
                    return RedirectToAction("BinhLuan");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới không thành công");
                }
            }
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            new BinhLuanDAO().Delete(id);
            return RedirectToAction("BinhLuan");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var idbl = new BinhLuanDAO().GetByID(id);
            return View(idbl);
        }

        [HttpPost]
        public ActionResult Edit(BinhLuan idnv)
        {
            if (ModelState.IsValid)
            {
                var dao = new BinhLuanDAO();
                if (dao.Edit(idnv))
                {
                    return RedirectToAction("BinhLuan", "BinhLuan");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            return View("BinhLuan");
        }

        [HttpPost, ActionName("DuyetBL")]
        public ActionResult DuyetBL(int mabl, string tenbl,string nd, int mabds,DateTime ngaybl)
            {
            if (ModelState.IsValid)
            {
                var bl = db.BinhLuans.Find(mabl);
                bl.MaBL = mabl;
                bl.TenBL = tenbl;
                bl.NoiDungBL = nd;
                bl.MaBDS = mabds;
                bl.NgayBL = ngaybl;
                bl.Duyet = 1;
                db.SaveChanges();
                return RedirectToAction("BinhLuan", "BinhLuan");

            }
            return View();
        }

        [HttpPost, ActionName("BoDuyetBL")]
        public ActionResult BoDuyetBL(int mabl, string tenbl, string nd, int mabds, DateTime ngaybl)
        {
            if (ModelState.IsValid)
            {
                var bl = db.BinhLuans.Find(mabl);
                bl.MaBL = mabl;
                bl.TenBL = tenbl;
                bl.NoiDungBL = nd;
                bl.MaBDS = mabds;
                bl.NgayBL = ngaybl;
                bl.Duyet = 0;
                db.SaveChanges();
                return RedirectToAction("BinhLuan", "BinhLuan");

            }
            return View();
        }
    }
}