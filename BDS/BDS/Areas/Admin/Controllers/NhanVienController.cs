using AFModels;
using BDS.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BDS.Areas.Admin.Controllers
{
    public class NhanVienController : Controller
    {
        // GET: Admin/NhanVien
        public ActionResult Index()
        {
            return View();
        }
        NhanVienDAO nhanvienDAO = new NhanVienDAO();
        BatDongSanContext db = new BatDongSanContext();
        public ActionResult NhanVien(int pageNumber = 1, int pageSize = 5)
        {

            if (Session["User"] != null)
            {
                var pt = nhanvienDAO.ListAll(pageNumber, pageSize);
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
        public ActionResult Create(NhanVien model)
        {
            if (ModelState.IsValid)
            {
                var id = new NhanVienDAO().Insert(model);
                if (id > 0)
                {
                    return RedirectToAction("NhanVien");
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
            new NhanVienDAO().Delete(id);
            return RedirectToAction("NhanVien");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var idnv = new NhanVienDAO().GetByID(id);
            return View(idnv);
        }

        [HttpPost]
        public ActionResult Edit(NhanVien idnv)
        {
            if (ModelState.IsValid)
            {
                var dao = new NhanVienDAO();
                if (dao.Edit(idnv))
                {
                    return RedirectToAction("NhanVien", "NhanVien");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            return View("NhanVien");
        }
    }
}