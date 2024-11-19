
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
            
            var usuario = new Usuario (usuarioDto.Id,
                usuarioDto.Nome,
                usuarioDto.Email,
                usuarioDto.Senha
            );              
            _dbContext.Usuarios.Add(usuario);
            Console.WriteLine("teste");
            await _dbContext.SaveChangesAsync();

            return Ok("Usuário cadastrado com sucesso!");
        }


        // Autenticar Usuário
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var usuario = await _dbContext.Usuarios
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email && u.Senha == loginDto.Senha);

            if (usuario == null)
                return Unauthorized("Usuário ou senha inválidos.");

            return Ok("Login realizado com sucesso!");
        }

        // Criar Avaliação
        [HttpPost("review")]
        public async Task<IActionResult> CreateReview([FromBody]AvaliacaoDto avaliacaoDto)
        {
            var usuario = await _dbContext.Usuarios.FindAsync(avaliacaoDto.IdUsuario);
            if (usuario == null) return NotFound("Usuário não encontrado.");

            var categoria = await _dbContext.Categorias.FindAsync(avaliacaoDto.IdCategoria);
            if (categoria == null) return NotFound("Categoria não encontrada.");

            var avaliacao = new Avaliacao (avaliacaoDto.Id,avaliacaoDto.IdUsuario, avaliacaoDto.IdCategoria,avaliacaoDto.Nota, avaliacaoDto.Comentario, avaliacaoDto.DataAvaliacao.Year,avaliacaoDto.DataAvaliacao.Month,avaliacaoDto.DataAvaliacao.Day);

            _dbContext.Avaliacoes.Add(avaliacao);
            await _dbContext.SaveChangesAsync();

            return Ok("Avaliação criada com sucesso!");
        }

        // Editar Avaliação
        [HttpPut("review/{id}")]
        public async Task<IActionResult> EditReview(int id, [FromBody] EditAvaliacaoDto editDto)
        {
            var avaliacao = await _dbContext.Avaliacoes.Include(a => a.IdUsuario).FirstOrDefaultAsync(a => a.Id == id);
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
        // Obter todos os usuários
        [HttpGet("usuarios")]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _dbContext.Usuarios
                .Select(u => new 
                {
                    u.Id,
                    u.Nome,
                    u.Email
                    // Excluímos a senha para não expor dados sensíveis
                })
                .ToListAsync();
            return Ok(usuarios);
        }

// Obter um único usuário por ID
        [HttpGet("usuarios/{id}")]
        public async Task<IActionResult> GetUsuarioById(int id)
        {
            var usuario = await _dbContext.Usuarios
                .Where(u => u.Id == id)
                .Select(u => new 
                {
                    u.Id,
                    u.Nome,
                    u.Email
                })
                .FirstOrDefaultAsync();

            if (usuario == null)
                return NotFound($"Usuário com ID {id} não encontrado.");

            return Ok(usuario);
        }

    }
}
