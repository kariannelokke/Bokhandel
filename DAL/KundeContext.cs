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
    public class dbKunde
    {
        [Key]
        public int Id { get; set; }
        public string Epost { get; set; }
        [ScaffoldColumn(false)]
        public byte[] Passord { get; set; }

        [Required(ErrorMessage = "Fornavn må oppgis")]
        [DisplayName("Fornavn")]
        [StringLength(50)]
        public string Fornavn { get; set; }

        [Required(ErrorMessage = "Etternavn må oppgis")]
        [DisplayName("Etternavn")]
        [StringLength(50)]
        public string Etternavn { get; set; }

        [Required(ErrorMessage = "Adresse må oppgis")]
        [DisplayName("Adresse")]
        [StringLength(50)]
        public string Adresse { get; set; }

        [Required]
        public virtual PostSted Poststed { get; set; }
    }
    public class PostSted
    {
        [Key]
        [Required(ErrorMessage = "Postnr må oppgis")]
        [RegularExpression(@"[0-9]{4}", ErrorMessage = "Postnr må være 4 siffer")]
        [DisplayName("Postnummer")]
        public string Postnr { get; set; }

        [Required(ErrorMessage = "Poststed må oppgis")]
        [StringLength(50, ErrorMessage = "Maks 50 tegn i poststed")]
        [DisplayName("Poststed")]
        public string Poststed { get; set; }
    }

    public class KundeContext : DbContext
    {
        public KundeContext()
        : base("name=Kunder")
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