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

namespace BDS.Areas.User.Controllers
{
    public class UserController : Controller
    {
        // GET: User/User
        BatDongSanDAO batdongsanDAO = new BatDongSanDAO();
        LienHeDAO lienheDAO = new LienHeDAO();
        BatDongSanContext db = new BatDongSanContext();
        //----------------------------List ra danh sách Bất Động Sản---------------------
        public ActionResult Index()
        {
            List<BatDongSanViewModel> kq = (from bds in db.BatDongSans
                                            join ct in db.ChiTietBatDongSans
                                             on bds.MaBDS equals ct.MaBDS
                                            join n in db.NhomLoais
                                            on bds.MaNhom equals n.MaNhom
                                            where bds.Duyet == 1
                                            select new BatDongSanViewModel()
                                            {
                                                MaBDS = bds.MaBDS,
                                                TenBDS = bds.TenBDS,
                                                MaNhom = bds.MaNhom,
                                                MaLoaiBDS = bds.MaLoaiBDS,
                                                NgayDang = bds.NgayDang,
                                                UserName = bds.UserName,
                                                Duyet = bds.Duyet,
                                                Gia = ct.Gia,
                                                Mota = ct.Mota,
                                                Anh = ct.Anh,
                                                PhongNgu = ct.PhongNgu,
                                                PhongTam = ct.PhongTam,
                                                TenNhom = n.TenNhom
                                            }).ToList();
            return View(kq);
        }

        //------------------------------Xem chi tiết------------------------------------

        public ActionResult Detail(int? id)
        {
            if (id.HasValue)
            {
                ChiTietBDSViewModel p = batdongsanDAO.Detail(id.Value);
                return View(p);
            }
            else
                return new HttpNotFoundResult("Không tìm thấy trang này!");
        }




        public ViewResult XemChiTiet(int MaBDS = 0)
        {
            BatDongSan hoa = db.BatDongSans.SingleOrDefault(n => n.MaBDS == MaBDS);
            if (hoa == null)
            {
                //Trả về trang báo lỗi 
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.TenDM = db.ChiTietBatDongSans.Single(n => n.MaBDS == hoa.MaBDS).Gia;
            return View(hoa);
        }



        NhanVienDAO nhanvienDAO = new NhanVienDAO();
        public ActionResult NhanVien(int pageNumber = 1, int pageSize = 5)
        {
            var pt = nhanvienDAO.ListAll(pageNumber, pageSize);
            return View(pt);
        }

        BinhLuanDAO binhLuanDAO = new BinhLuanDAO();
        public ActionResult BinhLuan(int pageNumber = 1, int pageSize = 5)
        {
            var pt = binhLuanDAO.ListAll(pageNumber, pageSize);
            return View(pt);
        }
        //-------------------------------------------LOGIN-------------------------------------------
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            string sTaiKhoan = f["txtTaiKhoan"].ToString();
            string sMatKhau = f.Get("txtMatKhau").ToString();
            ThanhVien tv = (from a in db.ThanhViens
                          where a.UserName == sTaiKhoan && a.Pw == sMatKhau
                          select a).SingleOrDefault();
            if (tv != null)
            {
                    FormsAuthentication.SetAuthCookie(tv.UserName, false);
                    Session["TV"] = tv;
                    Session["UserName"] = tv.UserName;
                    Session["HoTen"] = tv.HoTenTV;
                    Session["Email"] = tv.Email;
                    Session["Pw"] = tv.Pw;
                    Session["MaKH"] = tv.MaTV;
                     return RedirectToAction("Index", "User");
            }
            ViewBag.ThongBao = "Tên tài khoản hoặc mật khẩu không đúng!";
            return View();
        }
        //-------------------------------------------Đăng ký-------------------------------------------
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
                string toAddress = model.Email;
                string subject = "Bất Động Sản CTTNHH BDS DN | Đăng ký";
                string path = Path.Combine(HttpRuntime.AppDomainAppPath, "C:/Users/Nam/Desktop/BDS/BDS/BDS/MailTemplate/MailRegister.html");
                string body = System.IO.File.ReadAllText(path);
                body = body.Replace("{Name}", model.HoTenTV); //replacing the required things  
                body = body.Replace("{Username}", model.UserName);
                body = body.Replace("{Password}", model.Pw);
                //body = "Chúc mừng "+model.HoTenTV+" đã Đăng ký tài khoản thành công ! TenTK : "+model.UserName+"| MK: "+model.Pw;

                var id = new ThanhVienDAO().Insert(model);
                Send(toAddress, subject, body);
                if (id > 0)
                {
                   
                    return RedirectToAction("Thongbao","User");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới không thành công");
                    

                }
            }
            return View(model);
        }

        //-------------------------------------------Liên hệ với công ty-------------------------------------------
        [HttpGet]
        public ActionResult LienHe()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LienHe(LienHe model)
        {


            string toAddress = model.Email;
            string subject = "CTTNHH BDS DN | Liên hệ";
            string body = "chúng tôi đã nhận được thư của bạn. chúng tôi sẽ phản hồi trong thời gian sớm nhất đến bạn ! lời nhắn từ bạn: " + model.LoiNhan;          
            string path = Path.Combine(HttpRuntime.AppDomainAppPath, "C:/Users/Nam/Desktop/BDS/BDS/BDS/MailTemplate/MailLienHe.html");
            body = System.IO.File.ReadAllText(path);
            body = body.Replace("{Name}", model.HoTen); //replacing the required things  
            body = body.Replace("{Content}", model.LoiNhan);
            if (ModelState.IsValid)
            {
                var id = new LienHeDAO().Insert(model);
                Send(toAddress, subject, body);
                if (id > 0)
                {
                   
                    return RedirectToAction("ThongbaoLienHe","User");
                }
                else
                {
                    ModelState.AddModelError("", "Liên hệ không thành công");

                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }


        //-------------------------------------- Thêm thông tin khách hàng trước khi liên hệ ---------------------------------
        [HttpGet]
        public ActionResult InformationCustomer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InformationCustomer(KhachHang model)
        {
            if (ModelState.IsValid)
            {

                var id = new KhachHangDAO().Insert(model);
                if (id > 0)
                {
                    Session["KH"] = model;
                    Session["MaKH"] = model.MaKH;
                    return RedirectToAction("DatThue","DatThue");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới không thành công");
                }
            }
            return View(model);
        }

        //-------------------------------------Menu chỉnh sửa các thông tin cá nhân của khách hàng---------------------------
        [HttpGet]
        public ActionResult Account()
        {
            return View();
        }

        //--------------------------------------Chỉnh sửa thông tin cá nhân---------------------------------------------------

        [HttpGet]
        public ActionResult EditInfo()
        {
            string user = Session["UserName"].ToString();
            ThanhVien kq = (from tv in db.ThanhViens
                            where tv.UserName == user
                            select tv).SingleOrDefault();
            return View(kq);
        }

        [HttpPost]
        public ActionResult EditInfo(ThanhVien idnv)
        {
            if (ModelState.IsValid)
            {
                    var tv = db.ThanhViens.Find(idnv.MaTV);
                    tv.HoTenTV = idnv.HoTenTV;
                    tv.Pw = idnv.Pw;
                    tv.DienThoai = idnv.DienThoai;
                    tv.GioiTinh = idnv.GioiTinh;
                    tv.DiaChi = idnv.DiaChi;
                    tv.Email = idnv.Email;
                    db.SaveChanges();
                string user = Session["UserName"].ToString();
                QuanTri kq = (from qt in db.QuanTris
                                where qt.UserName == user
                                select qt).SingleOrDefault();
                kq.Pw = idnv.Pw;
                db.SaveChanges();
                return RedirectToAction("Account", "User");

              }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            return View();
        }

        public ActionResult Thongbao()
        {
            return View();
        }

        public ActionResult ThongbaoLienHe()
        {
            return View();
        }
        public ActionResult ForgotPass()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPass(FormCollection f)
        {
            if (ModelState.IsValid)
            {
               
                string sTaiKhoan = f["txtTaiKhoan"].ToString();
                string sGmail = f.Get("txtGmail").ToString();
                ThanhVien tv = (from a in db.ThanhViens
                               where a.UserName == sTaiKhoan && a.Email == sGmail
                               select a).SingleOrDefault();
                if (tv != null)
                {
                    string toAddress = sGmail;
                    string subject = "Bất Động Sản CTTNHH BDS DN | Đăng ký";
                    string path = Path.Combine(HttpRuntime.AppDomainAppPath, "C:/Users/Nam/Desktop/BDS/BDS/BDS/MailTemplate/MailForgot.html");
                    string body = System.IO.File.ReadAllText(path);
                    body = body.Replace("{Name}", tv.HoTenTV); //replacing the required things  
                    body = body.Replace("{Username}", tv.UserName);
                    body = body.Replace("{Password}", tv.Pw);
                    //body = "Chúc mừng "+model.HoTenTV+" đã Đăng ký tài khoản thành công ! TenTK : "+model.UserName+"| MK: "+model.Pw;

                    Send(toAddress, subject, body);
                    return RedirectToAction("Login", "User");
                }
                else
                {
                    ViewBag.ThongBao = "Bạn đã nhập sai gmail hoặc gmail này chưa đăng ký!";
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