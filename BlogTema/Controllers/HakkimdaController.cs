using BlogTema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogTema.Controllers
{
    public class HakkimdaController : Controller
    {
        // GET: Hakkimda
        BlogTemplateEntities db = new BlogTemplateEntities();
        public ActionResult Index()
        {
            return View(db.TBLAyarlar.Where(x=> x.ID == 1).ToList());
        }
    }
}