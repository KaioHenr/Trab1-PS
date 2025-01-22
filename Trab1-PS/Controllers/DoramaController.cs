﻿using Microsoft.AspNetCore.Mvc;
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

    public DoramaController(IDoramaRepository doramaRepository)
    {
        _doramaRepository = doramaRepository;
    }

    [HttpPost("CadastrarDorama")]
    public async Task<IActionResult> CadastrarDorama([FromBody] Dorama dorama)
    {
        if (await _doramaRepository.ExistsByTituloAsync(dorama.Titulo))
            return BadRequest(new { Message = "Dorama já cadastrado!" });

        await _doramaRepository.AddAsync(dorama);
        return Ok(new { Message = "Dorama registrado com sucesso!" });
    }

    [HttpGet("PesquisarDorama")]
    public async Task<IActionResult> PesquisarDorama([FromQuery] string titulo)
    {
        var doramas = await _doramaRepository.SearchByTituloAsync(titulo);

        if (!doramas.Any())
            return NotFound(new { Message = "Nenhum dorama encontrado!" });
        var doramaDtos = doramas.Select(d => new DoramaDTO(
            d.Id,
            d.Titulo,
            d.Descricao,
            d.QtdEpisodios,
            d.DataLancamento,
            d.Generos.Select(g => g.Nome).ToList(),
            d.Avaliacoes.Select(a => new AvaliacaoDTO(
                a.Id,
                a.UsuarioId,
                a.DoramaId,
                a.Nota,
                a.Comentario,
                a.DataAvaliacao
            )).ToList()
        ));

        return Ok(doramaDtos);
    }

  

    [HttpPut("EditarDorama/{id}")]
    public async Task<IActionResult> EditarDorama(int id, [FromBody] Dorama dorama)
    {
        var existingDorama = await _doramaRepository.GetByIdAsync(id);
        if (existingDorama == null)
            return NotFound("Dorama não encontrado");

        existingDorama.Titulo = dorama.Titulo;
        existingDorama.Descricao = dorama.Descricao;

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