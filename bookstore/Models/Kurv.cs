using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Kurv
    {
        [Key]
        public int VareID {get; set;}
        public string KurvID { get; set; }
        public int ISBN { get; set; }
        public int Count { get; set; }
        public System.DateTime Datum { get; set; }

        public virtual Bok Bok { get; set; }

    }
}