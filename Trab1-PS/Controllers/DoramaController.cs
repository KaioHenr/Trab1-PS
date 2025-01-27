using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trab1_PS.dto;
using Trab1_PS.Models;
using Trab1_PS.Repository.Interfaces;

namespace Trab1_PS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoramaController : ControllerBase
{
    private readonly IDoramaRepository _doramaRepository;
    private readonly IGeneroRepository _generoRepository;

    public DoramaController(IDoramaRepository doramaRepository, IGeneroRepository generoRepository)
    {
        _doramaRepository = doramaRepository;
        _generoRepository = generoRepository;
    }
    
   
    
    [HttpPost("CadastrarDorama")]
    public async Task<IActionResult> CadastrarDorama([FromBody] DoramaDTO doramaDto)
    {
        // Verifica se o título do dorama já existe
        if (await _doramaRepository.ExistsByTituloAsync(doramaDto.Titulo))
            return BadRequest(new { Message = "Dorama já cadastrado!" });

        // Verifica se há gêneros a serem associados
        if (doramaDto.Generos != null && doramaDto.Generos.Any())
        {
            // Busca os gêneros existentes no banco de dados pelo ID
            var generos = await _generoRepository.ListarGenerosAsync(doramaDto.Generos);

            // Verifica se todos os gêneros fornecidos foram encontrados
            if (generos.Count() != doramaDto.Generos.Count)
            {
                return BadRequest(new { Message = "Um ou mais gêneros não foram encontrados!" });
            }

            // Cria o objeto Dorama e associa os gêneros encontrados
            var dorama = new Dorama
            {
                Id = doramaDto.Id, // Mantém o ID fornecido
                Titulo = doramaDto.Titulo,
                Descricao = doramaDto.Descricao,
                QtdEpisodios = doramaDto.QtdEpisodios,
                DataLancamento = doramaDto.DataLancamento,
                GenerosIds = doramaDto.Generos // Usando GenerosIds como esperado
            };

            // Adiciona o dorama ao repositório
            await _doramaRepository.AddAsync(dorama);

            // Exibe o ID do dorama no console
            Console.WriteLine($"Dorama cadastrado com o ID: {dorama.Id}");

            return Ok(new { Message = "Dorama registrado com sucesso!" });
        }

        // Caso não haja gêneros para associar
        return BadRequest(new { Message = "Gêneros não informados ou inválidos!" });
    }


    
    
    [HttpGet("PesquisarDorama")]
    public async Task<IActionResult> PesquisarDorama([FromQuery] string titulo)
    {
        var doramas = await _doramaRepository.SearchByTituloAsync(titulo);

        if (!doramas.Any())
            return NotFound(new { Message = "Nenhum dorama encontrado!" });

        var doramaDtos = doramas.Select(d => new DoramaDTO
        {
            Id = d.Id,
            Titulo = d.Titulo,
            Descricao = d.Descricao,
            QtdEpisodios = d.QtdEpisodios,
            DataLancamento = d.DataLancamento,
            Generos = d.GenerosIds, // Apenas os IDs
            Avaliacoes = d.Avaliacoes.Select(a => new AvaliacaoDTO
            {
                Id = a.Id,
                UsuarioId = a.UsuarioId,
                DoramaId = a.DoramaId,
                Nota = a.Nota,
                Comentario = a.Comentario,
                DataAvaliacao = a.DataAvaliacao
            }).ToList()
        });

        return Ok(doramaDtos);
    }

    [HttpPut("EditarDorama/{id}")]
    public async Task<IActionResult> EditarDorama(int id, [FromBody] DoramaDTO doramaDto)
    {
        var existingDorama = await _doramaRepository.GetByIdAsync(id);
        if (existingDorama == null)
            return NotFound(new { Message = "Dorama não encontrado" });

        // Atualiza o título e a descrição apenas se o usuário fornecer valores novos
        if (!string.IsNullOrEmpty(doramaDto.Titulo))
            existingDorama.Titulo = doramaDto.Titulo;

        if (!string.IsNullOrEmpty(doramaDto.Descricao))
            existingDorama.Descricao = doramaDto.Descricao;

        // Atualiza os gêneros com os novos IDs fornecidos, se existirem
        if (doramaDto.Generos != null && doramaDto.Generos.Any())
        {
            // Verifica se os gêneros fornecidos são válidos (existe no banco)
            var generos = await _generoRepository.ListarGenerosAsync(doramaDto.Generos);

            if (generos.Count() != doramaDto.Generos.Count)
            {
                return BadRequest(new { Message = "Um ou mais gêneros não foram encontrados!" });
            }

            // Associa os IDs dos gêneros ao dorama
            existingDorama.GenerosIds = doramaDto.Generos; // Altera somente os IDs dos gêneros
        }

        // Atualiza o dorama no banco de dados
        await _doramaRepository.UpdateAsync(existingDorama);
        return Ok(new { Message = "Dorama editado com sucesso!" });
    }

    [HttpDelete("DeletarDorama/{id}")]
    public async Task<IActionResult> DeletarDorama(int id)
    {
        var existingDorama = await _doramaRepository.GetByIdAsync(id);
        if (existingDorama == null)
            return NotFound("Dorama não encontrado");

        await _doramaRepository.DeleteAsync(id);
        return Ok("Dorama excluído com sucesso!");
    }
}