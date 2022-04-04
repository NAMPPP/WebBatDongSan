using AFModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BDS.Areas.User.Controllers
{
    public class TimKiemController : Controller
    {
        BatDongSanContext db = new BatDongSanContext();

        [HttpPost]
        public ActionResult KetQuaTimKiem(FormCollection f, int? page)
        {
            string sTuKhoa = f["txtTimKiem"].ToString();
            ViewBag.TuKhoa = sTuKhoa;
            List<BatDongSan> lstKQTK = db.BatDongSans.Where(n => n.TenBDS.Contains(sTuKhoa)).ToList();
            //Phân trang
            int pageNumber = (page ?? 1);
            int pageSize = 9;
            if (lstKQTK.Count == 0)
            {
                ViewBag.ThongBao = "Không tìm thấy sản phẩm nào";
                //return View(db.BatDongSans.OrderBy(n => n.TenBDS).ToPagedList(pageNumber, pageSize));
            }
            ViewBag.ThongBao = "Đã tìm thấy " + lstKQTK.Count + " kết quả!";
            return View(lstKQTK.OrderBy(n => n.TenBDS).ToPagedList(pageNumber, pageSize));
        }
        //[HttpGet]
        //public ActionResult KetQuaTimKiem(int? page, string sTuKhoa)
        //{
        //    ViewBag.TuKhoa = sTuKhoa;
        //    List<BatDongSan> lstKQTK = db.BatDongSans.Where(n => n.TenBDS.Contains(sTuKhoa)).ToList();
        //    //Phân trang
        //    int pageNumber = (page ?? 1);
        //    int pageSize = 9;
        //    if (lstKQTK.Count == 0)
        //    {
        //        ViewBag.ThongBao = "Không tìm thấy sản phẩm nào";
        //        return View(db.BatDongSans.OrderBy(n => n.TenBDS).ToPagedList(pageNumber, pageSize));
        //    }
        //    ViewBag.ThongBao = "Đã tìm thấy " + lstKQTK.Count + " kết quả!";
        //    return View(lstKQTK.OrderBy(n => n.TenBDS).ToPagedList(pageNumber, pageSize));
        //}
    }
}