// DoramaController.cs
using Microsoft.AspNetCore.Mvc;
using Trab1_PS.dto;
using Trab1_PS.Models;
using Trab1_PS.Services;

namespace Trab1_PS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoramaController : ControllerBase
    {
        private readonly IDoramaService _doramaService;

        public DoramaController(IDoramaService doramaService)
        {
            _doramaService = doramaService;
        }

        [HttpPost("CadastrarDorama")]
        public async Task<(bool, string)> CadastrarDorama([FromBody] DoramaDTO doramaDto)
        {
            return await _doramaService.CadastrarDorama(doramaDto);
        }

        [HttpGet("PesquisarDorama")]
        public async Task<IEnumerable<Dorama>> PesquisarDorama(string titulo)
        {
            return await _doramaService.PesquisarDorama(titulo);
        }

        [HttpPut("EditarDorama")]
        public async Task<(bool, string)> EditarDorama(int id, [FromBody] DoramaDTO doramaDto)
        {
            return await _doramaService.EditarDorama(id, doramaDto);
        }

        [HttpDelete("DeletarDorama")]
        public async Task<(bool, string)> DeletarDorama(int id)
        {
            return await _doramaService.DeletarDorama(id);
        }
    }
}