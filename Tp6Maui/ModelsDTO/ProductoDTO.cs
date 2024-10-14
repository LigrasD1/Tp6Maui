using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp6Maui.ModelsDTO
{
    public class ProductoDTO
    {
        public string? title { get; set; }
        public double price { get; set; }
        public string? description { get; set; }
        public string? category { get; set; }
        public int stock { get; set; }
        public FileResult? Imagen {  get; set; }
    }
}
