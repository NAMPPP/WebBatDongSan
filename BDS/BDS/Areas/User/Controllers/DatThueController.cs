using AFModels;
using BDS.DAO;
using BDS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BDS.Areas.User.Controllers
{
    public class DatThueController : Controller
    {

        BatDongSanContext db = new BatDongSanContext();
        HoaDonDAO hoadondao = new HoaDonDAO();
        // GET: User/DatThue
        public ActionResult Index()
        {
            return View();
        }
        // Lấy ra danh sách đặt chổ
        public List<DatThue> LayDatThue()
        {
            List<DatThue> lstDatThue = Session["DatThue"] as List<DatThue>;
            if (lstDatThue == null)
            {
                //Nếu giỏ hàng chưa tồn tại thì mình tiến hành khởi tao list giỏ hàng (sessionDatThue)
                lstDatThue = new List<DatThue>();
                Session["DatThue"] = lstDatThue;
            }
            return lstDatThue;
        }

        // Thêm vào danh sách đặt chổ

        public ActionResult ThemDatThue(int iMaBDS, string strURL)
        {

            BatDongSan bds = db.BatDongSans.SingleOrDefault(n => n.MaBDS == iMaBDS);
            if (bds == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy ra session đặt chổ
            List<DatThue> lstDatThue = LayDatThue();
            //Kiểm tra sách này đã tồn tại trong session[datthue] chưa
            DatThue sp = lstDatThue.Find(n => n.iMaBDS == iMaBDS);
            if (sp == null)
            {
                sp = new DatThue(iMaBDS);
                //Add sản phẩm mới thêm vào list
                lstDatThue.Add(sp);
                return Redirect(strURL);
            }
            else
            {
               // báo bạn đã đặt chổ rồi
                return Redirect(strURL);
            }
        }
        // Xây dựng trang hiển thị thông tin đặt chổ
        public ActionResult DatThue()
        {
            if (Session["DatThue"] == null)
            {
                return RedirectToAction("Index", "User");
            }
            List<DatThue> lstDatThue = LayDatThue();
            return View(lstDatThue);
        }

        private double DemSL()
        {
            int sl = 0;
            List<DatThue> lstDatThue = Session["DatThue"] as List<DatThue>;
            if (lstDatThue != null)
            {
                sl = lstDatThue.Count;            }
            return sl;
        }
        // Hiển thị số lượng đã đặt
        public ActionResult SLDaDat()
        {
            if (DemSL() == 0)
            {
                return PartialView();
            }
            ViewBag.SL =DemSL();
            return PartialView();
        }

        // Xóa đặt chổ

        public ActionResult XoaDatThue(int iMaBDS)
        {
            //Kiểm tra masp
            BatDongSan bds = db.BatDongSans.SingleOrDefault(n => n.MaBDS == iMaBDS);
            //Nếu get sai masp thì sẽ trả về trang lỗi 404
            if (bds == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy giỏ hàng ra từ session
            List<DatThue> lstDatThue = LayDatThue();
            DatThue dt = lstDatThue.SingleOrDefault(n => n.iMaBDS == iMaBDS);
            //Nếu mà tồn tại thì chúng ta cho sửa số lượng
            if (dt != null)
            {
                lstDatThue.RemoveAll(n => n.iMaBDS == iMaBDS);

            }
            if (lstDatThue.Count == 0)
            {
                return RedirectToAction("Index", "User");
            }
            return RedirectToAction("DatThue");
        }
        //Xây dựng chức năng đặt hàng
        [HttpPost]
        public ActionResult DatThueNgay()
        {
            KhachHang makh = (KhachHang)Session["KH"];
            //Kiểm tra thông tin đăng nhập, khách hàng đã điền thông tin hay chưa?
            if (makh==null && Session["Username"] == null)
            {
                return RedirectToAction("InformationCustomer", "User");
            }
            
            //Kiểm tra thông tin đặt chổ
            if (Session["DatThue"] == null)
            {
                RedirectToAction("Index", "User");
            }

            //Thêm đơn hàng
            QuanLyDatThue qldt = new QuanLyDatThue();
            QuanLyDatThueKhachHang qldtkh = new QuanLyDatThueKhachHang();
            ThanhVien tv = (ThanhVien)Session["TV"];
            List<DatThue> dt = LayDatThue();
            if (Session["Username"] !=null)
            {
 
                qldt.MaKH = tv.MaTV;
                foreach (var item in dt)
                {
                    qldt.MaBDS = item.iMaBDS;
                    qldt.NgayDat = DateTime.Now;
                    qldt.GhiChu = "Thanh toán tiền mặt";
                }
                db.QuanLyDatThues.Add(qldt);

                db.SaveChanges();

                BatDongSan kq = (from bds in db.BatDongSans
                                 where bds.MaBDS == qldt.MaBDS
                                 select bds).SingleOrDefault();

                string toAddress = Session["Email"].ToString(); ;
                string subject = "Bất Động Sản 18DTHC2 | Đặt chổ - Thành viên";
                string path = Path.Combine(HttpRuntime.AppDomainAppPath, "C:/Users/Nam/Desktop/BDS/BDS/BDS/MailTemplate/MailDatThue.html");
                string body = System.IO.File.ReadAllText(path);
                body = body.Replace("{Name}", Session["UserName"].ToString()); //replacing the required things  
                body = body.Replace("{TenBDS}", kq.TenBDS); //replacing the required things 
                LienHeDAO lienheDAO = new LienHeDAO();
                
                Session["DatThue"] = null;
            }

            if (Session["KH"] != null)

            {

                qldtkh.MaKH = makh.MaKH;
                foreach (var item in dt)
                {
                    qldtkh.MaBDS = item.iMaBDS;
                    qldtkh.NgayDat = DateTime.Now;
                    qldtkh.GhiChu = "Khách hàng - Thanh toán tiền mặt";
                }
                db.QuanLyDatThueKhachHangs.Add(qldtkh);

                db.SaveChanges();

                BatDongSan bdss = (from bds in db.BatDongSans
                                 where bds.MaBDS == qldtkh.MaBDS
                                 select bds).SingleOrDefault();

                string toAddress = makh.Email ;
                string subject= "Bất Động Sản 18DTHC2 | Đặt chổ - Khách hàng";
                string path = Path.Combine(HttpRuntime.AppDomainAppPath, "C:/Users/Admin/Downloads/DACN-151250533116/BDS/BDS/MailTemplate/MailDatThue.html");
                string body = System.IO.File.ReadAllText(path);
                body = body.Replace("{Name}", makh.HoTenKH); //replacing the required things  
                body = body.Replace("{TenBDS}", bdss.TenBDS); //replacing the required things  
                LienHeDAO lienheDAOs = new LienHeDAO();
              
                Session["DatThue"] = null;
                Session["KH"] = null;
            }
            
            return RedirectToAction("ThongBao", "DatThue");
        }

        public ActionResult ThongBao()
        {
            return View();
        }


    }
}