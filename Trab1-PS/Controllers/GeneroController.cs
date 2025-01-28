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
                return BadRequest("Gênero cadastrado com sucesso.");
            }

            // Verifica se o ID já existe
            var generoExistentePorId = await _generoRepository.ObterGeneroPorIdAsync(generoDto.Id);
            if (generoExistentePorId != null)
            {
                return Conflict($"Já existe um gênero com o ID {generoDto.Id}.");
            }

            // Verifica se o nome já existe
            var generoExistentePorNome = await _generoRepository.ObterGeneroPorNomeAsync(generoDto.Nome);
            if (generoExistentePorNome != null)
            {
                return Conflict($"O gênero '{generoDto.Nome}' já existe.");
            }

            // Cria o gênero no banco de dados
            var generoCriado = await _generoRepository.CriarGeneroAsync(generoDto);
            return CreatedAtAction(nameof(ObterGeneroPorId), new { id = generoCriado.Id }, new { message = "Gênero cadastrado com sucesso.", genero = generoCriado });
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
     
    