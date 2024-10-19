using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Tp6Maui.Models;
using Tp6Maui.ModelsDTO;
using Tp6Maui.Utils;

namespace Tp6Maui.Services
{
    internal class ProductoServices : IProductoServices
    {
        HttpClient client = new HttpClient();
        private static JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true,
        };

        public ProductoServices()
        {
            client= new HttpClient();
            client.BaseAddress = new Uri(Constants.ApiDataServer);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<IEnumerable<Producto>> GetProductosAsync(int paginas)
        {
           var response= await client.GetAsync($"{Constants.ProductsEndpoint}?page={paginas}&pageSize={7}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<Producto>>(options);
            }
            return default;
            
        }

        public async Task<Producto> PostProductosAsync(ProductoDTO producto)
        {
            var content = new MultipartFormDataContent();

            // Agregar el resto de las propiedades del producto
            content.Add(new StringContent(producto.title ?? ""), "title");
            content.Add(new StringContent(producto.price.ToString()), "price");
            content.Add(new StringContent(producto.description ?? ""), "description");
            content.Add(new StringContent(producto.category ?? ""), "category");
            content.Add(new StringContent(producto.stock.ToString()), "stock");

            if (producto.Imagen != null)
            {
                var stream = await producto.Imagen.OpenReadAsync();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg"); // o el tipo adecuado
                content.Add(fileContent, "Imagen", producto.Imagen.FileName);
            }
            var response = await client.PostAsync(Constants.ProductsEndpoint, content);
            if (response.IsSuccessStatusCode)
            {
                // Deserializar la respuesta si es exitosa
                return await response.Content.ReadFromJsonAsync<Producto>(options);
            }
            else
            {
                // Manejar errores de la solicitud
                throw new HttpRequestException($"Error en la solicitud: {response.StatusCode}");
            }
        }

        public async Task<Producto> PutProductosAsync(int ProductoId,ProductoDTO producto)
        {
            var content = new MultipartFormDataContent();

            content.Add(new StringContent(producto.title ?? ""), "title");
            content.Add(new StringContent(producto.price.ToString()), "price");
            content.Add(new StringContent(producto.description ?? ""), "description");
            content.Add(new StringContent(producto.category ?? ""), "category");
            content.Add(new StringContent(producto.stock.ToString()), "stock");

            if (producto.Imagen != null)
            {
                var stream = await producto.Imagen.OpenReadAsync();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg"); // o el tipo adecuado
                content.Add(fileContent, "Imagen", producto.Imagen.FileName);
            }
            else
            {
                content.Add(new StringContent(string.Empty), "Imagen");
            }
            
            var url =$"{Constants.ProductsEndpoint}/{ProductoId}";
            var response = await client.PutAsync(url, content);
            if (response.IsSuccessStatusCode)
            {

                return await response.Content.ReadFromJsonAsync<Producto>(options);
            }
            else
            {
                return null;
            }
            
        }

        public async Task<Producto> SearchByIdAsync(int id)
        {
            var response = await client.GetAsync($"{Constants.ProductsEndpoint}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Producto>(options);
            }
            else
            {
                throw new HttpRequestException($"Error en la solicitud: {response.StatusCode}");

            }
        }

        public async Task<string> BorrarProducto(int id)
        {
            var response = await client.DeleteAsync($"{Constants.ProductsEndpoint}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return "Producto borrado con exito";
            }
            else
            {
                return  $"Error en la solicitud: {response.StatusCode}";

            }
        }

        public async Task<IEnumerable<ProductoCarrito>> GetProductoCarrito(int id)
        {
            var response = await client.GetAsync($"{Constants.PCarritoEndPoint}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<ProductoCarrito>>(options);
            }
            return default;
        }

        public async Task<string> RemoverProductoAsync(int Userid, int productoid)
        {
            var response = await client.DeleteAsync($"{Constants.PCarritoEndPoint}?userId={Userid}&productoId={productoid}");
            if (response.IsSuccessStatusCode)
            {
                return "Producto borrado con exito";
            }
            else
            {
                return $"Error en la solicitud: {response.StatusCode}";

            }
        }

        public async Task<string> ComprarCarroAsync(int userId)
        {
            var response = await client.PutAsync($"{Constants.PCarritoEndPoint}/?userId={userId}", null);
            if(response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return null;
            }

        }

        public async Task<string> AgregarAlCarritoAsync(int userId, int productoid, int cantidad)
        {

            var response = await client.PostAsync($"{Constants.PCarritoEndPoint}?usuarioId={userId}&idproducto={productoid}&cantidad={cantidad}",null);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
