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
            List<dbKunde> alleKunder = adminDb.hentAlle();
            return View(alleKunder);
        }
    }
}