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
        // Verifica se o título já existe
        if (await _doramaRepository.ExistsByTituloAsync(doramaDto.Titulo))
            return BadRequest(new { Message = "Dorama já cadastrado!" });

        // Verifica se há gêneros informados
        if (doramaDto.Generos == null || !doramaDto.Generos.Any())
        {
            return BadRequest(new { Message = "Gêneros não informados ou inválidos!" });
        }

        // Busca os gêneros existentes no banco pelo ID
        var generos = await _generoRepository.ListarGenerosAsync(doramaDto.Generos);

        // Verifica se todos os gêneros fornecidos foram encontrados
        if (generos.Count() != doramaDto.Generos.Count)
        {
            return BadRequest(new { Message = "Um ou mais gêneros não foram encontrados!" });
        }

        // Cria o objeto Dorama
        var dorama = new Dorama
        {
            Titulo = doramaDto.Titulo,
            Descricao = doramaDto.Descricao,
            QtdEpisodios = doramaDto.QtdEpisodios,
            DataLancamento = doramaDto.DataLancamento,
            GenerosIds = doramaDto.Generos
        };

        // Adiciona ao banco de dados
        await _doramaRepository.AddAsync(dorama);

        // O EF agora preenche automaticamente o Id do dorama
        Console.WriteLine($"Dorama cadastrado com o ID: {dorama.Id}");

        return Ok(new {
            Message = "Dorama registrado com sucesso!",
            DoramaId = dorama.Id // Retorna o ID gerado automaticamente
        });
    }


    
    
    [HttpGet("PesquisarDorama")]
    public async Task<IActionResult> PesquisarDorama([FromQuery] string titulo)
    {
        // Busca os doramas pelo título fornecido
        var doramas = await _doramaRepository.SearchByTituloAsync(titulo);

        // Verifica se não foram encontrados doramas
        if (!doramas.Any())
            return NotFound(new { Message = "Nenhum dorama encontrado!" });

        // Mapeia os resultados para o DTO sem incluir o ID da Avaliação
        var doramaDtos = doramas.Select(d => new
        {
            Id = d.Id,
            Titulo = d.Titulo,
            Descricao = d.Descricao,
            QtdEpisodios = d.QtdEpisodios,
            DataLancamento = d.DataLancamento,
            Generos = d.GenerosIds, // Lista de IDs dos gêneros
            Avaliacoes = d.Avaliacoes.Select(a => new
            {
                UsuarioId = a.UsuarioId,  
                DoramaId = a.DoramaId,
                Nota = a.Nota,
                Comentario = a.Comentario,
                DataAvaliacao = a.DataAvaliacao
            }).ToList()
        }).ToList();

        // Retorna a lista de doramas no formato DTO
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