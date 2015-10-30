using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class BestillingsDetaljer
    {
        [Key]
        public int BestillingsDetaljID { get; set; }
        public int BestillingsID { get; set; }
        public int Antall { get; set; }

        public int ISBN { get; set; }

        public decimal PrisPerBok { get; set; }

        public virtual Bok Bok { get; set; }
        public virtual Bestilling Bestilling { get; set; }
    }
}