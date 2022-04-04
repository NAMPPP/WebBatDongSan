using AFModels;
using BDS.DAO;
using BDS.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BDS.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        BatDongSanContext db = new BatDongSanContext();
        LoginDAO loginDao = new LoginDAO();
        [Authorize]
        public ActionResult Index()
        {
            if (Session["User"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Login(FormCollection f)
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
                    ViewBag.ThongBao = "Hi, Admin:" +qt.UserName;
                    FormsAuthentication.SetAuthCookie(qt.UserName, false);
                    Session["User"] = qt.UserName;
                    Session["Pw"] = qt.Pw;
                    return RedirectToAction("Index","Login");
                }
                else
                {
                    ViewBag.ThongBao = "Hi " + qt.UserName;
                    FormsAuthentication.SetAuthCookie(qt.UserName, false);
                    Session["User"] = qt.UserName;
                    Session["Pw"] = qt.Pw;
                    return RedirectToAction("Index", "Login");
                }
            }
            ViewBag.ThongBao = "Tên tài khoản hoặc mật khẩu không đúng!";
            return View();
        }


        public ActionResult ChangePass()
        {
            return View();
        }
        public ActionResult ForgotPass()
        {
            return View();
        }



        [HttpPost]
        public ActionResult ChangePass(FormCollection f)
        {
            string sUser = Session["User"].ToString();
            string sPw = Session["Pw"].ToString();
            string sMatKhauCu = f.Get("txtMatKhau").ToString();
            string reMatKhau = f.Get("txtreMatKhau").ToString();
            QuanTri qt = (from a in db.QuanTris
                          where a.UserName == sUser && a.Pw == sMatKhauCu
                          select a).SingleOrDefault();
            if (qt != null)
            {
                if(sPw==sMatKhauCu)
                { 

                if (ModelState.IsValid)
                    {
                        var dao = new QuanTriDAO();
                        if (dao.ChangePw(qt, reMatKhau))
                        {
                            Session["Pw"] = reMatKhau;
                            return RedirectToAction("Index", "Login");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Cập nhật không thành công");
                        }
                    }
            }
                return View("ChangePass");
            }
            ViewBag.ThongBao = "Mật khẩu không đúng!";
            return View();
        }


        public ActionResult Logout()    
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        int RandomCode()
        {
            int Numrd;
            Random rd = new Random();
            Numrd = rd.Next(1000, 9999);

            return Numrd;
        }
        [HttpPost]
        public ActionResult ForgotPass(FormCollection f)
        {
            if (ModelState.IsValid)
            {
                string sTaiKhoan = f["txtTaiKhoan"].ToString();
                string sGmail = f.Get("txtGmail").ToString();
                NhanVien nv = (from a in db.NhanViens
                               where a.UserName == sTaiKhoan && a.Email == sGmail
                               select a).SingleOrDefault();
                if (nv != null)
                {
                    QuanTri qt = (from a in db.QuanTris
                                   where a.UserName == sTaiKhoan
                                   select a).SingleOrDefault();


                    String tn = "<div><h1>Kính chào Quý khách:\t"+ sGmail +"</h1><i>Quý khách nhận được email này, vì đã tiến hành thực hiện lấy lại tài khoản trên hệ thống</i></div>" +
                        "<h2>Thông tin tài khoản:</h2>" + "<p>Tài khoản:\t" + sTaiKhoan + "</p><p>Mật khẩu:\t" + qt.Pw + "</p><p>Thời gian:\t" + DateTime.Now + "</p><h3></h3><p>Cảm ơn Quý khách đã sử dụng dịch vụ của chúng tôi!</p><p>Mọi thắc mắc xin vui lòng liên hệ CTTNHH BDS DN quaemail: hotro@gmail.com hoặc số hotline: 0396 342 804</p><p>Trân trọng!</p>";
                    
                    Send(sGmail, "CTTNHH BDS DN", tn);
                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    ViewBag.ThongBao = "Bạn đã nhập sai gmail hoặc gmail này chưa đăng ký!";
                }    
            }
            
            return View();
        }
        
        public ActionResult Code()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Code(FormCollection f)
        {
            if (ModelState.IsValid)
            {
                string sCode =  f["txtCode"].ToString();
                
                if ( int.Parse(sCode) == RandomCode())
                {
                    ViewBag.ThongBao = "Bạn nhập mã code đúng!";
                    //return RedirectToAction("ChangePass", "Login");
                }
                else
                {
                    ViewBag.ThongBao = "Bạn nhập mã code sai!";
                }
            }
            return View();
        }

        private static readonly string _from = "nguyenphuongnam989@gmail.com"; // Email của Sender (của bạn)
        private static readonly string _pass = "npkrqcfsrklkeurw"; // Mật khẩu Email của Sender (của bạn)
        public string Send(string sendto, string subject, string content)
        {
            //sendto: Email receiver (người nhận)
            //subject: Tiêu đề email
            //content: Nội dung của email, bạn có thể viết mã HTML
            //Nếu gửi email thành công, sẽ trả về kết quả: OK, không thành công sẽ trả về thông tin l�-i
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(_from);
                mail.To.Add(sendto);
                mail.Subject = subject;
                mail.IsBodyHtml = true;
                mail.Body = content;

                mail.Priority = MailPriority.High;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(_from, _pass);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }




    }
}