using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp6Maui.Models;
using Tp6Maui.ModelsDTO;

namespace Tp6Maui.Services
{
    public interface IProductoServices
    {
        public Task<IEnumerable<Producto>> GetProductosAsync();
        public Task<Producto> PostProductosAsync(ProductoDTO producto);
        public Task<Producto> PutProductosAsync(int ProductoId, ProductoDTO producto);
        public Task<Producto> SearchByIdAsync(int id);
        public Task<string>BorrarProducto(int id);
        public Task<IEnumerable<ProductoCarrito>> GetProductoCarrito(int id);
        public Task<string> RemoverProductoAsync(int Userid, int productoid);
        public Task<string> ComprarCarroAsync(int Userid);
        public Task<string> AgregarAlCarritoAsync(int userId, int productoid, int cantidad);

    }
}
