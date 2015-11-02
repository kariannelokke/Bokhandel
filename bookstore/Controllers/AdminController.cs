﻿using System;
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

    }
}