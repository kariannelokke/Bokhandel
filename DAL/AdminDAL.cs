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
        public List<Model.Kunde> hentAlle()
        {
            var db = new KundeContext();
            List<Model.Kunde> alleKunder = db.Kunder.Select(k => new Kunde()
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

        public bool endreKunde(int id, Kunde innKunde)
        {
            KundeContext kundeDatabase = new KundeContext();
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
    
        public Kunde hentEnKunde(int id)
        {
            var db = new KundeContext();

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
                    postnr = enDbKunde.Poststed.Poststed,
                    poststed = enDbKunde.Poststed.Postnr
                };
                return utKunde;
            }
        }

        public bool slettKunde(int slettId)
        {
            var db = new KundeContext();
            try
            {
                dbKunde slettKunde = db.Kunder.Find(slettId);
                db.Kunder.Remove(slettKunde);
                db.SaveChanges();
                return true;
            }
            catch (Exception feil)
            {
                return false;
            }
        }

    }
}
