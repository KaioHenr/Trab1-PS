using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trab1_PS.dto;
using Trab1_PS.Models;

namespace Trab1_PS.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _dbContextContext;
        public UserController(AppDbContext dbContextContext)
        {
            _dbContextContext = dbContextContext;
        }
        // Cadastrar Usuário
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] Usuario usuarioDto)
        {
            // Verificar se o e-mail já está cadastrado
            if (await _dbContextContext.Usuarios.AnyAsync(u => u.Email == usuarioDto.Email)) 
                return BadRequest(new { Message = "E-mail já cadastrado." });

            // Cria novo objeto com dados da requisição
            var usuario = new Usuario(
                usuarioDto.Id,
                usuarioDto.Nome,
                usuarioDto.Email,
                usuarioDto.Senha
            );

            // Adicionar o novo usuário criado ao banco
            _dbContextContext.Usuarios.Add(usuario);
            //_dbContext.Usuarios acessa a tabela de usuarios do banco e Add new usuario 
            await _dbContextContext.SaveChangesAsync();
            // Salva tudo no banco
            
            return Ok(new { Message = "Usuário registrado com sucesso!" });
        }
        // Autenticar Usuário
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioDTO loginDto)
        {
            var usuario = await _dbContextContext.Usuarios
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email && u.Senha == loginDto.Senha);
            if (usuario == null)
                return Unauthorized("Usuário ou senha inválidos.");
            return Ok("Login realizado com sucesso!");
        }
        // Criar Avaliação
        [HttpPost("review")]
        public async Task<IActionResult> CreateReview([FromBody]AvaliacaoDTO avaliacaoDto)
        {
            var usuario = await _dbContextContext.Usuarios.FindAsync(avaliacaoDto.IdUsuario);
            if (usuario == null) return NotFound("Usuário não encontrado.");

            var categoria = await _dbContextContext.Doramas.FindAsync(avaliacaoDto.IdDorama);
            if (categoria == null) return NotFound("Categoria não encontrada.");

            var avaliacao = new Avaliacao (avaliacaoDto.Id,avaliacaoDto.IdUsuario, avaliacaoDto.IdDorama,avaliacaoDto.Nota, avaliacaoDto.Comentario, avaliacaoDto.DataAvaliacao.Year,avaliacaoDto.DataAvaliacao.Month,avaliacaoDto.DataAvaliacao.Day);

            _dbContextContext.Avaliacoes.Add(avaliacao);
            await _dbContextContext.SaveChangesAsync();

            return Ok("Avaliação criada com sucesso!");
        }

        // Editar Avaliação
        [HttpPut("review/{id}")]
        public async Task<IActionResult> EditReview(int id, [FromBody] AvaliacaoDTO editDto)
        {
            var avaliacao = await _dbContextContext.Avaliacoes.Include(a => a.IdUsuario).FirstOrDefaultAsync(a => a.Id == id);
            if (avaliacao == null) return NotFound("Avaliação não encontrada.");

            avaliacao.Nota = editDto.Nota;
            avaliacao.Comentario = editDto.Comentario;

            _dbContextContext.Avaliacoes.Update(avaliacao);
            await _dbContextContext.SaveChangesAsync();

            return Ok("Avaliação editada com sucesso!");
        }

        // Excluir Avaliação
        [HttpDelete("review/{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var avaliacao = await _dbContextContext.Avaliacoes.FindAsync(id);
            if (avaliacao == null) return NotFound("Avaliação não encontrada.");

            _dbContextContext.Avaliacoes.Remove(avaliacao);
            await _dbContextContext.SaveChangesAsync();

            return Ok("Avaliação excluída com sucesso!");
        }
        [HttpGet]
        [Route("usuarios")]
        public async Task<IActionResult> GetAllUsuarios()
        {
            var usuarios = await _dbContextContext.Usuarios.ToListAsync();
            //_dbContext.Usuarios acessa a tabela usuarios no banco
            //ToList consulta pra buscar todos registros
            //await espera a resposta sem parar execução
            return Ok(usuarios);
            //volta a lista de objetos Usuario em json
        }
    }
}
