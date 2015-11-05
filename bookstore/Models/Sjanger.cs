using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    [TrackChanges]
    public class Sjanger
    {
        public int SjangerId { get; set; }
        public string Navn { get; set; }
        public string Description { get; set; }
        public List<Bok> Boker { get; set; }
    }
}