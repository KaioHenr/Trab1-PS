using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trab1_PS.dto;
using Trab1_PS.Models;
using Trab1_PS.Repository.Interfaces;

namespace Trab1_PS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UserController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> Cadastrar([FromBody] Usuario usuario)
        {
            if (await _usuarioRepository.ExistsByEmailAsync(usuario.Email))
                return BadRequest("E-mail já cadastrado.");

            await _usuarioRepository.AddAsync(usuario);
            return Ok("Usuário cadastrado com sucesso.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Usuario usuario)
        {
            var authenticatedUser = await _usuarioRepository.AuthenticateAsync(usuario.Email, usuario.Senha);
            if (authenticatedUser == null)
                return Unauthorized("Usuário ou senha inválidos.");

            return Ok("Login realizado com sucesso!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] Usuario usuario)
        {
            var existingUsuario = await _usuarioRepository.GetByIdAsync(id);
            if (existingUsuario == null)
                return NotFound("Usuário não encontrado.");

            existingUsuario.Nome = usuario.Nome;
            existingUsuario.Email = usuario.Email;
            existingUsuario.Senha = usuario.Senha;

            await _usuarioRepository.UpdateAsync(existingUsuario);
            return Ok("Usuário atualizado com sucesso!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var existingUsuario = await _usuarioRepository.GetByIdAsync(id);
            if (existingUsuario == null)
                return NotFound("Usuário não encontrado.");

            await _usuarioRepository.DeleteAsync(id);
            return Ok("Usuário deletado com sucesso!");
        }
    }
}
