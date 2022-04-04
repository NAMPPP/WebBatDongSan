using AFModels;
using BDS.DAO;
using BDS.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BDS.Areas.Admin.Controllers
{
    public class LienHeController : Controller
    {
        // GET: Admin/LienHe
        public ActionResult Index()
        {
            return View();
        }

        LienHeDAO lienheDAO = new LienHeDAO();
        BatDongSanContext db = new BatDongSanContext();
        public ActionResult LienHe(int pageNumber = 1, int pageSize = 5)
        {
            if (Session["User"] != null)
            {
                var pt = lienheDAO.ListAll(pageNumber, pageSize);
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
        public ActionResult Create(LienHe model)
        {
            if (ModelState.IsValid)
            {
                var id = new LienHeDAO().Insert(model);
                if (id > 0)
                {


                    return RedirectToAction("LienHe");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới không thành công");
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Reply(int? id)
        {
            LienHeViewModel lh = (from a in db.LienHes
                         where a.MaLienHe == id
                         select new LienHeViewModel
                         {
                             Email = a.Email
                         }
                         ).SingleOrDefault();
            Session["reEmail"] = lh.Email;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reply(FormCollection r)
        {
            string toAddress = r["txttoAddress"].ToString();
            string subject = r["txtSubject"].ToString();
            string body = r["txtbody"].ToString();

             

            return View();
        }

        }
    }