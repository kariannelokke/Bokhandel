using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BookStore.Models
{
    [TrackChanges]
    public class Bok
    {
        [Key]
        public int ISBN { get; set; }
        public int ForfatterId { get; set; }
        public int SjangerId { get; set; }
        public string Tittel { get; set; }
        public decimal Pris { get; set; } 
        public virtual Sjanger Sjanger { get; set; }
        public virtual Forfatter Forfatter { get; set; }

    }
}