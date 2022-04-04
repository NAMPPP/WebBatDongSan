using AFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BDS.Areas.Admin.Controllers
{
    public class AuthenticationController : Controller
    {
        BatDongSanContext db = new BatDongSanContext();
        // GET: Admin/Authentication
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            if (ModelState.IsValid)
            {
                string sTaiKhoan = f["txtTaiKhoan"].ToString();
                string sMatKhau = f.Get("txtMatKhau").ToString();
                QuanTri qt = (from a in db.QuanTris
                              where a.UserName == sTaiKhoan && a.Pw == sMatKhau
                              select a).SingleOrDefault();
                if (qt != null)
                {
                    if (qt.MaPQ == 1)
                    {
                        ViewBag.ThongBao = "Chúc mừng Admin " + qt.UserName + " đã đăng nhập thành công !";
                        FormsAuthentication.SetAuthCookie(qt.UserName, false);
                        Session["TaiKhoan"] = qt;
                        return RedirectToAction("Index", "Login");
                    }
                    else
                    {
                        ViewBag.ThongBao = "Chúc mừng bạn " + qt.UserName + " đã đăng nhập thành công !";
                        FormsAuthentication.SetAuthCookie(qt.UserName, false);
                        Session["TaiKhoan"] = qt;
                        return RedirectToAction("Index", "Login");
                    }
                }
            }
            ViewBag.ThongBao = "Tên tài khoản hoặc mật khẩu không đúng!";
            ModelState.AddModelError("CredentialError", "Invalid Username or Password");

            return View();
        }


        
    }
}