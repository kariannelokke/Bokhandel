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
    }
}
