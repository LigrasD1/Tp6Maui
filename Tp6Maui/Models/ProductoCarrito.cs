using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp6Maui.Models
{
    public class ProductoCarrito
    {
        public int ProductoId { get; set; }
        public string? titulo { get; set; }
        public int precio { get; set; }
        public int cantidad { get; set; }
        public string? imagen { get; set; }
        public int total { get; set; }
    }
}
