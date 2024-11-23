using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
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
        [HttpPost("cadastrar")]
        // metódo assicrono retorna task de mensagem(interface generica)
        public async Task<IActionResult> Cadastrar([FromBody] Usuario usuarioDto)
        {
            // Verifica na tabela usuarios do banco se o e-mail já está registrado
            if (await _dbContextContext.Usuarios.AnyAsync(u => u.Email == usuarioDto.Email))
                return BadRequest("E-mail já cadastrado.");

            // Cria um novo objeto de usuário
            var usuario = new Usuario
            {
                Nome = usuarioDto.Nome,
                Email = usuarioDto.Email,
                Senha = usuarioDto.Senha
            };

            //Acessa a tabela usuarios no banco e adiciona esse usuario criado
            _dbContextContext.Usuarios.Add(usuario);
            await _dbContextContext.SaveChangesAsync();

            return Ok("Usuário cadastrado com sucesso.");
        }

        // Autenticar Usuário
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioDTO loginDto)
        {
            //Acessa tabela Usuarios no banco e verifica se esse usuario exiSte no banco com esse email e senha
            var usuario = await _dbContextContext.Usuarios
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email && u.Senha == loginDto.Senha);

            if (usuario == null)
                return Unauthorized("Usuário ou senha inválidos.");

            // // Para salvar o "estar logado" na sessão
            // HttpContext.Session.SetString("UsuarioLogado", usuario.Email);

            return Ok("Login realizado com sucesso!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] Usuario updateDto)
        {
            //Procura o usuario pelo id e verifica se existe
            var usuario = await _dbContextContext.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound("Usuário não encontrado.");

            // Pega as informações novas e atualiza no objeto do usuario
            usuario.Nome = updateDto.Nome;
            usuario.Email = updateDto.Email;
            usuario.Senha = updateDto.Senha;

            //Atualiza na tabela do usuario no banco
            _dbContextContext.Usuarios.Update(usuario);
            await _dbContextContext.SaveChangesAsync();
            //Salva alterações

            return Ok("Usuário atualizado com sucesso!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarUsuarios(int id, [FromBody] Usuario updateDto)
        {
            var usuario = await _dbContextContext.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound("Usuário não encontrado.");

            _dbContextContext.Usuarios.Remove(usuario);
            await _dbContextContext.SaveChangesAsync();

            return Ok("Usuário deletado com sucesso!");

        }

        [HttpGet]
        [Route("usuarios")]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _dbContextContext.Usuarios.Include(u => u.Avaliacoes).ToListAsync();
            //_dbContext.Usuarios acessa a tabela usuarios no banco
            //ToList consulta pra buscar todos registros
            //await espera a resposta sem parar execução
            return Ok(usuarios);
            //volta a lista de objetos Usuario em json
        }

        
        
        [HttpPost]
        [Route("avaliar")]
        public async Task<IActionResult> Avaliar([FromBody] Avaliacao avaliacao)
        {
            // Buscar o usuário no banco de dados
            var usuario = await _dbContextContext.Usuarios
                .Include(u => u.Avaliacoes)
                .FirstOrDefaultAsync(u => u.Email == avaliacao.EmailUsuario);

            // Verificar se o usuário existe
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            // Buscar o dorama no banco de dados
            var dorama = await _dbContextContext.Doramas
                .Include(d => d.Avaliacoes)
                .FirstOrDefaultAsync(d => d.Titulo == avaliacao.NomeDorama);

            // Verificar se o dorama existe
            if (dorama == null)
            {
                return NotFound("Dorama não encontrado.");
            }
            
            avaliacao.DataAvaliacao = DateTime.Now;
            usuario.Avaliacoes.Add(avaliacao);
            dorama.Avaliacoes.Add(avaliacao);

            
            await _dbContextContext.SaveChangesAsync();
            return Ok(new { Message = "Avaliação registrada com sucesso!" });
        }
        

        [HttpPut("editar/{id}")]
        public async Task<IActionResult> EditarAvaliacao(int id, [FromBody] Avaliacao editDto)
        {
            
            var avaliacao = await _dbContextContext.Avaliacoes
                .FirstOrDefaultAsync(a => a.Id == id);

            if (avaliacao == null)
            {
                return NotFound(new { Message = $"Avaliação com ID {id} não encontrada." });
            }
            
            avaliacao.Nota = editDto.Nota;
            avaliacao.Comentario = editDto.Comentario;
            
            _dbContextContext.Avaliacoes.Update(avaliacao);
            await _dbContextContext.SaveChangesAsync();

            return Ok(new { Message = "Avaliação editada com sucesso!" });
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


    }
}
