using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tp6Maui.Models;
using Tp6Maui.ModelsDTO;
using Tp6Maui.Utils;

namespace Tp6Maui.Services
{
    internal class UsuarioService : IUsuarioService
    {
        HttpClient client = new HttpClient();
        private static JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true,
        };

        public UsuarioService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(Constants.ApiDataServer);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<Usuario> ValidarLogin(string Email, string Password)
        {
            var content = new StringContent(JsonSerializer.Serialize(new
            {
                email = Email,
                password = GetSHA256(Password)
            }), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(Constants.LoginEndpoint, content);
            
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Usuario>(options);
            }
            else
            {
                throw new HttpRequestException($"Error en la solicitud: {response.StatusCode}");
            }
        }

        public async Task<Usuario> RealizarRegistro(UserRegisterDTO usuario)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(
                    new
                    {
                        email=usuario.email,
                        password=GetSHA256(usuario.password),
                        name=usuario.name,
                        phone=usuario.phone,
                        username=usuario.username
                    }
                    ), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(Constants.RegistrarseEndpint, content);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Usuario>(options);
                
            }
            else
            {
                return default;
            }
        }

        public static string GetSHA256(string text)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
        

        public async Task<Usuario> ModificarUsuario(UserRegisterDTO usuario)
        {
            var content = new StringContent(
                           JsonSerializer.Serialize(
                               new
                               {
                                   email = usuario.email,
                                   password = GetSHA256(usuario.password),
                                   name = usuario.name,
                                   phone = usuario.phone,
                                   username = usuario.username
                               }
                               ), Encoding.UTF8, "application/json");
            var UrlFinal = $"{Constants.UsuarioEndpoint}/{Transports.IdUsuario}";
            var response = await client.PutAsync(UrlFinal, content);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Usuario>(options);

            }
            else
            {
                throw new HttpRequestException($"Error en la solicitud: {response.StatusCode}");
            }
        }

        public async Task<Usuario> UserPorId(int Id)
        {
            var response = await client.GetAsync($"{Constants.UsuarioEndpoint}/{Id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Usuario>(options);
            }
            else
            {
                throw new HttpRequestException($"Error en la solicitud: {response.StatusCode}");

            }
        }

        public async Task<IEnumerable<Usuario>> GetUsuariosAsync()
        {
            var response = await client.GetAsync(Constants.UsuarioEndpoint);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<Usuario>>(options);
            }
            return default;
        }

        public async Task<string> BorrarUsuario(int id)
        {
            var response = await client.DeleteAsync($"{Constants.UsuarioEndpoint}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return "Usuario borrado con exito";
            }
            else
            {
                return $"Error en la solicitud: {response.StatusCode}";

            }
        }
    }
}
