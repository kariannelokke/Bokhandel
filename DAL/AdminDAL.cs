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

        public Bok hentBokDetaljer(int id)
        {
            var db = new BokerContext();
            var bok = db.Boker.Find(id);
            return bok;
        }

        public List<Boken> hentAlleBoker()
        {
            var db = new BokerContext();
            List<Boken> alleBoker = db.Boker.Select(k => new Boken()
            {
                ISBN = k.ISBN,
                Tittel = k.Tittel,
                Pris = k.Pris,
                Sjanger = k.Sjanger.Navn,
                Forfatter = k.Forfatter.Navn

            }).ToList();

            return alleBoker;
        }

        public bool endreBok(int id, Boken innBok)
        {
            BokerContext db = new BokerContext();
            var bokSomSkalEndres = db.Boker.FirstOrDefault(p => p.ISBN == innBok.ISBN);

            if (bokSomSkalEndres == null)
                return false;

            bokSomSkalEndres.Tittel = innBok.Tittel;
            bokSomSkalEndres.Pris = innBok.Pris;


            string forfatter = innBok.Forfatter;

            var funnetForfatter = db.Forfattere.FirstOrDefault(p => p.Navn == forfatter);

            if (funnetForfatter == null) // fant ikke forfatter, må legge inn
            {
                var nyForfatter = new Forfatter();
                nyForfatter.Navn = forfatter;
                db.Forfattere.Add(nyForfatter);

                bokSomSkalEndres.Forfatter = nyForfatter;
            }
            else
            {
                bokSomSkalEndres.Forfatter = funnetForfatter;
            }

            string sjanger = innBok.Sjanger;

            var funnetSjanger = db.Sjangere.FirstOrDefault(p => p.Navn == sjanger);

            if (funnetSjanger == null) // fant ikke sjanger, må legge inn
            {
                var nySjanger = new Sjanger();
                nySjanger.Navn = sjanger;
                db.Sjangere.Add(nySjanger);

                bokSomSkalEndres.Sjanger = nySjanger;
            }
            else
            { // fant poststedet, legger det inn i den nye brukeren
                bokSomSkalEndres.Sjanger = funnetSjanger;
            }


            db.SaveChanges();
            return true;
        }

        public Boken hentEnBok(int id)
        {
            var db = new BokerContext();

            var enDbBok = db.Boker.Find(id);

            if (enDbBok == null)
            {
                return null;
            }
            else
            {
                var utBok = new Boken()
                {
                    ISBN = enDbBok.ISBN,
                    ForfatterId = enDbBok.ForfatterId,
                    SjangerId = enDbBok.SjangerId,
                    Tittel = enDbBok.Tittel,
                    Pris = enDbBok.Pris,
                    Sjanger = enDbBok.Sjanger.Navn,
                    Forfatter = enDbBok.Forfatter.Navn
                };
                return utBok;
            }
        }

        public bool settInnBok(Boken innBok)
        {
            var nyBok = new Bok()
            {
                ISBN = innBok.ISBN,
                Tittel = innBok.Tittel,
                Pris = innBok.Pris
            };

            var db = new BokerContext();
            try
            {
                var eksistererForfatter = db.Forfattere.FirstOrDefault(i => i.Navn == innBok.Forfatter);

                if (eksistererForfatter == null)
                {
                    var nyForfatter = new Forfatter()
                    {
                        Navn = innBok.Forfatter
                    };
                    nyBok.Forfatter = nyForfatter;
                    nyBok.ForfatterId = nyForfatter.ForfatterId;
                }
                else
                {
                    nyBok.Forfatter = eksistererForfatter;
                    nyBok.ForfatterId = eksistererForfatter.ForfatterId;
                }

                var eksistererSjanger = db.Sjangere.FirstOrDefault(i => i.Navn == innBok.Sjanger);

                if (eksistererSjanger == null)
                {
                    var nySjanger = new Sjanger()
                    {
                        Navn = innBok.Sjanger
                    };
                    nyBok.Sjanger = nySjanger;
                    nyBok.SjangerId = nySjanger.SjangerId;
                }
                else
                {
                    nyBok.Sjanger = eksistererSjanger;
                    nyBok.SjangerId = eksistererSjanger.SjangerId;
                }

                db.Boker.Add(nyBok);
                db.SaveChanges();
                return true;
            }
            catch (Exception feil)
            {
                return false;
            }
        }

        public bool slettBok(int slettId)
        {
            var db = new BokerContext();
            try
            {
                Bok slettBok = db.Boker.Find(slettId);
                db.Boker.Remove(slettBok);
                db.SaveChanges();
                return true;
            }
            catch (Exception feil)
            {
                return false;
            }


        }
    }
}
