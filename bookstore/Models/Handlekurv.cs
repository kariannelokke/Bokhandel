using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Models
{
    public partial class Handlekurv
    {
        BokerContext db = new BokerContext();

        string HandlekurvID { get; set; }

        public const string HandleSessionID = "KurvID";

        public static Handlekurv GetKurv(HttpContextBase context)
        {
            var kurv = new Handlekurv();
            kurv.HandlekurvID = kurv.GetKurvID(context);
            return kurv;
        }

        public static Handlekurv GetKurv(Controller controller)
        {
            return GetKurv(controller.HttpContext);
        }

        public void leggIKurv(Bok bok)
        {
            var vare = db.Kurver.SingleOrDefault(v => v.KurvID == HandlekurvID && v.ISBN == bok.ISBN);

            if(vare == null)
            {
                vare = new Kurv
                {
                    ISBN = bok.ISBN,
                    KurvID = HandlekurvID,
                    Count = 1,
                    Datum = DateTime.Now
                };

                db.Kurver.Add(vare);
            }
            else
            {
                vare.Count++;
            }
            db.SaveChanges();
        }

        public int FjernFraKurv(int id)
        {
            var vare = db.Kurver.Single(kurv => kurv.KurvID == HandlekurvID && kurv.VareID == id);

            int itemCount = 0;

            if(vare != null)
            {
                if (vare.Count > 1)
                {
                    vare.Count--;
                    itemCount = vare.Count;
                }
                else
                {
                    db.Kurver.Remove(vare);
                }
                db.SaveChanges();
            }
            return itemCount;
        }

        public void TomKurv()
        {
            var varer = db.Kurver.Where(kurv => kurv.KurvID == HandlekurvID);

            foreach (var vare in varer)
            {
                db.Kurver.Remove(vare);
            }
            db.SaveChanges();
        }

        public List<Kurv> GetVarer()
        {
            return db.Kurver.Where(kurv => kurv.KurvID == HandlekurvID).ToList();
        }

        public decimal GetTotal()
        {
            decimal? total = (from varer in db.Kurver where varer.KurvID == HandlekurvID select (int?)varer.Count * varer.Bok.Pris).Sum();

            return total ?? decimal.Zero;
        }

        public int SkapaBestilling(Bestilling bestilling)
        {
            decimal bestillingarTotalt = 0;

            var varer = GetVarer();

            foreach (var vare in varer)
            {
                var bestillingsDetalj = new BestillingsDetaljer
                {
                    ISBN = vare.ISBN,
                    BestillingsID = bestilling.BestillingsID,
                    PrisPerBok = vare.Bok.Pris,
                    Antall = vare.Count
                };

                bestillingarTotalt += (vare.Count * vare.Bok.Pris);

                db.BestillingsDetaljerna.Add(bestillingsDetalj);
            }

            bestilling.Total = bestillingarTotalt;

            db.SaveChanges();
            TomKurv();

            return bestilling.BestillingsID;
        }

        public string GetKurvID(HttpContextBase context)
        {
            if (context.Session[HandleSessionID] == null)
            {
                if(!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[HandleSessionID] = context.User.Identity.Name;
                }
                else
                {
                    Guid middlertidligKurvID = Guid.NewGuid();

                    context.Session[HandleSessionID] = middlertidligKurvID.ToString();
                }
            }
            return context.Session[HandleSessionID].ToString();
        }

        public void MigreraKurv(string brukernavn) // når bruker er logget på, flytte kurven till deres "bruker"
        {
            var handleKurv = db.Kurver.Where(k => k.KurvID == HandlekurvID);

            foreach (Kurv vare in handleKurv)
            {
                vare.KurvID = brukernavn;
            }
            db.SaveChanges();
        }
    }

   
}