﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp6Maui.Models
{
    public class Usuario
    {
        public int id { get; set; }
        public string? email { get; set; }
        public string? username { get; set; }
        public string? name { get; set; }
        public string? phone { get; set; }
        public string? rol { get; set; }
        public int? idrol { get; set; }
    }
}
