using AFModels;
using BDS.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BDS.Areas.Admin.Controllers
{
    public class LoaiBatDongSanController : Controller
    {
        // GET: Admin/LoaiBatDongSan
        public ActionResult Index()
        {
            return View();
        }

        LoaiBatDongSanDAO loaibatdongsanDAO = new LoaiBatDongSanDAO();
        BatDongSanContext db = new BatDongSanContext();
        public ActionResult LoaiBatDongSan(int pageNumber = 1, int pageSize = 5)
        {
            if (Session["User"] != null)
            {
                var pt = loaibatdongsanDAO.ListAll(pageNumber, pageSize);
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
        public ActionResult Create(LoaiBatDongSan model)
        {
            if (ModelState.IsValid)
            {
                var id = new LoaiBatDongSanDAO().Insert(model);
                if (id > 0)
                {
                    return RedirectToAction("LoaiBatDongSan");
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
            new LoaiBatDongSanDAO().Delete(id);
            return RedirectToAction("LoaiBatDongSan");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var idtheloai = new LoaiBatDongSanDAO().GetByID(id);
            return View(idtheloai);
        }

        [HttpPost]
        public ActionResult Edit(LoaiBatDongSan idtheloai)
        {
            if (ModelState.IsValid)
            {
                var dao = new LoaiBatDongSanDAO();
                if (dao.Edit(idtheloai))
                {
                    return RedirectToAction("LoaiBatDongSan", "LoaiBatDongSan");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            return View("LoaiBatDongSan");
        }

    }
}