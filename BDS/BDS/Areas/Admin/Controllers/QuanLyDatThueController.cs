using AFModels;
using BDS.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BDS.Areas.Admin.Controllers
{
    public class QuanLyDatThueController : Controller
    {
        // GET: Admin/QuanLyDatThue
        public ActionResult Index()
        {
            return View();
        }

        QuanLyDatThueDAO qldtDAO = new QuanLyDatThueDAO();
        BatDongSanContext db = new BatDongSanContext();
        public ActionResult QuanLyDatThue(int pageNumber = 1, int pageSize = 5)
        {
            if (Session["User"] != null)
            {
                var pt = qldtDAO.ListAll(pageNumber, pageSize);
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
        public ActionResult Create(QuanLyDatThue model)
        {
            if (ModelState.IsValid)
            {
                var id = new QuanLyDatThueDAO().Insert(model);
                if (id > 0)
                {
                    return RedirectToAction("QuanLyDatThue");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới không thành công");
                }
            }
            return View(model);
        }
        [HttpPost, ActionName("XuatHoaDon")]
        public ActionResult XuatHoaDon(int madat,int mabds,int makh)
        {
            var tv = db.QuanLyDatThues.Find(madat);
            tv.MaKH = makh;
            tv.MaBDS = mabds;
            tv.NgayDat = DateTime.Now;
            tv.GhiChu = "Đặt thuê";
            tv.ThanhToan = 1;
            db.SaveChanges();


            string user = Session["User"].ToString();
            NhanVien qt = (from a in db.NhanViens
                           join b in db.QuanTris
                           on a.UserName equals b.UserName
                          where b.UserName == user
                          select a).SingleOrDefault();
            ChiTietBatDongSan ct = (from a in db.ChiTietBatDongSans
                                    where a.MaBDS == mabds
                                    select a).SingleOrDefault();
            if (ModelState.IsValid)
            {
                HoaDon hd = new HoaDon();
                hd.MaKH = makh;
                hd.MaNV = qt.MaNV;
                hd.MaBDS = mabds;
                hd.TongTien = ct.Gia;
                hd.NgayThanhToan = DateTime.Now;
                db.HoaDons.Add(hd);
                db.SaveChanges();
                return RedirectToAction("HoaDon", "HoaDon");

            }
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int MaDat)
        {
            //Lấy ra đối tượng sách theo mã 
            QuanLyDatThue h = db.QuanLyDatThues.SingleOrDefault(n => n.MaDat == MaDat);
            if (h == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(h);
        }
        [HttpPost, ActionName("Delete")]

        public ActionResult XacNhanXoa(int MaDat)
        {
            QuanLyDatThue h = db.QuanLyDatThues.SingleOrDefault(n => n.MaDat == MaDat);
            if (h == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.QuanLyDatThues.Remove(h);
            db.SaveChanges();
            return RedirectToAction("QuanLyDatThue");

        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var idnv = new QuanLyDatThueDAO().GetByID(id);
            return View(idnv);
        }

        [HttpPost]
        public ActionResult Edit(QuanLyDatThue idnv)
        {
            if (ModelState.IsValid)
            {
                var dao = new QuanLyDatThueDAO();
                if (dao.Edit(idnv))
                {
                    return RedirectToAction("QuanLyDatThue", "QuanLyDatThue");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            return View("QuanLyDatThue");
        }
    }
}