using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Model
{
    
    public class Bestilling
    {
        [Key]  
        [ScaffoldColumn(false)]
        public int BestillingsID { get; set; }
        [ScaffoldColumn(false)]
        public string KundeId { get; set; }
        [ScaffoldColumn(false)]
        public System.DateTime BestillingsDato { get; set; }
        [ScaffoldColumn(false)]
        public decimal Total { get; set; }
        public List<BestillingsDetaljer> BestillingsDetaljer { get; set; }
    }
}