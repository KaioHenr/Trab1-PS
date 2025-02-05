using Microsoft.AspNetCore.Mvc;
using Trab1_PS.Models;
using Trab1_PS.Models.DTOs;
using Trab1_PS.Services;
using System.Threading.Tasks;
using Trab1_PS.dto;

namespace Trab1_PS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UserController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> Cadastrar([FromBody] UsuarioDTO usuarioDto)
        {
            var (success, message) = await _usuarioService.CadastrarUsuario(usuarioDto);

            if (!success)
                return BadRequest(new { Message = message });

            return Ok(new { Message = message });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Usuario usuario)
        {
            var (success, message) = await _usuarioService.Login(usuario.Email, usuario.Senha);

            if (!success)
                return Unauthorized(new { Message = message });

            return Ok(new { Message = message });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] Usuario usuario)
        {
            var (success, message) = await _usuarioService.AtualizarUsuario(id, usuario);

            if (!success)
                return NotFound(new { Message = message });

            return Ok(new { Message = message });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var (success, message) = await _usuarioService.DeletarUsuario(id);

            if (!success)
                return NotFound(new { Message = message });

            return Ok(new { Message = message });
        }
    }
}