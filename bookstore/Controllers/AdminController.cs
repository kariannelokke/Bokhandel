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
        // GET: Admin
        public ActionResult Index()
        {
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
        public ActionResult Endre(int id, Kunde endreKunde)
        {

            if (ModelState.IsValid)
            {
                var kundeDb = new AdminBLL();
                bool endringOK = kundeDb.endreKunde(id, endreKunde);
                if (endringOK)
                {
                    return RedirectToAction("Liste");
                }
            }
            return View();
        }

    }
}