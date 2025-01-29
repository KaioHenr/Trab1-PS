    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Trab1_PS.dto;
    using Trab1_PS.Models;
    using Trab1_PS.Models.DTOs;
    using Trab1_PS.Repository.Interfaces;

    [Route("api/[controller]")]
    [ApiController]
    public class GeneroController : ControllerBase
    {
        private readonly IGeneroRepository _generoRepository;

        public GeneroController(IGeneroRepository generoRepository)
        {
            _generoRepository = generoRepository;
        }

        [HttpPost("CriarGenero")]
        public async Task<IActionResult> CriarGenero([FromBody] GeneroDTO generoDto)
        {
            if (generoDto == null || string.IsNullOrWhiteSpace(generoDto.Nome))
            {
                return BadRequest("Nome do gênero é obrigatório.");
            }

            // Verifica se o nome já existe
            var generoExistente = await _generoRepository.ObterGeneroPorNomeAsync(generoDto.Nome);
            if (generoExistente != null)
            {
                return Conflict($"O gênero '{generoDto.Nome}' já existe.");
            }

            // Cria o objeto Genero (sem passar o Id, pois será gerado automaticamente no repositório)
            var generoCriado = await _generoRepository.CriarGeneroAsync(generoDto);

            // Retorna a resposta sem o Id no DTO, apenas a mensagem e o nome do gênero
            return CreatedAtAction(nameof(ObterGeneroPorNome), new { nome = generoCriado.Nome }, 
                new { message = "Gênero cadastrado com sucesso.", nome = generoCriado.Nome});
        }

// Adicione este método para permitir a consulta de gênero pelo nome
        [HttpGet("ObterGeneroPorNome")]
        public async Task<IActionResult> ObterGeneroPorNome([FromQuery] string nome)
        {
            var genero = await _generoRepository.ObterGeneroPorNomeAsync(nome);

            if (genero == null)
            {
                return NotFound(new { message = $"Gênero '{nome}' não encontrado." });
            }

            return Ok(new { nome = genero.Nome });
        }


        
        [HttpGet("ObterGeneroPorId/{id}")]
        public async Task<IActionResult> ObterGeneroPorId(int id)
        {
            var genero = await _generoRepository.ObterGeneroPorIdAsync(id);
            if (genero == null)
            {
                return NotFound($"Gênero com id {id} não encontrado.");
            }

            return Ok(genero);
        }
        [HttpPost("ListarGeneros")]
        public async Task<IActionResult> ListarGeneros([FromBody] List<int> generoIds)
        {
            if (generoIds == null || !generoIds.Any())
            {
                return BadRequest("Lista de IDs de gêneros não fornecida ou vazia.");
            }

            // Busca os gêneros com base nos IDs fornecidos
            var generos = await _generoRepository.ListarGenerosAsync(generoIds);

            if (!generos.Any())
            {
                return NotFound("Nenhum gênero encontrado para os IDs fornecidos.");
            }

            return Ok(generos);
        }
    }
     
    