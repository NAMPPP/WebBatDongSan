using AFModels;
using BDS.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BDS.Areas.Admin.Controllers
{
    public class QuanTriController : Controller
    {
        // GET: Admin/QuanTri
        public ActionResult Index()
        {
            return View();
        }

        QuanTriDAO quantriDAO = new QuanTriDAO();
        BatDongSanContext db = new BatDongSanContext();
        public ActionResult QuanTri(int pageNumber = 1, int pageSize = 5)
        {
            if (Session["User"] != null)
            {
                var pt = quantriDAO.ListAll(pageNumber, pageSize);
                return View(pt);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(QuanTri model)
        {
            if (ModelState.IsValid)
            {
                var id = new QuanTriDAO().Insert(model);
                if (id != "")
                {
                    return RedirectToAction("QuanTri");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới không thành công");
                }
            }
            return View(model);
        }
        public ActionResult Delete(String id)
        {
            new QuanTriDAO().Delete(id);
            return RedirectToAction("QuanTri");
        }

        [HttpGet]
        public ActionResult Edit(String id)
        {
            var idtheloai = new QuanTriDAO().GetByID(id);
            return View(idtheloai);
        }

        [HttpPost]
        public ActionResult Edit(QuanTri us)
        {
            if (ModelState.IsValid)
            {
                var dao = new QuanTriDAO();
                if (dao.Edit(us))
                {
                    return RedirectToAction("QuanTri", "QuanTri");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            return View("QuanTri");
        }

    }
}