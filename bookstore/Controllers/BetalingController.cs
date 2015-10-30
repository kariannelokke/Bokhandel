using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;

namespace BookStore.Controllers
{

    public class BetalingController : Controller
    {
        BokerContext db = new BokerContext();
        KundeContext kundeDatabase = new KundeContext();

        const string BetalingString = "Betala";

        public ActionResult Index()
        {
            if (Session["LoggetInn"] == null)
            {
                Session["LoggetInn"] = false;
                ViewBag.Innlogget = false;
            }
            else
            {
                ViewBag.Innlogget = (bool)Session["LoggetInn"];
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(Kunde innBruker)
        {
            if (Bruker_i_DB(innBruker) != null)
            {
                MigrateShoppingCart(innBruker.Epost);
                Session["Kunde"] = innBruker.Epost;
                Session["KundeID"] = Bruker_i_DB(innBruker).Id;
                Session["LoggetInn"] = true;
                ViewBag.Innlogget = true;
                return RedirectToAction("Betaling", "Betaling");
            }
            else
            {
                Session["Kunde"] = null;
                Session["KundeID"] = null;
                Session["LoggetInn"] = false;
                ViewBag.Innlogget = false;
                return View();
            }
        }

        public ActionResult Betaling()
        {
            if (Session["LoggetInn"] != null)
            {
               if(Session["KundeID"] != null)
                {
                    bool loggetInn = (bool)Session["LoggetInn"];
                    if (loggetInn)
                    {
                        return View();
                    }
                }
            }
            return RedirectToAction("Index");
        }
    
        [HttpPost]
        public ActionResult Betaling(FormCollection values)
        {
            var bestilling = new Bestilling();
            string epost = (string)Session["Kunde"];
            TryUpdateModel(bestilling);

            try
            {
                if (string.Equals(values["Betala"], BetalingString, StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(bestilling);
                }
                else
                {
                    var handlekurv = Handlekurv.GetKurv(this.HttpContext);
                    bestilling.KundeId = epost;

                    bestilling.BestillingsDato = DateTime.Now;
                    bestilling.Total = handlekurv.GetTotal();
                   
                    db.Bestillinger.Add(bestilling);
                    db.SaveChanges();
                

                    handlekurv.SkapaBestilling(bestilling);
                    return RedirectToAction("Kvittering", new { id = bestilling.BestillingsID });
                }
            }
            catch
            {
               
                return View(bestilling);
            }
        }
     
        public ActionResult Kvittering(int id)
        {
            // Sjekk at det er kundens sin ordre
            string epost = (string)Session["Kunde"];

            Bestilling bestilling = db.Bestillinger.Find(id);

            bool isValid = db.Bestillinger.Any(o => o.BestillingsID == id && o.KundeId == epost);
            var genreModel = db.Bestillinger.Include("BestillingsDetaljer").Single(g => g.BestillingsID == id && g.KundeId == epost);


            if (isValid)
            {
                return View(genreModel);
            }
            else
            {
                return View("Error");
            }
        }

        private void MigrateShoppingCart(string epost)
        {
            // Koppla varer i kurv med bruker som logger inn
            var cart = Handlekurv.GetKurv(this.HttpContext);
            cart.MigreraKurv(epost);
            Session[Handlekurv.HandleSessionID] = epost;
        }
        private static byte[] lagHash(string innPassord)
        {
            byte[] innData, utData;
            var algoritme = System.Security.Cryptography.SHA256.Create();
            innData = System.Text.Encoding.ASCII.GetBytes(innPassord);
            utData = algoritme.ComputeHash(innData);
            return utData;
        }

        private static dbKunde Bruker_i_DB(Kunde innBruker)
        {
            using (var db = new KundeContext())
            {
                byte[] passordDb = lagHash(innBruker.Passord);
                dbKunde funnetBruker = db.Kunder.FirstOrDefault
                (b => b.Passord == passordDb && b.Epost == innBruker.Epost);
                if (funnetBruker == null)
                {
                    return null;
                }
                else
                {
                    return funnetBruker;
                }
            }
        }

    }
}