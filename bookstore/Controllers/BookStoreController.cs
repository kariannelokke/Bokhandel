using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;


namespace BookStore.Controllers
{
    public class BookStoreController : Controller
    {
        BokerContext db = new BokerContext();
        public ActionResult Index()
        {
            var sjangere = db.Sjangere.ToList();
            return View(sjangere);
        }

        public ActionResult Detaljer(int id)
        {
            var bok = db.Boker.Find(id);
            return View(bok);
        }

        public ActionResult Browse (String sjanger)
        {
            var sjangerModel = db.Sjangere.Include("Boker").Single(g => g.Navn == sjanger);
            return View(sjangerModel);
        }
    }
}