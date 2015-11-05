using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using BookStore.Model;

namespace BookStore.DAL
{
    public class AdminContext : DbContext
    {
        public AdminContext() : base("name=Boker")
        {
            Database.CreateIfNotExists();
            Database.SetInitializer<AdminContext>(null);
        }

        public DbSet<Bok> Boker { get; set; }
        public DbSet<Sjanger> Sjangere { get; set; }
        public DbSet<Bestilling> Bestillinger { get; set; }
        public DbSet<BestillingsDetaljer> BestillingsDetaljerna { get; set; }
        public DbSet<Forfatter> Forfattere { get; set; }
        public DbSet<Administrator> Administratorer { get; set; }

    }
}