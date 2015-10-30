using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.ViewModels
{
    public class HandlekurvFjernViewModel
    {
        public string Meddelande { get; set; }
        public decimal KurvTotal { get; set; }

        public int KurvCount { get; set; }
        public int VareCount { get; set; }
        public int FjernID { get; set; }

    }
}