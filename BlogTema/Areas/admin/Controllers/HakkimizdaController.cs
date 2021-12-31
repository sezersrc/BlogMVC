using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogTema.Models;
namespace BlogTema.Areas.admin.Controllers
{
    public class HakkimizdaController : Controller
    {
        // GET: admin/Hakkimizda
        BlogTemplateEntities db = new BlogTemplateEntities();
        public ActionResult Index()
        {
            if (Session["Data"] != null)
            {
                return View(db.TBLAyarlar.Where(x => x.ID == 1).ToList());
            }
            else
            {
                return Redirect("/admin/Login");
            }
        }
        [HttpPost]
        public ActionResult Index(FormCollection Frm)
        {
            TBLAyarlar a = db.TBLAyarlar.Find(1);
            a.facebook = Request.Form["facebook"];
            a.twitter = Request.Form["twitter"];
            a.linkedin = Request.Form["linkedin"];
            a.stackoverflow = Request.Form["stackoverflow"];
            a.Yotube = Request.Form["Yotube"];
            a.KisaAciklama = Request.Form["KisaAciklama"];
            a.Aciklama = Request.Form["Aciklama"];
            a.Sifre = Request.Form["Sifre"];
            a.BloggerAdi = Request.Form["BloggerAdi"];
            db.SaveChanges();
            TempData["data"] = "<div class=\"alert alert-success\" style=\"margin-top:35px; text - align:center\">Data Güncellendi.</div>";
            return View(db.TBLAyarlar.Where(x => x.ID == 1).ToList());
        }
    }
}