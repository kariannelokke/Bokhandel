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
        public List<Model.dbKunde> hentAlle()
        {
            var db = new KundeContext();
           
            List<Model.dbKunde> alleKunder = db.Kunder.Select(k => new Model.dbKunde()
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
