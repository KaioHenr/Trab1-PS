using Microsoft.AspNetCore.Mvc;
using Trab1_PS.dto;
using Trab1_PS.Models;
using Trab1_PS.Repository.Interfaces;

namespace Trab1_PS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvaliacaoController : ControllerBase
    {
        private readonly IAvaliacaoRepository _avaliacaoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IDoramaRepository _doramaRepository;

        public AvaliacaoController(IAvaliacaoRepository avaliacaoRepository, IUsuarioRepository usuarioRepository, IDoramaRepository doramaRepository)
        {
            _avaliacaoRepository = avaliacaoRepository;
            _usuarioRepository = usuarioRepository;
            _doramaRepository = doramaRepository;
        }
        
        [HttpPost("CadastrarAvaliacao")]
        public async Task<IActionResult> CadastrarAvaliacao([FromBody] AvaliacaoDTO avaliacaoDTO)
        {
            // Verifica se o Usuário existe
            var usuario = await _usuarioRepository.GetByIdAsync(avaliacaoDTO.UsuarioId);
            if (usuario == null)
                return NotFound(new { Message = $"Usuário com ID {avaliacaoDTO.UsuarioId} não encontrado." });

            // Verifica se o Dorama existe
            var dorama = await _doramaRepository.GetByIdAsync(avaliacaoDTO.DoramaId);
            if (dorama == null)
                return NotFound(new { Message = $"Dorama com ID {avaliacaoDTO.DoramaId} não encontrado." });

            // Cria a entidade Avaliacao com Id gerado automaticamente
            var avaliacao = new Avaliacao
            {
                UsuarioId = avaliacaoDTO.UsuarioId,
                DoramaId = avaliacaoDTO.DoramaId,
                Nota = avaliacaoDTO.Nota,
                Comentario = avaliacaoDTO.Comentario,
                DataAvaliacao = avaliacaoDTO.DataAvaliacao
            };

            await _avaliacaoRepository.AddAsync(avaliacao);

            return Ok(new
            {
                Message = "Avaliação cadastrada com sucesso!",
                AvaliacaoId = avaliacao.Id // Retorna o ID gerado
            });
        }



        [HttpPut("EditarAvaliacao")]
        public async Task<IActionResult> EditarAvaliacao(int id, [FromBody] AvaliacaoDTO avaliacaoDTO)
        {
            var avaliacao = await _avaliacaoRepository.GetByIdAsync(id);
            if (avaliacao == null)
            {
                return NotFound("Avaliação não encontrada.");
            }

            var usuario = await _usuarioRepository.GetByIdAsync(avaliacaoDTO.UsuarioId); // Alterado para UsuarioId
            if (usuario == null)
            {
                return NotFound($"Usuário com ID {avaliacaoDTO.UsuarioId} não encontrado."); // Alterado para UsuarioId
            }

            var dorama = await _doramaRepository.GetByIdAsync(avaliacaoDTO.DoramaId); // Alterado para DoramaId
            if (dorama == null)
            {
                return NotFound($"Dorama com ID {avaliacaoDTO.DoramaId} não encontrado."); // Alterado para DoramaId
            }

            // Atualiza os campos da avaliação
            avaliacao.Nota = avaliacaoDTO.Nota;
            avaliacao.Comentario = avaliacaoDTO.Comentario;
            avaliacao.DataAvaliacao = avaliacaoDTO.DataAvaliacao;

            await _avaliacaoRepository.UpdateAsync(avaliacao);
            return Ok("Avaliação editada com sucesso!");
        }

        [HttpDelete("DeletarAvaliacao")]
        public async Task<IActionResult> DeletarAvaliacao(int id)
        {
            var avaliacao = await _avaliacaoRepository.GetByIdAsync(id);
            if (avaliacao == null)
            {
                return NotFound("Avaliação não encontrada.");
            }

            await _avaliacaoRepository.DeleteAsync(id);
            return Ok("Avaliação deletada com sucesso!");
        }

        [HttpGet("ObterAvaliacaoPorId")]
        public async Task<IActionResult> ObterAvaliacaoPorId(int id)
        {
            var avaliacao = await _avaliacaoRepository.GetByIdAsync(id);
            if (avaliacao == null)
            {
                return NotFound("Avaliação não encontrada.");
            }

            return Ok(avaliacao);
        }
    }
}
