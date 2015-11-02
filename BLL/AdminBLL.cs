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
    }
}
