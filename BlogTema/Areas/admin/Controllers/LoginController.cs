using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogTema.Models;
namespace BlogTema.Areas.admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: admin/Logint
        public ActionResult Index()
        {
            if (Session["Data"] == null)
            {
                return View();
            }
            else
            {
                return Redirect("/admin/Default");
            }
        }
        [HttpPost]
        public ActionResult Index(FormCollection Frm)
        {
            BlogTemplateEntities db = new BlogTemplateEntities();

            // Hem Sql'de Sorgulatıp hemde aynı anda input'tan veri çekemezsin.
            string KulAdi = Request.Form["KulAdi"];
            string Sifresi = Request.Form["Sifre"];

            if (db.TBLAyarlar.Any(x => x.KullaniciAdi == KulAdi && x.Sifre == Sifresi))
            {
                Session["Data"] = "Admin";

                return Redirect("/admin/default");
            }
            else
            {
                TempData["Mesaj"] = "Giriş Bilgileri Hatali";
                return View();
            }

        }
    }
}