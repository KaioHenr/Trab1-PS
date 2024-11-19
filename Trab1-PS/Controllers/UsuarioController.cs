
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trab1_PS.dto;
using Trab1_PS.Models;

namespace Trab1_PS.Controllers
{

    
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AvaliacaoDb _dbContext;

        public UserController(AvaliacaoDb dbContext)
        {
            _dbContext = dbContext;
        }

        // Cadastrar Usuário
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromQuery] Usuario usuarioDto)
        {
            if (await _dbContext.Usuarios.AnyAsync(u => u.Email == usuarioDto.Email))
                return BadRequest("E-mail já cadastrado."); 
            
            var usuario = new Usuario
            {
                Id = usuarioDto,
                Nome = usuarioDto.Nome,
                Email = usuarioDto.Email,
                Senha = usuarioDto.Senha
            };              
            _dbContext.Usuarios.Add(usuario);
            await _dbContext.SaveChangesAsync();

            return Ok("Usuário cadastrado com sucesso!");
        }


        // Autenticar Usuário
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromQuery] LoginDto loginDto)
        {
            var usuario = await _dbContext.Usuarios
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email && u.Senha == loginDto.Senha);

            if (usuario == null)
                return Unauthorized("Usuário ou senha inválidos.");

            return Ok("Login realizado com sucesso!");
        }

        // Criar Avaliação
        [HttpPost("review")]
        public async Task<IActionResult> CreateReview([FromQuery] AvaliacaoDto avaliacaoDto)
        {
            var usuario = await _dbContext.Usuarios.FindAsync(avaliacaoDto.Usuario);
            if (usuario == null) return NotFound("Usuário não encontrado.");

            var categoria = await _dbContext.Categorias.FindAsync(avaliacaoDto.Categoria);
            if (categoria == null) return NotFound("Categoria não encontrada.");

            var avaliacao = new Avaliacao (Nota = avaliacaoDto.Nota, Comentario = avaliacaoDto.Comentario);

            _dbContext.Avaliacoes.Add(avaliacao);
            await _dbContext.SaveChangesAsync();

            return Ok("Avaliação criada com sucesso!");
        }

        // Editar Avaliação
        [HttpPut("review/{id}")]
        public async Task<IActionResult> EditReview(int id, [FromQuery] EditAvaliacaoDto editDto)
        {
            var avaliacao = await _dbContext.Avaliacoes.Include(a => a.Usuario).FirstOrDefaultAsync(a => a.Id == id);
            if (avaliacao == null) return NotFound("Avaliação não encontrada.");

            avaliacao.Nota = editDto.Nota;
            avaliacao.Comentario = editDto.Comentario;

            _dbContext.Avaliacoes.Update(avaliacao);
            await _dbContext.SaveChangesAsync();

            return Ok("Avaliação editada com sucesso!");
        }

        // Excluir Avaliação
        [HttpDelete("review/{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var avaliacao = await _dbContext.Avaliacoes.FindAsync(id);
            if (avaliacao == null) return NotFound("Avaliação não encontrada.");

            _dbContext.Avaliacoes.Remove(avaliacao);
            await _dbContext.SaveChangesAsync();

            return Ok("Avaliação excluída com sucesso!");
        }
        
    }
}
