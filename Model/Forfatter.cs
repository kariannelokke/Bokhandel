using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Model
{
    public class Forfatter
    {
        public int ForfatterId { get; set; }
        public string Navn { get; set; }
    }

    public class Forfatteren
    {
        public int ForfatterId { get; set; }
        [Display(Name = "Navn")]
        [Required(ErrorMessage = "Navn på sjanger må oppgis")]
        public string Navn { get; set; }
    }
}