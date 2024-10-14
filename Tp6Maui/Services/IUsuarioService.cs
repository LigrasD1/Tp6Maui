using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp6Maui.Models;
using Tp6Maui.ModelsDTO;

namespace Tp6Maui.Services
{
    public interface IUsuarioService
    {
        public Task<Usuario> RealizarRegistro(UserRegisterDTO usuario);
        public Task<Usuario> ValidarLogin(string Email, string password);
        public Task<Usuario> ModificarUsuario(UserRegisterDTO usuario);
        public Task<Usuario> UserPorId(int Id);
        public Task<IEnumerable<Usuario>> GetUsuariosAsync();
        public Task<string> BorrarUsuario(int id);

    }
}
