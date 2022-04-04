using AFModels;
using BDS.DAO;
using BDS.ViewModel;
using Rotativa;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BDS.Areas.Admin.Controllers
{
    public class HoaDonController : Controller
    {
        // GET: Admin/HoaDon
        public ActionResult Index()
        {
            return View();
        }
        HoaDonDAO hdDAO = new HoaDonDAO();
        BatDongSanContext db = new BatDongSanContext();
        public ActionResult HoaDon(int pageNumber = 1, int pageSize = 5)
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
        public ActionResult Create(HoaDon model)
        {
            if (ModelState.IsValid)
            {
                var id = new HoaDonDAO().Insert(model);
                if (id > 0)
                {
                    return RedirectToAction("HoaDon");
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
            HoaDon h = db.HoaDons.SingleOrDefault(n => n.MaHD == MaHD);
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
            HoaDon h = db.HoaDons.SingleOrDefault(n => n.MaHD == MaHD);
            if (h == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.HoaDons.Remove(h);
            db.SaveChanges();
            return RedirectToAction("HoaDon");

        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var idnv = new HoaDonDAO().GetByID(id);
            return View(idnv);
        }

        [HttpPost]
        public ActionResult Edit(HoaDon idnv)
        {
            if (ModelState.IsValid)
            {
                var dao = new HoaDonDAO();
                if (dao.Edit(idnv))
                {
                    return RedirectToAction("HoaDon", "HoaDon");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            return View("HoaDon");
        }


        public ActionResult ExportToWord(int id)
        {
            
            HoaDonViewModel data = (from hd in db.HoaDons
                                join tv in db.ThanhViens 
                                on hd.MaKH equals tv.MaTV
                                join nv in db.NhanViens 
                                on hd.MaNV equals nv.MaNV
                                join bds in db.BatDongSans 
                                on hd.MaBDS equals bds.MaBDS
                                join loai in db.LoaiBatDongSans
                                on bds.MaLoaiBDS equals loai.MaLoaiBDS
                                where hd.MaHD == id
                                select new HoaDonViewModel()
                                     {
                                           MaHD = hd.MaHD,
                                           HoTenTV = tv.HoTenTV,
                                           HoTenNV = nv.HoTenNV,
                                           TenBDS = bds.TenBDS,
                                           TongTien = hd.TongTien,
                                           DiaChi = tv.DiaChi,
                                           Email = tv.Email,
                                           TenLoaiBDS = loai.TenLoaiBDS,
                                           NgayThanhToan = hd.NgayThanhToan
                                    }).SingleOrDefault();
            Session["id"] = data.MaHD;
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

            return new ActionAsPdf("ExportToWord",new { @id = Session["id"].ToString()})
            {
                FileName = Server.MapPath("~/Admin/Content/HoaDon.pdf")
            };
        }

    }
}