using Trab1_PS.Models;
using Trab1_PS.Models.DTOs;
using System.Threading.Tasks;
using Trab1_PS.dto;

namespace Trab1_PS.Services
{
    public interface IUsuarioService
    {
        Task<(bool, string)> CadastrarUsuario(UsuarioDTO usuarioDto);
        Task<(bool, string)> Login(string email, string senha);
        Task<(bool, string)> AtualizarUsuario(int id, Usuario usuario);
        Task<(bool, string)> DeletarUsuario(int id);
    }
}