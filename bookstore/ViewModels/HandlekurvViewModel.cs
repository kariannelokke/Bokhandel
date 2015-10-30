using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Models;

namespace BookStore.ViewModels
{
    public class HandlekurvViewModel
    {
        public List<Kurv> Varer { get; set; }
        public decimal KurvTotal { get; set; }
    }
}