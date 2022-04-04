using AFModels;
using BDS.DAO;
using BDS.ViewModel;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BDS.Areas.Admin.Controllers
{
    public class BatDongSanController : Controller
    {

        // GET: Admin/BatDongSan
        public ActionResult Index()
        {
            return View();
        }
        BatDongSanDAO batdongsanDAO = new BatDongSanDAO();
        BatDongSanContext db = new BatDongSanContext();
        public ActionResult BatDongSan(int pageNumber = 1, int pageSize = 5)
        {
            if (Session["User"] != null)
            {
                var pt = batdongsanDAO.ListAll(pageNumber, pageSize);
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
            ViewBag.MaNhom = new SelectList(db.NhomLoais.ToList().OrderBy(n => n.TenNhom), "MaNhom", "TenNhom");
            ViewBag.MaLoaiBDS = new SelectList(db.LoaiBatDongSans.ToList().OrderBy(n => n.TenLoaiBDS), "MaLoaiBDS", "TenLoaiBDS");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(BatDongSan model)
        {
            ViewBag.MaNhom = new SelectList(db.NhomLoais.ToList().OrderBy(n => n.TenNhom), "MaNhom", "TenNhom");
            ViewBag.MaLoaiBDS = new SelectList(db.LoaiBatDongSans.ToList().OrderBy(n => n.TenLoaiBDS), "MaLoaiBDS", "TenLoaiBDS");
            if (ModelState.IsValid)
            {

                BatDongSan bds = new BatDongSan();
                bds.TenBDS = model.TenBDS;
                bds.MaNhom = model.MaNhom;
                bds.MaLoaiBDS = model.MaLoaiBDS;
                bds.NgayDang = DateTime.Now;
                bds.UserName = Session["User"].ToString();
                bds.Duyet = model.Duyet;
                db.BatDongSans.Add(bds);
                db.SaveChanges();
                Session["MaBDS"] = null;
                Session["MaBDS"] = bds.MaBDS;
                return RedirectToAction("CreateDetail", "BatDongSan", new { @id =Session["MaBDS"].ToString()});
                //return RedirectToAction("BatDongSan", "BatDongSan");
            }
            return RedirectToAction("BatDongSan"); ;
        }

        [HttpGet]
        public ActionResult CreateDetail()
        {

            return View();
        }

        [HttpPost]
        public ActionResult CreateDetail(ChiTietBatDongSan model)
        {

                if (ModelState.IsValid)
                {
                    var id = new ChiTietBatDongSanDAO().Insert(model);
                    if (id > 0)
                    {
                        return RedirectToAction("BatDongSan");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm mới không thành công");
                    }
                }
            
           
            return View();
        }

        public ActionResult Delete(int id)
        {
            new BatDongSanDAO().Delete(id);
            return RedirectToAction("BatDongSan");
        }



        [HttpGet]
        public ActionResult Edit(int id)
        {
            var idtheloai = new BatDongSanDAO().GetByID(id);
            return View(idtheloai);
        }


        [HttpPost]
        public ActionResult Edit(BatDongSan idtheloai)
        {
            if (ModelState.IsValid)
            {
                var dao = new BatDongSanDAO();
                if (dao.Edit(idtheloai))
                {
                    return RedirectToAction("BatDongSan", "BatDongSan");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            return View("BatDongSan");
        }




        public ActionResult Detail(int? id)
        {
            if (id.HasValue)
            {
                ChiTietBDSViewModel p = batdongsanDAO.Detail(id.Value);
                if (p == null)
                {
                    Session["MaBDS"] = null;
                    Session["MaBDS"] = id;
                    return RedirectToAction("CreateDetail", "BatDongSan", new { @id = id });
                }
                return View(p);
            }
            else
                return new HttpNotFoundResult("Không tìm thấy trang này!");
        }

        [HttpPost, ActionName("DuyetBDS")]
        public ActionResult DuyetBDS(int mabds, int? maloai,string tenbds, int? manhom, DateTime? ngaydang, string user,int? duyet)
        {
            if (ModelState.IsValid)
            {
                var bds = db.BatDongSans.Find(mabds);
                bds.MaBDS = mabds;
                bds.TenBDS = tenbds;
                bds.MaNhom = manhom;
                bds.MaLoaiBDS = maloai;
                bds.NgayDang = ngaydang;
                bds.UserName = user;
                bds.Duyet = 1;
                db.SaveChanges();
                return RedirectToAction("BatDongSan", "BatDongSan");

            }
            return View();
        }

        [HttpPost, ActionName("BoDuyetBDS")]
        public ActionResult BoDuyetBDS(int mabds, int? maloai, string tenbds, int? manhom, DateTime? ngaydang, string user, int? duyet)
        {
            if (ModelState.IsValid)
            {
                var bds = db.BatDongSans.Find(mabds);
                bds.MaBDS = mabds;
                bds.TenBDS = tenbds;
                bds.MaNhom = manhom;
                bds.MaLoaiBDS = maloai;
                bds.NgayDang = ngaydang;
                bds.UserName = user;
                bds.Duyet = 0;
                db.SaveChanges();
                return RedirectToAction("BatDongSan", "BatDongSan");

            }
            return View();
        }

        public ActionResult ExportPDF()
        {
            return new ActionAsPdf("BatDongSan")
            {
                FileName = Server.MapPath("~/Admin/Content/HoaDon.pdf")
            };
        }

    }
}