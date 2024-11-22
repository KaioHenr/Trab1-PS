using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trab1_PS.Models;

namespace Trab1_PS.Controllers;
[ApiController]
[Route("api/[controller]")]
public class DoramaController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly HttpClient _httpClient;

    public DoramaController(AppDbContext context, HttpClient httpClient)
    {
        _context = context;
        _httpClient = httpClient;
    }
    [HttpPost]
    [Route("CadastrarDorama")]
    public async Task<IActionResult> CadastrarDorama([FromBody] Dorama doramasForm)
    {
        if (await _context.Doramas.AnyAsync(D => D.Titulo == doramasForm.Titulo)) 
            return BadRequest(new { Message = "Dorama já cadastrado!" });
        var doramaObj = new Dorama( doramasForm.Id, doramasForm.Titulo, doramasForm.Descricao, doramasForm.DataLancamento.Year,  doramasForm.DataLancamento.Month,  doramasForm.DataLancamento.Day, doramasForm.Episodios);
        _context.Doramas.Add(doramaObj);
        await _context.SaveChangesAsync();
        return Ok(new { Message = "Dorama registrado com sucesso!" });
    }
    [HttpGet]
    [Route("PesquisarDorama/{id}")]
    public async Task<IActionResult> PesquisarDorama(int id)
    {
        var dorama = await _context.Doramas
            .FirstOrDefaultAsync(dorama => dorama.Id == id);
        if (dorama == null)
        {
            return NotFound($"Dorama com ID {id} não foi encontrado.");
        }
        return Ok(dorama);
    }
    [HttpPut]
    [Route("EditarDorama/{id}")]
    public async Task<IActionResult> EditarDorama(int id, [FromBody] Dorama editDorama)
    {
        var dorama = await _context.Doramas.FindAsync(id);
        if (dorama == null)
        {
            return NotFound("Dorama não encontrado");

        }
        
        dorama.Titulo = editDorama.Titulo;
        dorama.Descricao = editDorama.Descricao;
        
        _context.Doramas.Update(dorama);
        await _context.SaveChangesAsync();
        return Ok(new { Message = "Dorama editado com sucesso!" });
        
    }
    
    [HttpDelete]
    [Route("DeletarDorama/{id}")]
    public async Task<IActionResult> DeletarDorama(int id, [FromBody] Dorama DeleteDorama)
    {
        var dorama = await _context.Doramas.FindAsync(id);

        if (dorama == null)
        {
            return NotFound("Dorama não encontrado");

        }
        _context.Doramas.Remove(dorama);
        await _context.SaveChangesAsync();
        return Ok("Dorama excluído com sucesso!");
        
    }
}
