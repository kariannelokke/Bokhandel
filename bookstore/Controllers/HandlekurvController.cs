using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
using BookStore.ViewModels;

namespace BookStore.Controllers
{
    public class HandlekurvController : Controller
    {
        BokerContext db = new BokerContext();
    
        public ActionResult Index()
        {
            var kurv = Handlekurv.GetKurv(this.HttpContext);

            var viewModel = new HandlekurvViewModel
            {
                Varer = kurv.GetVarer(),
                KurvTotal = kurv.GetTotal()
            };
            return View(viewModel);
        }

        public ActionResult LeggIKurv(int id)
        {
            var lagtTilBok = db.Boker.Single(bok => bok.ISBN == id);

            var kurv = Handlekurv.GetKurv(this.HttpContext);

            kurv.leggIKurv(lagtTilBok);

            return RedirectToAction("Index");
        }

        public ActionResult FjernFraKurv(int id)
        {
            var kurv = Handlekurv.GetKurv(this.HttpContext);

            string bokNavn = db.Kurver.Single(vare => vare.VareID == id).Bok.Tittel;

            int vareCount = kurv.FjernFraKurv(id);

            var resultat = new HandlekurvFjernViewModel
            {
                Meddelande = Server.HtmlEncode(bokNavn) + " har blitt fjernet fra handlekurven din.",
                KurvTotal = kurv.GetTotal(),
                VareCount = vareCount,
                FjernID = id
            };

            return Json(resultat);  
        }
    }
}