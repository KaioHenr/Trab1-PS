using Trab1_PS.Models;
using Trab1_PS.Models.DTOs;
using Trab1_PS.Repository.Interfaces;
using System.Threading.Tasks;
using Trab1_PS.dto;

namespace Trab1_PS.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<(bool, string)> CadastrarUsuario(UsuarioDTO usuarioDto)
        {
            if (await _usuarioRepository.ExistsByEmailAsync(usuarioDto.Email))
                return (false, "E-mail já cadastrado.");

            var usuario = new Usuario
            {
                Nome = usuarioDto.Nome,
                Email = usuarioDto.Email,
                Senha = usuarioDto.Senha
            };

            await _usuarioRepository.AddAsync(usuario);
            return (true, "Usuário cadastrado com sucesso.");
        }

        public async Task<(bool, string)> Login(string email, string senha)
        {
            var usuarioAutenticado = await _usuarioRepository.AuthenticateAsync(email, senha);
            if (usuarioAutenticado == null)
                return (false, "Usuário ou senha inválidos.");

            return (true, "Login realizado com sucesso.");
        }

        public async Task<(bool, string)> AtualizarUsuario(int id, Usuario usuario)
        {
            var existingUsuario = await _usuarioRepository.GetByIdAsync(id);
            if (existingUsuario == null)
                return (false, "Usuário não encontrado.");

            existingUsuario.Nome = usuario.Nome;
            existingUsuario.Email = usuario.Email;
            existingUsuario.Senha = usuario.Senha;

            await _usuarioRepository.UpdateAsync(existingUsuario);
            return (true, "Usuário atualizado com sucesso.");
        }

        public async Task<(bool, string)> DeletarUsuario(int id)
        {
            var existingUsuario = await _usuarioRepository.GetByIdAsync(id);
            if (existingUsuario == null)
                return (false, "Usuário não encontrado.");

            await _usuarioRepository.DeleteAsync(id);
            return (true, "Usuário deletado com sucesso.");
        }
    }
}
