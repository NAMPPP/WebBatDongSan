using AFModels;
using BDS.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BDS.Areas.Admin.Controllers
{
    public class ChiTietBatDongSanController : Controller
    {

        ChiTietBatDongSanDAO batdongsanDAO = new ChiTietBatDongSanDAO();
        BatDongSanContext db = new BatDongSanContext();
        // GET: Admin/ChiTietBatDongSan
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DeleteDetail(int id)
        {
            new ChiTietBatDongSanDAO().Delete(id);
            return RedirectToAction("BatDongSan","BatDongSan");
        }

        [HttpGet]
        public ActionResult EditDetail(int id)
        {
            var idd = new ChiTietBatDongSanDAO().GetByID(id);
            return View(idd);
        }


        [HttpPost]
        public ActionResult EditDetail(ChiTietBatDongSan idbds)
        {
            if (ModelState.IsValid)
            {
                var dao = new ChiTietBatDongSanDAO();
                if (dao.Edit(idbds))
                {
                    return RedirectToAction("BatDongSan","BatDongSan");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            return View("BatDongSan");
        }

    }
}