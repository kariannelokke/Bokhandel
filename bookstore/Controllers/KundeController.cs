using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
using System.Data.Entity;
using BookStore.ViewModels;

namespace BookStore.Controllers
{
    public class KundeController : Controller
    {
        private KundeContext kundeDatabase = new KundeContext();
        private BokerContext db = new BokerContext();

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
            dbKunde Kunde = new dbKunde();
            Kunde = Bruker_i_DB(innBruker);
            if (Kunde != null)
            {
                Session["LoggetInn"] = true;
                Session["KundeID"] = Kunde.Id;
                Session["Kunde"] = Kunde.Epost;
                ViewBag.Innlogget = true;
                return RedirectToAction("VisEnKunde", new { id = Kunde.Id });
            }
            else
            {
                Session["LoggetInn"] = false;
                ViewBag.Innlogget = false;
                return View();
            }
        }

        public ActionResult loggUt()
        {
            Session["LoggetInn"] = false;
            Session["KundeID"] = null;
            Session["Kunde"] = null;
            return View();
        }

        public ActionResult Registrer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registrer(Kunde innBruker)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            using (var db = new KundeContext())
            {
                try
                {
                    var nyBruker = new dbKunde();

                    byte[] passordDb = lagHash(innBruker.Passord);
                    nyBruker.Passord = passordDb;
                    nyBruker.Epost = innBruker.Epost;
                    nyBruker.Fornavn = innBruker.Fornavn;
                    nyBruker.Etternavn = innBruker.Etternavn;
                    nyBruker.Adresse = innBruker.Adresse;

                    string innPostnr = innBruker.Poststed.Postnr;

                    var funnetPostSted = db.Poststeder.FirstOrDefault(p => p.Postnr == innPostnr);
                    if (funnetPostSted == null) // fant ikke poststed, må legge inn et nytt
                    {
                        var nyttPoststed = new Models.PostSted();
                        nyttPoststed.Postnr = innBruker.Poststed.Postnr;
                        nyttPoststed.Poststed = innBruker.Poststed.Poststed;
                        db.Poststeder.Add(nyttPoststed);
                        // det nye poststedet legges i den nye brukeren
                        nyBruker.Poststed = nyttPoststed;

                    }
                    else
                    { // fant poststedet, legger det inn i den nye brukeren
                        nyBruker.Poststed = funnetPostSted;
                    }
                    db.Kunder.Add(nyBruker);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception feil)
                {
                    return View();
                }
            }
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
                dbKunde funnetBruker = db.Kunder.FirstOrDefault(b => b.Passord == passordDb && b.Epost == innBruker.Epost);
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

        public ActionResult EditKunde(int Id)
        {
            if (Session["LoggetInn"] != null)
            {
                bool loggetInn = (bool)Session["LoggetInn"];
                int kundeID = (int)Session["KundeID"];

                if(Session["KundeID"] != null)
                {
                    if (loggetInn)
                    {
                        if (kundeID == Id)
                        {
                            dbKunde kunde = kundeDatabase.Kunder.Find(Id);
                            return View(kunde);
                        }
                    }
                }
            }
            return RedirectToAction("Index");
    
        }

        [HttpPost]
        public ActionResult EditKunde(dbKunde kunde)
        {

            var kundeSomSkalEndres = kundeDatabase.Kunder.FirstOrDefault(p => p.Id == kunde.Id);
            if (kundeSomSkalEndres == null)
                return HttpNotFound();

            kundeSomSkalEndres.Fornavn = kunde.Fornavn;
            kundeSomSkalEndres.Etternavn = kunde.Etternavn;
            kundeSomSkalEndres.Adresse = kunde.Adresse;

            string innPostnr = kunde.Poststed.Postnr;

            var funnetPostSted = kundeDatabase.Poststeder.FirstOrDefault(p => p.Postnr == innPostnr);
            if (funnetPostSted == null) // fant ikke poststed, må legge inn et nytt
            {
                var nyttPoststed = new Models.PostSted();
                nyttPoststed.Postnr = kunde.Poststed.Postnr;
                nyttPoststed.Poststed = kunde.Poststed.Poststed;
                kundeDatabase.Poststeder.Add(nyttPoststed);
                // det nye poststedet legges i den nye brukeren
                kundeSomSkalEndres.Poststed = nyttPoststed;

            }
            else
            { // fant poststedet, legger det inn i den nye brukeren
                kundeSomSkalEndres.Poststed = funnetPostSted;
            }

            kundeDatabase.SaveChanges();
            return View(kunde);
        }

        public ActionResult VisEnKunde(int Id)
        {
            if (Session["LoggetInn"] != null)
            {
                bool loggetInn = (bool)Session["LoggetInn"];
                int kundeID = (int)Session["KundeID"];

                if(Session["KundeID"] != null)
                {
                    if (loggetInn)
                    {
                        if (kundeID == Id)
                        {
                            dbKunde kunde = kundeDatabase.Kunder.Find(Id);
                            return View(kunde);
                        }
                    }
                }
            }
            return RedirectToAction("Index");
        }
        
        public ActionResult VisOrdreDetaljer(int id)
        {
            if (Session["LoggetInn"] != null)
            {
                bool loggetInn = (bool)Session["LoggetInn"];

                if(Session["KundeID"] != null)
                {
                    int kundeID = (int)Session["KundeID"];
                    dbKunde kunde = kundeDatabase.Kunder.Find(kundeID);

                    if (loggetInn)
                    {
                        if (kundeID == kunde.Id)
                        {
                            Bestilling bestilling = db.Bestillinger.Find(id);
                            var bokerModel = db.Bestillinger.Include("BestillingsDetaljer").Single(g => g.BestillingsID == id);

                            return View(bokerModel);
                        }
                    }
                }
            }
            return RedirectToAction("Index");

        }
        
        public ActionResult VisOrdre(int id)
        {
            if (Session["LoggetInn"] != null)
            {
                bool loggetInn = (bool)Session["LoggetInn"];
                dbKunde kunde = kundeDatabase.Kunder.Find(id);

                if (Session["KundeID"] != null)
                {
                    int kundeID = (int)Session["KundeID"];

                    if (loggetInn)
                    {

                        if (kundeID == kunde.Id)
                        {
                            var kundesOrdre = db.Bestillinger.Include("BestillingsDetaljer").Where(b => b.KundeId == kunde.Epost);

                            return View(kundesOrdre);
                        }
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}