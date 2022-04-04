using AFModels;
using BDS.DAO;
using BDS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BDS.Areas.User.Controllers
{
    public class BatDongSanController : Controller
    {
        // GET: User/BatDongSan
        public ActionResult Index()
        {
            return View();
        }
        BatDongSanDAO batdongsanDAO = new BatDongSanDAO();
        BatDongSanContext db = new BatDongSanContext();
        // Hiển thi các bất động sản có trong csdl, nếu duyệt = 1 thì hiển thị ra ngoài, duyệt = 0 sẽ bị ẩn khỏi trang web
        public ActionResult BatDongSan()
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

        public ActionResult BatDongSanBan()
        {
            List<BatDongSanViewModel> kq = (from bds in db.BatDongSans
                                            join ct in db.ChiTietBatDongSans
                                             on bds.MaBDS equals ct.MaBDS
                                             join n in db.NhomLoais
                                             on bds.MaNhom equals n.MaNhom
                                            where (bds.Duyet == 1) && (bds.MaNhom == 1)
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


        public ActionResult BatDongSanChoThue()
        {
            List<BatDongSanViewModel> kq = (from bds in db.BatDongSans
                                            join ct in db.ChiTietBatDongSans
                                             on bds.MaBDS equals ct.MaBDS
                                            join n in db.NhomLoais
                                            on bds.MaNhom equals n.MaNhom
                                            where (bds.Duyet == 1) && (bds.MaNhom == 2)
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


        // Hiển thị thông tin bất động sản của thành viên đăng.
        public ActionResult Poster(string user)
        {
            user = Session["UserName"].ToString();
            List<BatDongSanViewModel> kq = (from bds in db.BatDongSans
                                            where bds.UserName == user                         
                                            select new BatDongSanViewModel()
                                            {
                                                MaBDS = bds.MaBDS,
                                                TenBDS = bds.TenBDS,
                                                MaNhom = bds.MaNhom,
                                                MaLoaiBDS = bds.MaLoaiBDS,
                                                NgayDang = bds.NgayDang,
                                                Duyet = bds.Duyet,
                                            }).ToList();
            return View(kq);
        }

        // Đăng tin bất động sản lên trang web
        [HttpGet]
        public ActionResult Create()
        {
            if (Session["UserName"] != null)
            {
                ViewBag.MaNhom = new SelectList(db.NhomLoais.ToList().OrderBy(n => n.TenNhom), "MaNhom", "TenNhom");
                ViewBag.MaLoaiBDS = new SelectList(db.LoaiBatDongSans.ToList().OrderBy(n => n.TenLoaiBDS), "MaLoaiBDS", "TenLoaiBDS");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
           
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
                ThanhVien tv = (ThanhVien)Session["TV"];
                bds.TenBDS = model.TenBDS;
                bds.MaNhom = model.MaNhom;
                bds.MaLoaiBDS = model.MaLoaiBDS;
                bds.NgayDang = DateTime.Now;
                bds.UserName = tv.UserName;
                bds.Duyet = 0;
                db.BatDongSans.Add(bds);
                db.SaveChanges();
                Session["MaBDS"] = null;
                Session["MaBDS"] = bds.MaBDS;
                return RedirectToAction("CreateDetail", "BatDongSan", new { @id = Session["MaBDS"].ToString() });
            }
            return RedirectToAction("BatDongSan"); ;
        }

        // Tạo  chi tiết bất động sản
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
                    return RedirectToAction("ThongBao", "BatDongSan");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới không thành công");
                }
            }
            return View(model);
        }

        public ActionResult ThongBao()
        {
            return View();
        }


        // Xóa bất động sản
        public ActionResult Delete(int id)
        {
            new BatDongSanDAO().Delete(id);
            return RedirectToAction("BatDongSan","BatDongSan");
        }

        // chỉnh sửa bất động sản
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.MaNhom = new SelectList(db.NhomLoais.ToList().OrderBy(n => n.TenNhom), "MaNhom", "TenNhom");
            ViewBag.MaLoaiBDS = new SelectList(db.LoaiBatDongSans.ToList().OrderBy(n => n.TenLoaiBDS), "MaLoaiBDS", "TenLoaiBDS");
            var idtheloai = new BatDongSanDAO().GetByID(id);
            return View(idtheloai);
        }


        [HttpPost]
        public ActionResult Edit(BatDongSan idtheloai)
        {
            if (ModelState.IsValid)
            {
                ViewBag.MaNhom = new SelectList(db.NhomLoais.ToList().OrderBy(n => n.TenNhom), "MaNhom", "TenNhom");
                ViewBag.MaLoaiBDS = new SelectList(db.LoaiBatDongSans.ToList().OrderBy(n => n.TenLoaiBDS), "MaLoaiBDS", "TenLoaiBDS");
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

        [HttpGet]
        public ActionResult EditDetail(int id)
        {
            var idd = new ChiTietBatDongSanDAO().GetByID(id);
            return View(idd);
        }


        [HttpPost]
        public ActionResult EditDetail(ChiTietBatDongSan idtheloai)
        {
            if (ModelState.IsValid)
            {
                var dao = new ChiTietBatDongSanDAO();
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

        public ActionResult DeleteDetail(int id)
        {
            new ChiTietBatDongSanDAO().Delete(id);
            return RedirectToAction("Poster", "BatDongSan", new { @user = Session["UserName"].ToString()});
        }

        public ActionResult Detail(int? id)
        {
            if (id.HasValue)
            {
                ChiTietBDSViewModel p = batdongsanDAO.Detail(id.Value);
                Session["MaBDS"] = id;
                if (p == null)
                {
                    Session["MaBDS"] = null;
                    Session["MaBDS"] = id;
                    return RedirectToAction("CreateDetail", "BatDongSan", new { @id = id});
                }
                return View(p);
            }
            else
                return new HttpNotFoundResult("Không tìm thấy trang này!");
        }


    }
}