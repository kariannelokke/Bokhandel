﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    [TrackChanges]
    public class Forfatter
    {
        public int ForfatterId { get; set; }
        public string Navn { get; set; }
    }
}