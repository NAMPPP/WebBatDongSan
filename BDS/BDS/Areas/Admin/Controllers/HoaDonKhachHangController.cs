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
    public class HoaDonKhachHangController : Controller
    {
        // GET: Admin/HoaDonKhachHang
        public ActionResult Index()
        {
            return View();
        }
        HoaDonKhachHangDAO hdDAO = new HoaDonKhachHangDAO();
        BatDongSanContext db = new BatDongSanContext();
        public ActionResult HoaDonKhachHang(int pageNumber = 1, int pageSize = 5)
        {

            if (Session["User"] != null)
            {
                var pt = hdDAO.ListAll(pageNumber, pageSize);
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
        public ActionResult Create(HoaDonKhachHang model)
        {
            if (ModelState.IsValid)
            {
                var id = new HoaDonKhachHangDAO().Insert(model);
                if (id > 0)
                {
                    return RedirectToAction("HoaDonKhachHang");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới không thành công");
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int MaHD)
        {
            //Lấy ra đối tượng sách theo mã 
            HoaDonKhachHang h = db.HoaDonKhachHangs.SingleOrDefault(n => n.MaHD == MaHD);
            if (h == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(h);
        }
        [HttpPost, ActionName("Delete")]

        public ActionResult XacNhanXoa(int MaHD)
        {
            HoaDonKhachHang h = db.HoaDonKhachHangs.SingleOrDefault(n => n.MaHD == MaHD);
            if (h == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.HoaDonKhachHangs.Remove(h);
            db.SaveChanges();
            return RedirectToAction("HoaDonKhachHang");

        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var idnv = new HoaDonKhachHangDAO().GetByID(id);
            return View(idnv);
        }

        [HttpPost]
        public ActionResult Edit(HoaDonKhachHang idnv)
        {
            if (ModelState.IsValid)
            {
                var dao = new HoaDonKhachHangDAO();
                if (dao.Edit(idnv))
                {
                    return RedirectToAction("HoaDonKhachHang", "HoaDonKhachHang");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            return View("HoaDonKhachHang");
        }


        public ActionResult ExportToWord(int id)
        {
            HoaDonKhachHangViewModel data = (from hd in db.HoaDonKhachHangs
                                    join tv in db.KhachHangs
                                    on hd.MaKH equals tv.MaKH
                                    join nv in db.NhanViens
                                    on hd.MaNV equals nv.MaNV
                                    join bds in db.BatDongSans
                                    on hd.MaBDS equals bds.MaBDS
                                    join loai in db.LoaiBatDongSans
                                    on bds.MaLoaiBDS equals loai.MaLoaiBDS
                                    where hd.MaHD == id
                                    select new HoaDonKhachHangViewModel()
                                    {
                                        MaHD = hd.MaHD,
                                        HoTenKH = tv.HoTenKH,
                                        HoTenNV = nv.HoTenNV,
                                        TenBDS = bds.TenBDS,
                                        TongTien = hd.TongTien,
                                        DiaChi = tv.DiaChi,
                                        Email = tv.Email,
                                        TenLoaiBDS = loai.TenLoaiBDS,
                                        NgayThanhToan = hd.NgayThanhToan
                                    }).SingleOrDefault();
            Session["idd"] = data.MaHD;
            return View(data);
        }

        //public ActionResult ExportToWord()
        //{

        //    GridView gv = new GridView();
        //    gv.DataSource = db.BatDongSans.ToList();
        //    gv.DataBind();
        //    Response.ClearContent();
        //    Response.Buffer = true;
        //    Response.AddHeader("content-disposition", "attachment; filename=hoadon.doc");
        //    Response.ContentType = "application/vnd.ms-word ";
        //    Response.Charset = string.Empty;

        //    StringWriter sw = new StringWriter();
        //    HtmlTextWriter htw = new HtmlTextWriter(sw);
        //    gv.RenderControl(htw);
        //    Response.Output.Write(sw.ToString());
        //    Response.Flush();
        //    Response.End();

        //    return RedirectToAction("HoaDon");
        //}

        public ActionResult ExportPDF()
        {

            return new ActionAsPdf("ExportToWord", new { @id = Session["idd"].ToString() })
            {
                FileName = Server.MapPath("~/Admin/Content/HoaDonKhachHang.pdf")
            };
        }
    }
}