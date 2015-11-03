using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Model
{
    public class Sjanger
    {
        public int SjangerId { get; set; }
        public string Navn { get; set; }
    }

    public class Sjangeren
    {
        public int SjangerId { get; set; }
        [Display(Name = "Navn")]
        [Required(ErrorMessage = "Navn på sjanger må oppgis")]
        public string Navn { get; set; }
    }

}