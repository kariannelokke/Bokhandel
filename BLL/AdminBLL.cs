using System;
using System.Collections.Generic;
using BookStore.Model;
using BookStore.DAL2;

namespace BookStore.BLL
{
    public class AdminBLL : BLL.IAdminLogikk
    {
        private IAdminRepository _repository;

        public AdminBLL() // ikke test
        {
            _repository = new AdminRepository();
        }

        public AdminBLL(IAdminRepository stub) // test
        {
            _repository = stub;
        }

        public bool settInnAdmin(Administratoren innAdmin)
        {

            return _repository.settInnAdmin(innAdmin);
        }

        public Administrator Bruker_i_DB(Administratoren innAdmin)
        {

            Administrator admin = _repository.Bruker_i_DB(innAdmin);
            return admin;
        }
        public List<Kunde> hentAlle()
        {     
            List<Kunde> alleKunder = _repository.hentAlle();
            return alleKunder;
        }
        public bool endreKunde(int id, Kunde innKunde)
        {

            return _repository.endreKunde(id, innKunde);
        }
        public Kunde hentEnKunde(int id)
        {
            return _repository.hentEnKunde(id);
        }

        public bool slettKunde(int slettId)
        {
            return _repository.slettKunde(slettId);
        }

        public bool slettBok(int slettId)
        {
            return _repository.slettBok(slettId);
        }

        public Boken hentEnBok(int id)
        {
            return _repository.hentEnBok(id);
        }

        public List<Boken> hentAlleBoker()
        {
            List<Boken> boker = _repository.hentAlleBoker();
            return boker;
        }

        public bool endreBok(int id, Boken innBok)
        {
            return _repository.endreBok(id, innBok);
        }

        public bool settInnBok(Boken innBok)
        {

            return _repository.settInnBok(innBok);
        }

        public bool settInnSjanger(Sjangeren innSjanger)
        {
            return _repository.settInnSjanger(innSjanger);
        }

        public List<Sjangeren> hentSjangere()
        {

            List<Sjangeren> alleSjangere = _repository.hentSjangere();
            return alleSjangere;
        }

        public bool endreSjanger(int id, Sjangeren innSjanger)
        {

            return _repository.endreSjanger(id, innSjanger);
        }

        public Sjangeren hentEnSjanger(int id)
        {

            return _repository.hentEnSjanger(id);
        }

        public bool slettSjanger(int slettId)
        {

            return _repository.slettSjanger(slettId);
        }


        public bool settInnForfatter(Forfatteren innForfatter)
        {

            return _repository.settInnForfatter(innForfatter);
        }

        public List<Forfatteren> hentForfattere()
        {

            List<Forfatteren> alleForfattere = _repository.hentForfattere();
            return alleForfattere;
        }

        public bool endreForfatter(int id, Forfatteren innForfatter)
        {

            return _repository.endreForfatter(id, innForfatter);
        }

        public Forfatteren hentEnForfatter(int id)
        {

            return _repository.hentEnForfatter(id);
        }
        public bool slettForfatter(int slettId)
        {
            return _repository.slettForfatter(slettId);
        }
        public List<Bestilling> hentAlleOrdre(int id)
        {
            List<Bestilling> alleOrdre = _repository.hentAlleOrdre(id);
            return alleOrdre;
        }

        public Bestilling hentAlleOrdreDetaljer(int id)
        {
            Bestilling ordrensDetaljer = _repository.hentAlleOrdreDetaljer(id);
            return ordrensDetaljer;
        }

    }
}
