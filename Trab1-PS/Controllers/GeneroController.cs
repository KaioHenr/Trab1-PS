using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trab1_PS.Models.DTOs;
using Trab1_PS.Services;

[Route("api/[controller]")]
[ApiController]
public class GeneroController : ControllerBase
{
    private readonly IGeneroService _generoService;

    public GeneroController(IGeneroService generoService)
    {
        _generoService = generoService;
    }

    [HttpPost("CriarGenero")]
    public async Task<IActionResult> CriarGenero([FromBody] GeneroDTO generoDto)
    {
        var (sucesso, mensagem) = await _generoService.CriarGenero(generoDto);

        if (!sucesso)
        {
            return BadRequest(new { message = mensagem });
        }

        return CreatedAtAction(nameof(ObterGeneroPorNome), new { nome = generoDto.Nome }, 
            new { message = mensagem, nome = generoDto.Nome });
    }

    [HttpGet("ObterGeneroPorNome")]
    public async Task<IActionResult> ObterGeneroPorNome([FromQuery] string nome)
    {
        try
        {
            var genero = await _generoService.ObterGeneroPorNome(nome);
            return Ok(genero);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpGet("ObterGeneroPorId/{id}")]
    public async Task<IActionResult> ObterGeneroPorId(int id)
    {
        try
        {
            var genero = await _generoService.ObterGeneroPorId(id);
            return Ok(genero);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPost("ListarGeneros")]
    public async Task<IActionResult> ListarGeneros([FromBody] List<int> generoIds)
    {
        try
        {
            var generos = await _generoService.ListarGeneros(generoIds);
            return Ok(generos);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
