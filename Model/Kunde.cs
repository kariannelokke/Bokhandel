using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Model
{
    public class Kunde
    {
        public int id { get; set; }

        [Display(Name = "Fornavn")]
        [Required(ErrorMessage = "Fornavn må oppgis")]
        public string fornavn { get; set; }

        [Display(Name = "Etternavn")]
        [Required(ErrorMessage = "Etternavn må oppgis")]
        public string etternavn { get; set; }

        [Display(Name = "Adresse")]
        [Required(ErrorMessage = "Adressen må oppgis")]
        public string adresse { get; set; }

        [Display(Name = "Postnr")]
        [Required(ErrorMessage = "Postnr må oppgis")]
        [RegularExpression(@"[0-9]{4}", ErrorMessage = "Postnr må være 4 siffer")]
        public string postnr { get; set; }

        [Display(Name = "Poststed")]
        [Required(ErrorMessage = "Poststed må oppgis")]
        public string poststed { get; set; }
    }
}
