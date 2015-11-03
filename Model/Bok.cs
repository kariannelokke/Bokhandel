using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Model
{
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

    public class Boken
    {
        [Key]
        public int ISBN { get; set; }
        public int ForfatterId { get; set; }
        public int SjangerId { get; set; }

        [Display(Name = "Tittel")]
        [Required(ErrorMessage = "Tittel må oppgis")]
        public string Tittel { get; set; }

        [Display(Name = "Pris")]
        [Required(ErrorMessage = "Pris må oppgis")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Pris må være et tall")]
        public decimal Pris { get; set; }

        [Display(Name = "Sjanger")]
        [Required(ErrorMessage = "Sjanger må oppgis")]
        public string Sjanger { get; set; }

        [Display(Name = "Forfatter fornavn")]
        [Required(ErrorMessage = "Forfatter fornavn må oppgis")]
        public string ForfatterFornavn { get; set; }

        [Display(Name = "Forfatter etternavn")]
        [Required(ErrorMessage = "Forfatter etternavn må oppgis")]
        public string ForfatterEtternavn { get; set; }
    }
}