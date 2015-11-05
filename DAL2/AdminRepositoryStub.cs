using System;
using System.Collections.Generic;
using System.Linq;
using BookStore.Model;

namespace BookStore.DAL2
{

    public class AdminRepositoryStub : DAL2.IAdminRepository
    {

        public List<Kunde> hentAlle()
        {

            var kundeListe = new List<Kunde>();
            var kunde = new Kunde()
            {
                id = 100,
                fornavn = "Ole",
                etternavn = "Olsen",
                adresse = "Storgata 1",
                postnr = "3557",
                poststed = "Molde"
            };

            kundeListe.Add(kunde);
            kundeListe.Add(kunde);
            kundeListe.Add(kunde);
            return kundeListe;

        }

        public bool endreKunde(int id, Kunde innKunde)
        {
            if (id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public Kunde hentEnKunde(int id)
        {
            if (id == 0)
            {
                var kunde = new Kunde();
                kunde.id = 0;
                return kunde;
            }
            else
            {
                var kunde = new Kunde()
                {
                    id = 100,
                    fornavn = "Ole",
                    etternavn = "Olsen",
                    adresse = "Storgata 1",
                    postnr = "3557",
                    poststed = "Molde"
                };
                return kunde;
            }
        }

        public bool slettKunde(int id)
        {
            if (id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Boken hentEnBok(int id) 
        {
            if (id == 0)
            {
                var bok = new Boken();
                bok.ISBN = 0;
                return bok;
            }
            else
            {
                var bok = new Boken()
                {
                    ISBN = 100,
                    ForfatterId = 100,
                    SjangerId = 100,
                    Tittel = "Isprinsessen",
                    Pris = 399,
                    Sjanger = "Krim",
                    Forfatter = "Camilla Läckberg"
                };
                return bok;
            }
        }

        public bool slettBok(int slettId)
        {
            if (slettId == 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public List<Boken> hentAlleBoker()
        {
            var bokListe = new List<Boken>();
            var bok = new Boken()
            {
                ISBN = 100,
                ForfatterId = 100,
                SjangerId = 100,
                Tittel = "Isprinsessen",
                Pris = 399,
                Sjanger = "Krim",
                Forfatter = "Camilla Läckberg"
            };

            bokListe.Add(bok);
            bokListe.Add(bok);
            bokListe.Add(bok);
            return bokListe;

        }

        public bool endreBok(int id, Boken innBok)
        {
            if (id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool settInnBok(Boken innBok)
        {
            if (innBok.Tittel == "")
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public Sjangeren hentEnSjanger(int id)
        {
            if (id == 0)
            {
                var sjanger = new Sjangeren();
                sjanger.SjangerId = 0;
                return sjanger;
            }
            else
            {
                var sjanger = new Sjangeren()
                {
                    Navn = "Roman",

                };
                return sjanger;
            }
        }

        public bool settInnSjanger(Sjangeren innSjanger)
        {
            if (innSjanger.Navn == "")
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public List<Sjangeren> hentSjangere()
        {
            var sjangerListe = new List<Sjangeren>();
            var sjanger = new Sjangeren()
            {
                Navn = "Roman",
            };

            sjangerListe.Add(sjanger);
            sjangerListe.Add(sjanger);
            sjangerListe.Add(sjanger);
            return sjangerListe;

        }

        public bool endreSjanger(int id, Sjangeren innSjanger)
        {
            if (id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool slettSjanger(int slettId)
        {
            if (slettId == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool settInnForfatter(Forfatteren innForfatter)
        {
            if (innForfatter.Navn == "")
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public Forfatteren hentEnForfatter(int id)
        {
            if (id == 0)
            {
                var forfattere = new Forfatteren();
                forfattere.ForfatterId = 0;
                return forfattere;
            }
            else
            {
                var forfattere = new Forfatteren()
                {
                    ForfatterId = 100,
                    Navn = "Sofia Aittamaa",

                };
                return forfattere;
            }
        }

        public bool endreForfatter(int id, Forfatteren innForfatter)
        {
            if (id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public List<Forfatteren> hentForfattere()
        {
            var forfatterListe = new List<Forfatteren>();
            var forfatter = new Forfatteren()
            {
                Navn = "Sofia Aittamaa",
            };

            forfatterListe.Add(forfatter);
            forfatterListe.Add(forfatter);
            forfatterListe.Add(forfatter);
            return forfatterListe;

        }

        public bool slettForfatter(int slettId)
        {
            if (slettId == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public List<Bestilling> hentAlleOrdre(int id)
        {
            if (id == 0)
            {
                var ordreListe = new List<Bestilling>();
                var ordre = new Bestilling();
                ordre.BestillingsID = 0;
                ordreListe.Add(ordre);
                return ordreListe;

            }
            else
            {
                var ordreListe = new List<Bestilling>();
                var ordre = new Bestilling()
                {
                    BestillingsID = 100,
                    KundeId = "sofia",
                    BestillingsDato = new DateTime(2015, 11, 04, 08, 00, 00),
                    Total = 10,
                };

                ordreListe.Add(ordre);
                ordreListe.Add(ordre);
                ordreListe.Add(ordre);
                return ordreListe;
            }
        }

        public Bestilling hentAlleOrdreDetaljer(int id)
        {
            if (id == 0)
            {
                var bestilling = new Bestilling();
                bestilling.BestillingsID = 0;
                return bestilling;
            }
            else
            {
                var bestilling = new Bestilling()
                {
                    BestillingsID = 100,
                    KundeId = "sofia",
                    BestillingsDato = new DateTime(2015, 11, 04, 08, 00, 00),
                    Total = 0,

                };
                return bestilling;
            }

        }

        public bool settInnAdmin(Administratoren innAdmin)
        {
            if (innAdmin.Brukernavn == "")
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public Administrator Bruker_i_DB(Administratoren innBruker)
        {
            if (innBruker == null)
            {
                var admin = new Administrator();
                admin.Brukernavn = null;
                return admin;
            }
            else
            {
                var admin = new Administrator()
                {
                    Id = 100,
                    Brukernavn = "Sofia Aittamaa",

                };
                return admin;
            }
        }


    }

}
