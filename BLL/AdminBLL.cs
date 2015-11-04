using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Model;
using BookStore.DAL;

namespace BookStore.BLL
{
    public class AdminBLL
    {
        public List<Kunde> hentAlle()
        {
            var AdminDAL = new AdminDAL();
            List<Kunde> alleKunder = AdminDAL.hentAlle();
            return alleKunder;
        }
        public bool endreKunde(int id, Kunde innKunde)
        {
            var AdminDAL = new AdminDAL();
            return AdminDAL.endreKunde(id, innKunde);
        }

        public bool slettKunde(int slettId)
        {
            var AdminDAL = new AdminDAL();
            return AdminDAL.slettKunde(slettId);
        }

        public Kunde hentEnKunde(int id)
        {
            var AdminDAL = new AdminDAL();
            return AdminDAL.hentEnKunde(id);
        }

        public List<Bestilling> hentAlleOrdre(int id)
        {
            var AdminDAL = new AdminDAL();
            List<Bestilling> alleOrdre = AdminDAL.hentAlleOrdre(id);
            return alleOrdre;
        }

        public Bestilling hentAlleOrdreDetaljer(int id)
        {
            var AdminDal = new AdminDAL();
            Bestilling ordrensDetaljer = AdminDal.hentAlleOrdreDetaljer(id);
            return ordrensDetaljer;
        }

        public List<Boken> hentAlleBoker()
        {
            var AdminDal = new AdminDAL();
            List<Boken> boker = AdminDal.hentAlleBoker();
            return boker;
        }

        public bool endreBok(int id, Boken innBok)
        {
            var AdminDAL = new AdminDAL();
            return AdminDAL.endreBok(id, innBok);
        }

        public Boken hentEnBok(int id)
        {
            var AdminDAL = new AdminDAL();
            return AdminDAL.hentEnBok(id);
        }

        public bool settInnBok(Boken innBok)
        {
            var AdminDAL = new AdminDAL();
            return AdminDAL.settInnBok(innBok);
        }

        public bool settInnSjanger(Sjangeren innSjanger)
        {
            var AdminDAL = new AdminDAL();
            return AdminDAL.settInnSjanger(innSjanger);
        }

        public bool slettBok(int slettId)
        {
            var AdminDAL = new AdminDAL();
            return AdminDAL.slettBok(slettId);
        }
        public List<Sjangeren> hentSjangere()
        {
            var AdminDAL = new AdminDAL();
            List<Sjangeren> alleSjangere = AdminDAL.hentSjangere();
            return alleSjangere;
        }

        public bool endreSjanger(int id, Sjangeren innSjanger)
        {
            var AdminDAL = new AdminDAL();
            return AdminDAL.endreSjanger(id, innSjanger);
        }

        public Sjangeren hentEnSjanger(int id)
        {
            var AdminDAL = new AdminDAL();
            return AdminDAL.hentEnSjanger(id);
        }
        public bool settInnForfatter(Forfatteren innForfatter)
        {
            var AdminDAL = new AdminDAL();
            return AdminDAL.settInnForfatter(innForfatter);
        }

        public List<Forfatteren> hentForfattere()
        {
            var AdminDAL = new AdminDAL();
            List<Forfatteren> alleForfattere = AdminDAL.hentForfattere();
            return alleForfattere;
        }

        public bool endreForfatter(int id, Forfatteren innForfatter)
        {
            var AdminDAL = new AdminDAL();
            return AdminDAL.endreForfatter(id, innForfatter);
        }

        public Forfatteren hentEnForfatter(int id)
        {
            var AdminDAL = new AdminDAL();
            return AdminDAL.hentEnForfatter(id);
        }

    }
}
