using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BookStore.Models
{
    public class BokerContext : DbContext
    {
        public BokerContext() : base("name=Boker")
        {
            Database.CreateIfNotExists();
            Database.SetInitializer<KundeContext>(null);
        }

        public DbSet<Bok> Boker { get; set; }
        public DbSet<Sjanger> Sjangere { get; set; }
        public DbSet<Kurv> Kurver { get; set; }
        public DbSet<Bestilling> Bestillinger { get; set; }
        public DbSet<BestillingsDetaljer> BestillingsDetaljerna { get; set; }
    }
}