using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using BookStore.Model;
using TrackerEnabledDbContext;

namespace BookStore.DAL2
{
    public class BokerContext : TrackerContext
    {
        public BokerContext() : base("name=Bok")
        {
            Database.CreateIfNotExists();
            Database.SetInitializer<BokerContext>(null);
        }

        public DbSet<Bok> Boker { get; set; }
        public DbSet<Sjanger> Sjangere { get; set; }
        public DbSet<Bestilling> Bestillinger { get; set; }
        public DbSet<BestillingsDetaljer> BestillingsDetaljerna { get; set; }
        public DbSet<Forfatter> Forfattere { get; set; }
        public DbSet<Administrator> Administratorer { get; set; }

    }
}