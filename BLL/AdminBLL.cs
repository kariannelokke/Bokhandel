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

        public Sjanger hentAlleBokerSjanger(string sjanger)
        {
            var AdminDal = new AdminDAL();
            Sjanger boker = AdminDal.hentAlleBokerSjanger(sjanger);
            return boker;
        }

        public List<Sjanger> hentAlleSjangere()
        {
            var AdminDal = new AdminDAL();
            List<Sjanger> sjangere = AdminDal.hentAlleSjangere();
            return sjangere;
        }

        public Bok hentBokDetaljer(int id)
        {
            var AdminDal = new AdminDAL();
            Bok bok = AdminDal.hentBokDetaljer(id);
            return bok;
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
    }
}
