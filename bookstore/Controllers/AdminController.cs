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
        private IAdminLogikk _adminBLL;

        public AdminController()
        {
            _adminBLL = new AdminBLL();
        }

        public AdminController(IAdminLogikk stub)
        {
            _adminBLL = stub;
        }
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

            Administrator admin = new Administrator();
            admin = _adminBLL.Bruker_i_DB(innAdmin);
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

                if (loggetInn)
                {
                    return View();
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

                bool insertOK = _adminBLL.settInnAdmin(innAdmin);
                if (insertOK)
                {
                    return RedirectToAction("hentAlleBoker");
                }
            }
            return View();
        }

        public ActionResult loggUtadmin()
        {
            Session["AdminLoggetInn"] = false;
            Session["AdminID"] = null;
            return View();
        }

        public ActionResult Liste()
        {
            if (Session["AdminLoggetInn"] != null)
            {
                bool loggetInn = (bool)Session["AdminLoggetInn"];

                if (loggetInn)
                {

                    List<Kunde> alleKunder = _adminBLL.hentAlle();
                    return View(alleKunder);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult EndreKunde(int id)
        {
            if (Session["AdminLoggetInn"] != null)
            {
                bool loggetInn = (bool)Session["AdminLoggetInn"];

                if (loggetInn)
                {

                    Kunde enKunde = _adminBLL.hentEnKunde(id);
                    return View(enKunde);
                }

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EndreKunde(int id, Kunde endreKunde)
        {
            if (ModelState.IsValid)
            {
                bool endringOK = _adminBLL.endreKunde(id, endreKunde);
                if (endringOK)
                {
                    return RedirectToAction("Liste");
                }
            }
            return View();
        }

        public ActionResult Slett(int id)
        {
            if (Session["AdminLoggetInn"] != null)
            {
                bool loggetInn = (bool)Session["AdminLoggetInn"];

                if (loggetInn)
                {
                    Kunde enKunde = _adminBLL.hentEnKunde(id);
                    return View(enKunde);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Slett(int id, Kunde slettKunde)
        {
            bool slettOK = _adminBLL.slettKunde(id);
            if (slettOK)
            {
                return RedirectToAction("Liste");
            }
            return View();
        }

        public ActionResult SlettBok(int id)
        {
            if (Session["AdminLoggetInn"] != null)
            {
                bool loggetInn = (bool)Session["AdminLoggetInn"];

                if (loggetInn)
                {

                    Boken enBok = _adminBLL.hentEnBok(id);
                    return View(enBok);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult SlettBok(int id, Boken slettBok)
        {

            bool slettOK = _adminBLL.slettBok(id);
            if (slettOK)
            {
                return RedirectToAction("hentAlleBoker");
            }
            return View();
        }

        public ActionResult hentAlleBoker()
        {
            if (Session["AdminLoggetInn"] != null)
            {
                bool loggetInn = (bool)Session["AdminLoggetInn"];
                if (loggetInn)
                {

                    List<Boken> boker = _adminBLL.hentAlleBoker();
                    return View(boker);
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult EndreBok(int id)
        {
            if (Session["AdminLoggetInn"] != null)
            {
                bool loggetInn = (bool)Session["AdminLoggetInn"];

                if (loggetInn)
                {

                    Boken enBok = _adminBLL.hentEnBok(id);
                    return View(enBok);
                }

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EndreBok(int id, Boken endreBok)
        {
            if (ModelState.IsValid)
            {

                bool endringOK = _adminBLL.endreBok(id, endreBok);
                if (endringOK)
                {
                    return RedirectToAction("hentAlleBoker");
                }
            }
            return View();
        }

        public ActionResult registrerBok()
        {
            if (Session["AdminLoggetInn"] != null)
            {
                bool loggetInn = (bool)Session["AdminLoggetInn"];

                if (loggetInn)
                {
                    return View();
                }

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult registrerBok(Boken innBok)
        {
            if (ModelState.IsValid)
            {

                bool insertOK = _adminBLL.settInnBok(innBok);
                if (insertOK)
                {
                    return RedirectToAction("hentAlleBoker");
                }
            }
            return View();
        }
        public ActionResult registrerSjanger()
        {
            if (Session["AdminLoggetInn"] != null)
            {
                bool loggetInn = (bool)Session["AdminLoggetInn"];

                if (loggetInn)
                {
                    return View();
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult registrerSjanger(Sjangeren innSjanger)
        {
            if (ModelState.IsValid)
            {

                bool insertOK = _adminBLL.settInnSjanger(innSjanger);
                if (insertOK)
                {
                    return RedirectToAction("hentSjangere");
                }
            }
            return View();
        }

        public ActionResult hentSjangere()
        {
            if (Session["AdminLoggetInn"] != null)
            {
                bool loggetInn = (bool)Session["AdminLoggetInn"];

                if (loggetInn)
                {

                    List<Sjangeren> alleSjangere = _adminBLL.hentSjangere();
                    return View(alleSjangere);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult EndreSjanger(int id)
        {
            if (Session["AdminLoggetInn"] != null)
            {
                bool loggetInn = (bool)Session["AdminLoggetInn"];

                if (loggetInn)
                {

                    Sjangeren enSjanger = _adminBLL.hentEnSjanger(id);
                    return View(enSjanger);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EndreSjanger(int id, Sjangeren endreSjanger)
        {
            if (ModelState.IsValid)
            {

                bool endringOK = _adminBLL.endreSjanger(id, endreSjanger);
                if (endringOK)
                {
                    return RedirectToAction("hentSjangere");
                }
            }
            return View();
        }
        public ActionResult SlettSjanger(int id)
        {
            if (Session["AdminLoggetInn"] != null)
            {
                bool loggetInn = (bool)Session["AdminLoggetInn"];

                if (loggetInn)
                {

                    Sjangeren enKunde = _adminBLL.hentEnSjanger(id);
                    return View(enKunde);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult SlettSjanger(int id, Sjangeren slettSjanger)
        {

            bool slettOK = _adminBLL.slettSjanger(id);
            if (slettOK)
            {
                return RedirectToAction("hentSjangere");
            }
            return View();
        }


        public ActionResult registrerForfatter()
        {
            if (Session["AdminLoggetInn"] != null)
            {
                bool loggetInn = (bool)Session["AdminLoggetInn"];

                if (loggetInn)
                {
                    return View();
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult registrerForfatter(Forfatteren innForfatter)
        {
            if (ModelState.IsValid)
            {

                bool insertOK = _adminBLL.settInnForfatter(innForfatter);
                if (insertOK)
                {
                    return RedirectToAction("hentForfattere");
                }
            }
            return View();
        }

        public ActionResult hentForfattere()
        {
            if (Session["AdminLoggetInn"] != null)
            {
                bool loggetInn = (bool)Session["AdminLoggetInn"];

                if (loggetInn)
                {

                    List<Forfatteren> alleForfattere = _adminBLL.hentForfattere();
                    return View(alleForfattere);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult EndreForfatter(int id)
        {
            if (Session["AdminLoggetInn"] != null)
            {
                bool loggetInn = (bool)Session["AdminLoggetInn"];

                if (loggetInn)
                {

                    Forfatteren enForfatter = _adminBLL.hentEnForfatter(id);
                    return View(enForfatter);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EndreForfatter(int id, Forfatteren endreForfatter)
        {
            if (ModelState.IsValid)
            {

                bool endringOK = _adminBLL.endreForfatter(id, endreForfatter);
                if (endringOK)
                {
                    return RedirectToAction("hentForfattere");
                }
            }
            return View();
        }

        public ActionResult SlettForfatter(int id)
        {
            if (Session["AdminLoggetInn"] != null)
            {
                bool loggetInn = (bool)Session["AdminLoggetInn"];

                if (loggetInn)
                {

                    Forfatteren enForfatter = _adminBLL.hentEnForfatter(id);
                    return View(enForfatter);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult SlettForfatter(int id, Forfatteren slettForfatter)
        {

            bool slettOK = _adminBLL.slettForfatter(id);
            if (slettOK)
            {
                return RedirectToAction("hentForfattere");
            }
            return View();
        }


        public ActionResult VisOrdre(int id)
        {
            if (Session["AdminLoggetInn"] != null)
            {
                bool loggetInn = (bool)Session["AdminLoggetInn"];

                if (loggetInn)
                {

                    List<Bestilling> ordre = _adminBLL.hentAlleOrdre(id);
                    Session["KundId"] = id;
                    return View(ordre);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult VisOrdreDetaljer(int id)
        {
            if (Session["AdminLoggetInn"] != null)
            {
                bool loggetInn = (bool)Session["AdminLoggetInn"];
                if (loggetInn)
                {

                    Bestilling ordreDetaljer = _adminBLL.hentAlleOrdreDetaljer(id);
                    return View(ordreDetaljer);
                }
            }
            return RedirectToAction("Index");
        }

    }
}