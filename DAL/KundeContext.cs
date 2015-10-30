using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using BookStore.Model;

namespace BookStore.DAL
{
    public class KundeContext : DbContext
    {
        public KundeContext() : base("name=Kunder")
        {
            Database.CreateIfNotExists();
            Database.SetInitializer<KundeContext>(null);
        }
        public DbSet<dbKunde> Kunder { get; set; }
        public DbSet<PostSted> Poststeder { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}