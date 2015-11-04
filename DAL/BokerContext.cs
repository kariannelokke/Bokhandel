using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using BookStore.Model;

namespace BookStore.DAL
{
    public class BokerContext : DbContext
    {
        public BokerContext() : base("name=Boker")
        {
            Database.CreateIfNotExists();
            Database.SetInitializer<BokerContext>(null);
        }

        public DbSet<Bok> Boker { get; set; }
        public DbSet<Sjanger> Sjangere { get; set; }
        public DbSet<Bestilling> Bestillinger { get; set; }
        public DbSet<BestillingsDetaljer> BestillingsDetaljerna { get; set; }
        public DbSet<Forfatter> Forfattere { get; set; }
        public DbSet<dbAdmin> Adminer { get; set; }
    }
}