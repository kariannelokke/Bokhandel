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
        public List<dbKunde> hentAlle()
        {
            var db = new KundeContext();
           
            List<dbKunde> alleKunder = db.Kunder.Select(k => new dbKunde()
            {
                Id = k.Id,
                Fornavn = k.Fornavn,
                Etternavn = k.Etternavn,
                Adresse = k.Adresse
            }
            
                ).ToList();
            return alleKunder;
        }
    }
}
