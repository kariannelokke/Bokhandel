using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Model
{
    public class dbKunde
    {
        [Key]
        public int Id { get; set; }
        public string Epost { get; set; }
        [ScaffoldColumn(false)]
        public byte[] Passord { get; set; }
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
}
