using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.BLL;
using BookStore.Model;

namespace BookStore.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            if (Session["AdminLoggetInn"] == null)
            {
                Session["AdminLoggetInn"] = false;
            }
  
            return View();
        }

        [HttpPost]
        public ActionResult Index(Administratoren innAdmin)
        {
            AdminBLL AdminBLL = new AdminBLL();
            Administrator admin = new Administrator();
            admin = AdminBLL.Bruker_i_DB(innAdmin);
            if (admin != null)
            {
                Session["AdminLoggetInn"] = true;
                Session["AdminID"] = admin.Id;
            
                return RedirectToAction("adminSide");
            }
            else
            {
                Session["AdminLoggetInn"] = false;
                return View();
            }
        }

        public ActionResult adminSide()
        {
            if (Session["AdminLoggetInn"] != null)
            {
                bool loggetInn = (bool)Session["AdminLoggetInn"];
                int adminID = (int)Session["AdminID"];

                if (Session["AdminID"] != null)
                {
                    if (loggetInn)
                    {
                        return View();
                    }
                }
            }  
            return RedirectToAction("Index");       
        }

        public ActionResult registrerAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult registrerAdmin(Administratoren innAdmin)
        {
            if (ModelState.IsValid)
            {
                var adminDb = new AdminBLL();
                bool insertOK = adminDb.settInnAdmin(innAdmin);
                if (insertOK)
                {
                    return RedirectToAction("hentAlleBoker");
                }
            }
            return View();
        }

        public ActionResult loggUt()
        {
            Session["LoggetInn"] = false;
            Session["KundeID"] = null;
            Session["Kunde"] = null;
            return View();
        }

        public ActionResult Liste()
        {
            var adminDb = new AdminBLL();
            List<Kunde> alleKunder = adminDb.hentAlle();
            return View(alleKunder);
        }

        public ActionResult EndreKunde(int id)
        {
            var kundeDb = new AdminBLL();
            Kunde enKunde = kundeDb.hentEnKunde(id);
            return View(enKunde);
        }

        [HttpPost]
        public ActionResult EndreKunde(int id, Kunde endreKunde)
        {
            if (ModelState.IsValid)
            {
                var Admin = new AdminBLL();
                bool endringOK = Admin.endreKunde(id, endreKunde);
                if (endringOK)
                {
                    return RedirectToAction("Liste");
                }
            }
            return View();
        }

        public ActionResult Slett(int id)
        {
            var Admin = new AdminBLL();
            Kunde enKunde = Admin.hentEnKunde(id);
            return View(enKunde);
        }

        [HttpPost]
        public ActionResult Slett(int id, Kunde slettKunde)
        {
            var Admin = new AdminBLL();
            bool slettOK = Admin.slettKunde(id);
            if (slettOK)
            {
                return RedirectToAction("Liste");
            }
            return View();
        }

        public ActionResult VisOrdre(int id)
        {
            var Admin = new AdminBLL();
            List<Bestilling> ordre = Admin.hentAlleOrdre(id);
            Session["KundId"] = id;
            return View(ordre);
        }

        public ActionResult VisOrdreDetaljer(int id)
        {
            var Admin = new AdminBLL();
            Bestilling ordreDetaljer = Admin.hentAlleOrdreDetaljer(id);
            return View(ordreDetaljer);
        }

        public ActionResult hentAlleBoker()
        {
            var Admin = new AdminBLL();
            List<Boken> boker = Admin.hentAlleBoker();
            return View(boker);

        }
    
        public ActionResult EndreBok(int id)
        {
            var bokDb = new AdminBLL();
            Boken enBok = bokDb.hentEnBok(id);
            return View(enBok);
        }

        [HttpPost]
        public ActionResult EndreBok(int id, Boken endreBok)
        {
            if (ModelState.IsValid)
            {
                var Admin = new AdminBLL();
                bool endringOK = Admin.endreBok(id, endreBok);
                if (endringOK)
                {
                    return RedirectToAction("hentAlleBoker");
                }
            }
            return View();
        }

        public ActionResult registrerBok()
        {
            return View();
        }

        [HttpPost]
        public ActionResult registrerBok(Boken innBok)
        {
            if (ModelState.IsValid)
            {
                var adminDb = new AdminBLL();
                bool insertOK = adminDb.settInnBok(innBok);
                if (insertOK)
                {
                    return RedirectToAction("hentAlleBoker");
                }
            }
            return View();
        }

        public ActionResult SlettBok(int id)
        {
            var Admin = new AdminBLL();
            Boken enBok = Admin.hentEnBok(id);
            return View(enBok);
        }

        [HttpPost]
        public ActionResult SlettBok(int id, Boken slettBok)
        {
            var Admin = new AdminBLL();
            bool slettOK = Admin.slettBok(id);
            if (slettOK)
            {
                return RedirectToAction("hentAlleBoker");
            }
            return View();
        }

        public ActionResult registrerSjanger()
        {
            return View();
        }

        [HttpPost]
        public ActionResult registrerSjanger(Sjangeren innSjanger)
        {
            if (ModelState.IsValid)
            {
                var adminDb = new AdminBLL();
                bool insertOK = adminDb.settInnSjanger(innSjanger);
                if (insertOK)
                {
                    return RedirectToAction("hentSjangere");
                }
            }
            return View();
        }

        public ActionResult hentSjangere()
        {
            var adminDb = new AdminBLL();
            List<Sjangeren> alleSjangere = adminDb.hentSjangere();
            return View(alleSjangere);
        }

        public ActionResult EndreSjanger(int id)
        {
            var Db = new AdminBLL();
            Sjangeren enSjanger = Db.hentEnSjanger(id);
            return View(enSjanger);
        }

        [HttpPost]
        public ActionResult EndreSjanger(int id, Sjangeren endreSjanger)
        {
            if (ModelState.IsValid)
            {
                var Admin = new AdminBLL();
                bool endringOK = Admin.endreSjanger(id, endreSjanger);
                if (endringOK)
                {
                    return RedirectToAction("hentSjangere");
                }
            }
            return View();
        }

        public ActionResult registrerForfatter()
        {
            return View();
        }

        [HttpPost]
        public ActionResult registrerForfatter(Forfatteren innForfatter)
        {
            if (ModelState.IsValid)
            {
                var adminDb = new AdminBLL();
                bool insertOK = adminDb.settInnForfatter(innForfatter);
                if (insertOK)
                {
                    return RedirectToAction("hentForfattere");
                }
            }
            return View();
        }

        public ActionResult hentForfattere()
        {
            var adminDb = new AdminBLL();
            List<Forfatteren> alleForfattere = adminDb.hentForfattere();
            return View(alleForfattere);
        }

        public ActionResult EndreForfatter(int id)
        {
            var Db = new AdminBLL();
            Forfatteren enForfatter = Db.hentEnForfatter(id);
            return View(enForfatter);
        }

        [HttpPost]
        public ActionResult EndreForfatter(int id, Forfatteren endreForfatter)
        {
            if (ModelState.IsValid)
            {
                var Admin = new AdminBLL();
                bool endringOK = Admin.endreForfatter(id, endreForfatter);
                if (endringOK)
                {
                    return RedirectToAction("hentForfattere");
                }
            }
            return View();
        }

    }
}