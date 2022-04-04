using AFModels;
using BDS.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BDS.Areas.Admin.Controllers
{
    public class KhachHangController : Controller
    {
        // GET: Admin/KhachHang
        public ActionResult Index()
        {
            return View();
        }

        KhachHangDAO khachhangDAO = new KhachHangDAO();
        BatDongSanContext db = new BatDongSanContext();
        public ActionResult KhachHang(int pageNumber = 1, int pageSize = 5)
        {
            if (Session["User"] != null)
            {
                var pt = khachhangDAO.ListAll(pageNumber, pageSize);
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
        [ValidateInput(false)]
        public ActionResult Create(KhachHang model)
        {
            if (ModelState.IsValid)
            {
                var id = new KhachHangDAO().Insert(model);
                if (id > 0)
                {

                    return RedirectToAction("KhachHang");
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
            new KhachHangDAO().Delete(id);
            return RedirectToAction("KhachHang");
        }



        [HttpGet]
        public ActionResult Edit(int id)
        {
            var idtheloai = new KhachHangDAO().GetByID(id);
            return View(idtheloai);
        }


        [HttpPost]
        public ActionResult Edit(KhachHang idtheloai)
        {
            if (ModelState.IsValid)
            {
                var dao = new KhachHangDAO();
                if (dao.Edit(idtheloai))
                {
                    return RedirectToAction("KhachHang", "KhachHang");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            return View("KhachHang");
        }
    }
}