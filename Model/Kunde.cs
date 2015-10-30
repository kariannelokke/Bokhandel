using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Design;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Model
{
    public class Kunde
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "Epost må oppgis")]
        public string Epost { get; set; }
        [Required(ErrorMessage = "Passord må oppgis")]
        public string Passord { get; set; }
        [Required(ErrorMessage = "Fornavn må oppgis")]
      
        [StringLength(50)]
        public string Fornavn { get; set; }
        [Required(ErrorMessage = "Etternavn må oppgis")]
        
        [StringLength(50)]
        public string Etternavn { get; set; }
        [Required(ErrorMessage = "Adresse må oppgis")]
      
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
       
        public string Postnr { get; set; }

        [Required(ErrorMessage = "Poststed må oppgis")]
        [StringLength(50, ErrorMessage = "Maks 50 tegn i poststed")]
       
        public string Poststed { get; set; }
    }
}
