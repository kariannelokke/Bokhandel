using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Model;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.IO;
using System.Diagnostics;

namespace BookStore.DAL
{
    public class AdminDAL
    {
        public bool settInnAdmin(Administratoren innAdmin)
        { 
            byte[] passordDb = lagHash(innAdmin.Passord);
            var nyAdmin = new Administrator()
            {
                Id = innAdmin.Id,
                Brukernavn = innAdmin.Brukernavn,
                Passord = passordDb
            };

            var db = new BokerContext();
            try
            {
                //db.Entry(nyAdmin).State = nyAdmin.Id == 0 ? EntityState.Added : EntityState.Modified;
                db.Administratorer.Add(nyAdmin);        
                db.SaveChanges();
                return true;
            }
            catch (Exception feil)
            {
                skrivTilFil(feil);
                return false;
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

        public Administrator Bruker_i_DB(Administratoren innBruker)
        {
            try
            {
                using (var db = new BokerContext())
                {
                    byte[] passordDb = lagHash(innBruker.Passord);
                    Administrator funnetBruker = db.Administratorer.FirstOrDefault(b => b.Passord == passordDb && b.Brukernavn == innBruker.Brukernavn);
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
            catch (Exception feil)
            {
                skrivTilFil(feil);
                return null;
            }
        }

        public List<Kunde> hentAlle()
        {
            var db = new KundeContext();
            try
            {
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
            catch (Exception feil)
            {
                skrivTilFil(feil);
                return null;
            }
        }

        public bool endreKunde(int id, Kunde innKunde)
        {
            KundeContext kundeDatabase = new KundeContext();

            try
            {
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
            catch (Exception feil)
            {
                skrivTilFil(feil);
                return false;
            }
        }

        public Kunde hentEnKunde(int id)
        {
            var db = new KundeContext();

            try
            {
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
            catch (Exception feil)
            {
                skrivTilFil(feil);
                return null;
            }
        }

        public bool slettKunde(int slettId)
        {
            var db = new KundeContext();
            var database = new BokerContext();
            try
            {
                dbKunde slettKunde = db.Kunder.Find(slettId);

                var bestillinger = database.Bestillinger.Where(c => c.KundeId == slettKunde.Epost);

                foreach (var u in bestillinger)
                {
                    database.Bestillinger.Remove(u);
                }

                database.SaveChanges();
                db.Kunder.Remove(slettKunde);
                db.SaveChanges();
                return true;
            }
            catch (Exception feil)
            {
                skrivTilFil(feil);
                return false;
            }
        }

        public List<Bestilling> hentAlleOrdre(int id)
        {
            var kundeDatabase = new KundeContext();
            try
            {
                var db = new BokerContext();

                dbKunde kunde = kundeDatabase.Kunder.Find(id);

                var kundesOrdre = db.Bestillinger.Include("BestillingsDetaljer").Where(b => b.KundeId == kunde.Epost).ToList();

                return kundesOrdre;
            }
            catch (Exception feil)
            {
                skrivTilFil(feil);
                return null;
            }

        }

        public Bestilling hentAlleOrdreDetaljer(int id)
        {
            var db = new BokerContext();

            try
            {
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
            catch (Exception feil)
            {
                skrivTilFil(feil);
                return null;
            }
        }

        public List<Boken> hentAlleBoker()
        {
            var db = new BokerContext();
            try
            {
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
            catch (Exception feil)
            {
                skrivTilFil(feil);
                return null;
            }
        }

        public bool endreBok(int id, Boken innBok)
        {
            var db = new BokerContext();
            try
            {
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
            catch (Exception feil)
            {
                skrivTilFil(feil);
                return false;
            }
        }

        public Boken hentEnBok(int id)
        {
            var db = new BokerContext();

            try
            {
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
            catch (Exception feil)
            {
                skrivTilFil(feil);
                return null;
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
                skrivTilFil(feil);
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
                skrivTilFil(feil);
                return false;
            }
        }
        public bool settInnSjanger(Sjangeren innSjanger)
        {
            var nySjanger = new Sjanger()
            {
                Navn = innSjanger.Navn,
   
            };

            var db = new BokerContext();
            try
            {
                db.Sjangere.Add(nySjanger);
                db.SaveChanges();
                return true;
            }
            catch (Exception feil)
            {
                skrivTilFil(feil);
                return false;
            }
        }

        public List<Sjangeren> hentSjangere()
        {
            var db = new BokerContext();
            try
            {
                List<Sjangeren> alleSjangere = db.Sjangere.Select(k => new Sjangeren()
                {
                    SjangerId = k.SjangerId,
                    Navn = k.Navn

                }).ToList();

                return alleSjangere;
            }
            catch (Exception feil)
            {
                skrivTilFil(feil);
                return null;
            }
        }

        public bool endreSjanger(int id, Sjangeren innSjanger)
        {
            BokerContext db = new BokerContext();
            try
            {
                var sjangerSomSkalEndres = db.Sjangere.FirstOrDefault(p => p.SjangerId == innSjanger.SjangerId);

                if (sjangerSomSkalEndres == null)
                    return false;

                sjangerSomSkalEndres.Navn = innSjanger.Navn;

                db.SaveChanges();
                return true;
            }
            catch (Exception feil)
            {
                skrivTilFil(feil);
                return false;
            }
        }

        public Sjangeren hentEnSjanger(int id)
        {
            var db = new BokerContext();
            try
            {

                var enDbSjanger = db.Sjangere.Find(id);

                if (enDbSjanger == null)
                {
                    return null;
                }
                else
                {
                    var utSjanger = new Sjangeren()
                    {
                        SjangerId = enDbSjanger.SjangerId,
                        Navn = enDbSjanger.Navn
                    };
                    return utSjanger;
                }
            }
            catch (Exception feil)
            {
                skrivTilFil(feil);
                return null;
            }
        }

        public bool slettSjanger(int slettId)
        {
            var db = new BokerContext();
            try
            {
                Sjanger slettSjanger = db.Sjangere.Find(slettId);
                var boker = db.Boker.Where(c => c.Sjanger.SjangerId == slettSjanger.SjangerId);

                foreach (var u in boker)
                {
                    db.Boker.Remove(u);
                }

                db.Sjangere.Remove(slettSjanger);
                db.SaveChanges();
                return true;
            }
            catch (Exception feil)
            {
                skrivTilFil(feil);
                return false;
            }
        }

        public bool settInnForfatter(Forfatteren innForfatter)
        {
            var nyForfatter = new Forfatter()
            {
                Navn = innForfatter.Navn,

            };

            var db = new BokerContext();
            try
            {
                db.Forfattere.Add(nyForfatter);
                db.SaveChanges();
                return true;
            }
            catch (Exception feil)
            {
                skrivTilFil(feil);
                return false;
            }
        }

        public List<Forfatteren> hentForfattere()
        {
            var db = new BokerContext();
            try
            {
                List<Forfatteren> alleSjangere = db.Forfattere.Select(k => new Forfatteren()
                {
                    ForfatterId = k.ForfatterId,
                    Navn = k.Navn

                }).ToList();

                return alleSjangere;
            }
            catch (Exception feil)
            {
                skrivTilFil(feil);
                return null;
            }
        }

        public bool endreForfatter(int id, Forfatteren innForfatter)
        {
            BokerContext db = new BokerContext();
            try
            {
                var forfatterSomSkalEndres = db.Forfattere.FirstOrDefault(p => p.ForfatterId == innForfatter.ForfatterId);

                if (forfatterSomSkalEndres == null)
                    return false;

                forfatterSomSkalEndres.Navn = innForfatter.Navn;

                db.SaveChanges();
                return true;
            }
            catch (Exception feil)
            {
                skrivTilFil(feil);
                return false;
            }

        }

        public Forfatteren hentEnForfatter(int id)
        {
            var db = new BokerContext();

            var enDbForfatter = db.Forfattere.Find(id);

            if (enDbForfatter == null)
            {
                return null;
            }
            else
            {
                var utForfatter = new Forfatteren()
                {
                    ForfatterId = enDbForfatter.ForfatterId,
                    Navn = enDbForfatter.Navn
                };
                return utForfatter;
            }
        }

        public bool slettForfatter(int slettId)
        {
            var db = new BokerContext();
            try
            {
                Forfatter slettForfatter = db.Forfattere.Find(slettId);

                var boker = db.Boker.Where(c => c.Forfatter.ForfatterId == slettForfatter.ForfatterId);

                foreach(var u in boker)
                {
                    db.Boker.Remove(u);
                }

                db.Forfattere.Remove(slettForfatter);
                db.SaveChanges();
                return true;
            }
            catch (Exception feil)
            {
                skrivTilFil(feil);
                return false;
            }
        }

        private void skrivTilFil(Exception e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"feilLogg.txt";
            Debug.WriteLine(path);
            try
            {
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(" - " + DateTime.Now.ToString() + " - ");
                    writer.WriteLine("");
                    writer.WriteLine("Message: " + e.Message + Environment.NewLine
                        + "Stacktrace: " + e.StackTrace + Environment.NewLine);
                }
            }
            catch (IOException ioe)
            {
                Debug.WriteLine(ioe.Message);
            }
            catch (UnauthorizedAccessException uae)
            {
                Debug.WriteLine(uae.Message);
            }
        }
    }
}
