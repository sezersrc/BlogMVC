using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogTema.Models;
namespace BlogTema.Areas.admin.Controllers
{
    public class YorumlarController : Controller
    {
        // GET: admin/Yorumlar
        BlogTemplateEntities db = new BlogTemplateEntities();
        public ActionResult Index()
        {
            if (Session["Data"] != null)
            {
                return View(db.TBLYorumlar.ToList());
            }
            else
            {
                return Redirect("/admin/Login");
            }
   
        }
        public ActionResult YorumDetay(int ID)
        {
            if (Session["Data"] != null)
            {
                return View(db.TBLYorumlar.Where(x => x.ID == ID).ToList());
            }
            else
            {
                return Redirect("/admin/Login");
            }
           
        }

        [HttpPost]
        public ActionResult YorumDetay(int ID,FormCollection Frm)
        {
            TBLYorumlar y = db.TBLYorumlar.Find(ID);
            y.Yorum = Request.Form["Yorum"];
            y.Onay = Convert.ToBoolean(Convert.ToInt32(Request.Form["Onay"]));
            db.SaveChanges();

            TempData["data"] = "<div class=\"alert alert-success\" style=\"margin-top:35px; text - align:center\">Yorum Güncellendi.</div>";

            return View(db.TBLYorumlar.Where(x => x.ID == ID).ToList());
        }
        public ActionResult Delete(int ID)
        {
            db.TBLYorumlar.Remove(db.TBLYorumlar.Find(ID));
            db.SaveChanges();
            return Redirect("/admin/yorumlar");
        }
    }
}