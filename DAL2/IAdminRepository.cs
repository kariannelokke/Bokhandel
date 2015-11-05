using System;
using System.Collections.Generic;
using BookStore.Model;

namespace BookStore.DAL2
{
    public interface IAdminRepository
    {
        List<Kunde> hentAlle();
        bool endreKunde(int id, Kunde innKunde);
        Kunde hentEnKunde(int id);
        bool slettKunde(int slettId);
        List<Boken> hentAlleBoker();
        Boken hentEnBok(int id);
        bool slettBok(int slettId);
        bool endreBok(int id, Boken innBok);
        bool settInnBok(Boken innBok);
        bool settInnSjanger(Sjangeren innSjanger);
        List<Sjangeren> hentSjangere();
        bool endreSjanger(int id, Sjangeren innSjanger);
        Sjangeren hentEnSjanger(int id);
        bool slettSjanger(int slettId);
        bool settInnForfatter(Forfatteren innForfatter);
        List<Forfatteren> hentForfattere();
        bool endreForfatter(int id, Forfatteren innForfatter);
        Forfatteren hentEnForfatter(int id);
        bool slettForfatter(int slettId);
        List<Bestilling> hentAlleOrdre(int id);
        Bestilling hentAlleOrdreDetaljer(int id);
        bool settInnAdmin(Administratoren innAdmin);
        Administrator Bruker_i_DB(Administratoren innBruker);


    }
}
