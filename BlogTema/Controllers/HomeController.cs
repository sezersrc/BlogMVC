using System;
using System.Linq;
using System.Web.Mvc;
using BlogTema.Models;

namespace BlogTema.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        BlogTemplateEntities db = new BlogTemplateEntities();
        public ActionResult Index()
        {
            return View(db.TBLBlog.ToList());
        }
        public JsonResult LikeLike(int BlogID, int Secim)
        {
            string ipAdres = Request.UserHostAddress;
            if (db.TBLTiklama.Any(x => x.ipadres == ipAdres && x.BlogID == BlogID))
            {
                bool BegenmeDurumu = Convert.ToBoolean(db.TBLTiklama.Where(x => x.ipadres == ipAdres && x.BlogID == BlogID).FirstOrDefault().durumu);

                if (BegenmeDurumu) // Daha Önce Beğenme Butonuna Tıklamışsa
                {
                    if (Secim == 1) // Tekrar Beğen Butonuna Tıkladıysa Çalışmayacak.
                    {

                    }
                    else // Beğenmeme Butonuna Tıkladıysa Çalışsın
                    {
                        TBLBlog b = db.TBLBlog.Find(BlogID);
                        b.Begenme -= 1;
                        b.Begenmeme += 1;
                        db.SaveChanges();

                        int TiklamaID = Convert.ToInt32(db.TBLTiklama.Where(x => x.ipadres == ipAdres && x.BlogID == BlogID).FirstOrDefault().ID);
                        TBLTiklama t = db.TBLTiklama.Find(TiklamaID);
                        t.durumu = false;
                        db.SaveChanges();
                    }
                }
                else // Daha Önce Beğenmeme Butonuna Tıklamışsa
                {
                    if (Secim == 0) // Tekrar Beğen Butonuna Tıkladıysa Çalışmayacak.
                    {

                    }
                    else // Beğenmeme Butonuna Tıkladıysa Çalışsın
                    {
                        TBLBlog b = db.TBLBlog.Find(BlogID);
                        b.Begenme += 1;
                        b.Begenmeme -= 1;
                        db.SaveChanges();

                        int TiklamaID = Convert.ToInt32(db.TBLTiklama.Where(x => x.ipadres == ipAdres && x.BlogID == BlogID).FirstOrDefault().ID);
                        TBLTiklama t = db.TBLTiklama.Find(TiklamaID);
                        t.durumu = true;
                        db.SaveChanges();
                    }
                }
            }
            else
            {
                if (Secim == 1)
                {
                    TBLBlog b = db.TBLBlog.Find(BlogID);
                    b.Begenme += 1;
                    db.SaveChanges();

                    TBLTiklama t = new TBLTiklama();
                    t.BlogID = BlogID;
                    t.ipadres = ipAdres;
                    t.durumu = Convert.ToBoolean(Secim);
                    db.TBLTiklama.Add(t);
                    db.SaveChanges();
                }
                else
                {
                    TBLBlog b = db.TBLBlog.Find(BlogID);
                    b.Begenmeme += 1;
                    db.SaveChanges();
                    TBLTiklama t = new TBLTiklama();
                    t.BlogID = BlogID;
                    t.ipadres = ipAdres;
                    t.durumu = Convert.ToBoolean(Secim);
                    db.TBLTiklama.Add(t);
                    db.SaveChanges();
                }
            }
            return Json("");
        }


        public ActionResult Detay(int ID)
        {
            // HTML Tarafına 2 veya daha fazla tablo göndermek istiyorsam ne yapmam lazım ?
            DataViewModel vm = new DataViewModel();
            vm.TBLYorumlar = db.TBLYorumlar.Where(x=> x.BlogID == ID && x.Onay == true).ToList();
            vm.TBLBlog = db.TBLBlog.Where(x => x.ID == ID).ToList();
            return View(vm);
        }
        [HttpPost]
        public ActionResult Detay(int ID,FormCollection Frm)
        {
            TBLYorumlar y = new TBLYorumlar();
            y.BlogID = ID;
            y.Tarih = DateTime.Now;
            y.Onay = false;
            y.YorumYapan = Request.Form["YorumYapan"];
            y.Yorum = Request.Form["Yorum"];
            db.TBLYorumlar.Add(y);
            db.SaveChanges();

            TempData["data"] = "<div class=\"alert alert-success\" style=\"margin-top:35px; text - align:center\">Yorumunuz Onaylandıktan sonra yayınlanacaktır.</div>";
            // HTML Tarafına 2 veya daha fazla tablo göndermek istiyorsam ne yapmam lazım ?
            DataViewModel vm = new DataViewModel();
            vm.TBLYorumlar = db.TBLYorumlar.Where(x => x.BlogID == ID && x.Onay == true).ToList();
            vm.TBLBlog = db.TBLBlog.Where(x => x.ID == ID ).ToList();
            return View(vm);
        }

        public ActionResult partialGetir()
        {
            return PartialView("/Views/_Shared/_PartialPage.cshtml",db.TBLAyarlar.Where(x=> x.ID == 1).ToList());
        }
    }
}