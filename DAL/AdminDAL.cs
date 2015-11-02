using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Model;

namespace BookStore.DAL
{
    public class AdminDAL
    {

        public List<Kunde> hentAlle()
        {
            var db = new KundeContext();
            List<Kunde> alleKunder = db.Kunder.Select(k => new Kunde()
            {
                id = k.Id,
                fornavn = k.Fornavn,
                etternavn = k.Etternavn,
                adresse = k.Adresse,
                postnr = k.Poststed.Postnr,
                poststed = k.Poststed.Poststed

            }).ToList();

            return alleKunder;
        }

        public bool endreKunde(int id, Kunde innKunde)
        {
            KundeContext kundeDatabase = new KundeContext();
            var kundeSomSkalEndres = kundeDatabase.Kunder.FirstOrDefault(p => p.Id == innKunde.id);

            if (kundeSomSkalEndres == null)
                return false;

            kundeSomSkalEndres.Fornavn = innKunde.fornavn;
            kundeSomSkalEndres.Etternavn = innKunde.etternavn;
            kundeSomSkalEndres.Adresse = innKunde.adresse;

            string innPostnr = innKunde.postnr;

            var funnetPostSted = kundeDatabase.Poststeder.FirstOrDefault(p => p.Postnr == innPostnr);

            if (funnetPostSted == null) // fant ikke poststed, må legge inn et nytt
            {
                var nyttPoststed = new PostSted();
                nyttPoststed.Postnr = innKunde.postnr;
                nyttPoststed.Poststed = innKunde.poststed;
                kundeDatabase.Poststeder.Add(nyttPoststed);
                // det nye poststedet legges i den nye brukeren
                kundeSomSkalEndres.Poststed = nyttPoststed;

            }
            else
            { // fant poststedet, legger det inn i den nye brukeren
                kundeSomSkalEndres.Poststed = funnetPostSted;
            }

            kundeDatabase.SaveChanges();
            return true;
        }

        public Kunde hentEnKunde(int id)
        {
            var db = new KundeContext();

            var enDbKunde = db.Kunder.Find(id);

            if (enDbKunde == null)
            {
                return null;
            }
            else
            {
                var utKunde = new Kunde()
                {
                    id = enDbKunde.Id,
                    fornavn = enDbKunde.Fornavn,
                    etternavn = enDbKunde.Etternavn,
                    adresse = enDbKunde.Adresse,
                    postnr = enDbKunde.Poststed.Postnr,
                    poststed = enDbKunde.Poststed.Poststed
                };
                return utKunde;
            }
        }

        public bool slettKunde(int slettId)
        {
            var db = new KundeContext();
            try
            {
                dbKunde slettKunde = db.Kunder.Find(slettId);
                db.Kunder.Remove(slettKunde);
                db.SaveChanges();
                return true;
            }
            catch (Exception feil)
            {
                return false;
            }
        }

        public List<Bestilling> hentAlleOrdre(int id)
        {
            var kundeDatabase = new KundeContext();
            var db = new BokerContext();

            dbKunde kunde = kundeDatabase.Kunder.Find(id);

            var kundesOrdre = db.Bestillinger.Include("BestillingsDetaljer").Where(b => b.KundeId == kunde.Epost).ToList();

            return kundesOrdre;

        }

        public Bestilling hentAlleOrdreDetaljer(int id)
        {
            var db = new BokerContext();

            Bestilling bestilling = db.Bestillinger.Find(id);
            var bokerModel = db.Bestillinger.Include("BestillingsDetaljer").Single(g => g.BestillingsID == id);
            if (bokerModel == null)
            {
                return null;
            }
            else
            {
                return bokerModel;
            }
        }

        public Sjanger hentAlleBokerSjanger(string sjanger)
        {
            var db = new BokerContext();
            var sjangerModel = db.Sjangere.Include("Boker").Single(g => g.Navn == sjanger);
            return sjangerModel;
        }

        public List<Sjanger> hentAlleSjangere()
        {
            var db = new BokerContext();
            var sjangere = db.Sjangere.ToList();
            return sjangere;
        }
    }
}
