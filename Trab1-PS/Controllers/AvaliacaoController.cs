// AvaliacaoController.cs
using Microsoft.AspNetCore.Mvc;
using Trab1_PS.dto;
using Trab1_PS.Models;
using Trab1_PS.Services;

namespace Trab1_PS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvaliacaoController : ControllerBase
    {
        private readonly IAvaliacaoService _avaliacaoService;

        public AvaliacaoController(IAvaliacaoService avaliacaoService)
        {
            _avaliacaoService = avaliacaoService;
        }

        [HttpPost("CadastrarAvaliacao")]
        public async Task<(bool, string)> CadastrarAvaliacao([FromBody] AvaliacaoDTO avaliacaoDTO)
        {
            return await _avaliacaoService.CadastrarAvaliacao(avaliacaoDTO);
        }

        [HttpPut("EditarAvaliacao")]
        public async Task<(bool, string)> EditarAvaliacao(int id, [FromBody] AvaliacaoDTO avaliacaoDTO)
        {
            return await _avaliacaoService.EditarAvaliacao(id, avaliacaoDTO);
        }

        [HttpDelete("DeletarAvaliacao")]
        public async Task<(bool, string)> DeletarAvaliacao(int id)
        {
            return await _avaliacaoService.DeletarAvaliacao(id);
        }

        [HttpGet("ObterAvaliacaoPorId")]
        public async Task<Avaliacao> ObterAvaliacaoPorId(int id)
        {
            return await _avaliacaoService.ObterAvaliacaoPorId(id);
        }
    }
}