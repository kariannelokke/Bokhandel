using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace BookStore.Model
{
    public class Administrator
    {
        [Key]
        public int Id { get; set; }
        public string Brukernavn { get; set; }
        public byte[] Passord { get; set; }
    }

    //Domenemodell
    public class Administratoren
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Brukernavn")]
        [Required(ErrorMessage = "Brukernavn på sjanger må oppgis")]
        public string Brukernavn { get; set; }
        [Display(Name = "Passord")]
        [Required(ErrorMessage = "Passord på sjanger må oppgis")]
        public string Passord { get; set; }
    }
}
