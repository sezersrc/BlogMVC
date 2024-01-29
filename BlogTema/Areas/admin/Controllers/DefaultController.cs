using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogTema.Models;
namespace BlogTema.Areas.admin.Controllers
{
    public class DefaultController : Controller
    {
        // GET: admin/Default
        BlogTemplateEntities db = new BlogTemplateEntities();
        public ActionResult Index()
        {
            if (Session["Data"] != null)
            {
                return View(db.TBLBlog.ToList());
            }
            else
            {
                return Redirect("/admin/Login");
            }
        }
        public ActionResult Insert()
        {
            if (Session["Data"] != null)
            {
                return View();
            }
            else
            {
                return Redirect("/admin/Login");
            }
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Insert(FormCollection Frm, HttpPostedFileBase Resim)
        {
            if (Resim != null && Resim.ContentLength > 0)
            {
                Random rnd = new Random();

                string YeniAdi = "Blog_" + rnd.Next(0, 879789) + ".jpg";
                if (Resim.ContentType == "image/jpg" || Resim.ContentType == "image/jpeg")
                {
                    string ResimYolu = Path.Combine(Server.MapPath("/images"), YeniAdi);
                    Resim.SaveAs(ResimYolu);

                    TBLBlog b = new TBLBlog();
                    b.Aciklama = Request.Form["Aciklama"];
                    b.Begenme = 0;
                    b.Resim = YeniAdi;
                    b.Begenmeme = 0;
                    b.Tarih = DateTime.Now;
                    b.Okunma = 0;
                    b.BlogAdi = Request.Form["BlogAdi"];
                    db.TBLBlog.AddOrUpdate(b);
                    //db.();
                    TempData["data"] = "<div class=\"alert alert-success\" style=\"margin-top:35px; text - align:center\">Data Yüklendi.</div>";
                }

            }
            else
            {
                TempData["data"] = "<div class=\"alert alert-danger\" style=\"margin-top:35px; text - align:center\">Resim Seçilmedi.</div>";
            }


            return View();
        }

        public ActionResult Update(int ID)
        {
            if (Session["Data"] != null)
            {
                return View(db.TBLBlog.Where(x => x.ID == ID));
            }
            else
            {
                return Redirect("/admin/Login");
            }
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Update(FormCollection Frm, HttpPostedFileBase Resim, int ID)
        {
            if (Resim != null && Resim.ContentLength > 0)
            {
                Random rnd = new Random();
                string YeniAdi = "Blog_" + rnd.Next(0, 879789) + ".jpg";
                if (Resim.ContentType == "image/jpg" || Resim.ContentType == "image/jpeg")
                {
                    string ResimYolu = Path.Combine(Server.MapPath("/images"), YeniAdi);
                    Resim.SaveAs(ResimYolu);

                    TBLBlog b = db.TBLBlog.Find(ID);
                    b.Aciklama = Request.Form["Aciklama"];
                    b.Begenme = 0;
                    b.Resim = YeniAdi;
                    b.Begenmeme = 0;
                    b.Tarih = DateTime.Now;
                    b.Okunma = 0;
                    b.BlogAdi = Request.Form["BlogAdi"];
                    db.SaveChanges();
                    TempData["data"] = "<div class=\"alert alert-success\" style=\"margin-top:35px; text - align:center\">Data Güncellendi.</div>";
                }
            }
            else
            {
                TBLBlog b = db.TBLBlog.Find(ID);
                b.Aciklama = Request.Form["Aciklama"];
                b.Begenme = 0;
                b.Begenmeme = 0;
                b.Tarih = DateTime.Now;
                b.Okunma = 0;
                b.BlogAdi = Request.Form["BlogAdi"];
                db.SaveChanges();
                TempData["data"] = "<div class=\"alert alert-success\" style=\"margin-top:35px; text - align:center\">Data Güncellendi.</div>";
            }

            return View(db.TBLBlog.Where(x => x.ID == ID));
        }

        public ActionResult Delete(int ID)
        {
            db.TBLBlog.Remove(db.TBLBlog.Find(ID));
            db.SaveChanges();
            return Redirect("/admin/Default");
        }

    }
}