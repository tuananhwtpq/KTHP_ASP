using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalTest1.Models;

namespace FinalTest1.Controllers
{
    public class NhanViensController : Controller
    {
        private Model1 db = new Model1();

        // GET: NhanViens
        public ActionResult Index()
        {
            var nhanViens = db.NhanViens.Include(n => n.Phong);
            return View(nhanViens.ToList());
        }

        // GET: NhanViens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // GET: NhanViens/Create
        public ActionResult Create()
        {
            ViewBag.Maphong = new SelectList(db.Phongs, "Maphong", "Tenphong");
            return View();
        }

        // POST: NhanViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Manv,Hoten,Tuoi,Diachi,Luong,Maphong")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                db.NhanViens.Add(nhanVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Maphong = new SelectList(db.Phongs, "Maphong", "Tenphong", nhanVien.Maphong);
            return View(nhanVien);
        }*/
        [HttpPost]
        public ActionResult Create(NhanVien nv)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.NhanViens.Add(nv);
                    db.SaveChanges();
                    return Json(new { result = true, JsonRequestBehavior.AllowGet });
                }
                else
                {
                    ViewBag.Maphong = new SelectList(db.Phongs, "Maphong", "Tenphong");
                    return View();
                }
            }
            catch (Exception ex)
            {
                return Json(new { msg = ex, JsonRequestBehavior.AllowGet });
            }
        }
        // GET: NhanViens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            ViewBag.Maphong = new SelectList(db.Phongs, "Maphong", "Tenphong", nhanVien.Maphong);
            return View(nhanVien);
        }

        // POST: NhanViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(NhanVien nv)
        {
            try
            {
                var newNV = db.NhanViens.FirstOrDefault(n => n.Manv == nv.Manv);
                if (newNV == null)
                {
                    return Json(new { result = false, JsonRequestBehavior.AllowGet });
                }
                else
                {
                    newNV.Hoten = nv.Hoten;
                    newNV.Tuoi = nv.Tuoi;
                    newNV.Diachi = nv.Diachi;
                    newNV.Maphong = nv.Maphong;
                    newNV.Luong = nv.Luong;

                    db.SaveChanges();
                    return Json(new { result = true, JsonRequestBehavior.AllowGet });
                }
            }
            catch (Exception ex)
            {
                return Json(new { result = false, error = ex.Message });
            }
        }

        // GET: NhanViens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // POST: NhanViens/Delete/5
        [HttpPost]
        /*[ValidateAntiForgeryToken]*/
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                NhanVien nhanVien = db.NhanViens.Find(id);
                db.NhanViens.Remove(nhanVien);
                db.SaveChanges();
                return Json(new { result = true, JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                return Json(new { result = false, JsonRequestBehavior.AllowGet });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult DiaChiList()
        {
            var list = db.Phongs.ToList();
            return PartialView(list);
        }

        
        public ActionResult ListPhong(int id)
        {
            var list = db.NhanViens.Where(n => n.Maphong == id).ToList();
            return View(list);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string TaiKhoan, string MatKhau)
        {
            var login = db.Taikhoans.FirstOrDefault(t=>t.tendn == TaiKhoan && t.matkhau==MatKhau);
            if (login!=null)
            {
                Session["Login"] = login.tendn;
                return RedirectToAction("Index", "NhanViens");
            }
            else
            {
                ViewBag.Message = "Ten Dang Nhap or Mk Sai";
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session["Login"] = null;
            return RedirectToAction("Login", "NhanViens");
        }
    }
}
