using AFModels;
using BDS.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BDS.Areas.Admin.Controllers
{
    public class ThanhVienController : Controller
    {
        // GET: Admin/ThanhVien
        public ActionResult Index()
        {
            return View();
        }

        ThanhVienDAO thanhvienDAO = new ThanhVienDAO();
        BatDongSanContext db = new BatDongSanContext();
        public ActionResult ThanhVien(int pageNumber = 1, int pageSize = 5)
        {
            if (Session["User"] != null)
            {
                var pt = thanhvienDAO.ListAll(pageNumber, pageSize);
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
        public ActionResult Create(ThanhVien model)
        {
            if (ModelState.IsValid)
            {
                var id = new ThanhVienDAO().Insert(model);
                if (id > 0)
                {
                    return RedirectToAction("ThanhVien");
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
            new ThanhVienDAO().Delete(id);
            return RedirectToAction("ThanhVien");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var idnv = new ThanhVienDAO().GetByID(id);
            return View(idnv);
        }

        [HttpPost]
        public ActionResult Edit(ThanhVien idnv)
        {
            if (ModelState.IsValid)
            {
                var dao = new ThanhVienDAO();
                if (dao.Edit(idnv))
                {
                    return RedirectToAction("ThanhVien", "ThanhVien");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            return View("ThanhVien");
        }

        [HttpPost, ActionName("DuyetThanhVien")]
        public ActionResult DuyetThanhVien(string user, string pw)
        {
            // Tìm xem user đã có trong quản trị hay chưa?
            QuanTri kq = (from a in db.QuanTris
                           where a.UserName == user
                           select a).SingleOrDefault();
            if (kq!=null)
            {
                return RedirectToAction("ThanhVien", "ThanhVien");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    QuanTri qt = new QuanTri();
                    qt.UserName = user;
                    qt.Pw = pw;
                    qt.MaPQ = 2;
                    db.QuanTris.Add(qt);
                    db.SaveChanges();
                    return RedirectToAction("QuanTri", "QuanTri");

                }
            }
            
            return View();
        }
    }
}
