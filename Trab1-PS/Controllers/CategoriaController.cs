using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trab1_PS.Models;

namespace Trab1_PS.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CategoriaController : ControllerBase
{
    private readonly AvaliacaoDb _context;
    private readonly HttpClient _httpClient;

    public CategoriaController(AvaliacaoDb context, HttpClient httpClient)
    {
        _context = context;
        _httpClient = httpClient;
    }
    
    
    [HttpGet("Pesquisar")]
    public async Task<IActionResult> PesquisarMidia([FromQuery] string nomeMidia, [FromQuery] string anoLancamento)        
    
    {
        if (string.IsNullOrWhiteSpace(nomeMidia) || string.IsNullOrWhiteSpace(anoLancamento))
        {
            return BadRequest(new { mensagem = "Por favor, forneça pelo menos um filtro de busca {Filme ou série}" });
        }

        var query = await _context.Categorias.AsQueryable().Where(tipoMidia =>
                tipoMidia.Titulo.Contains(nomeMidia) && tipoMidia.DataLancamento.Year.Equals(anoLancamento))
            .ToListAsync();

        if (!query.Any())
        {
            var response = await _httpClient.GetAsync(apiUrl);

            // Verifica se a resposta foi bem-sucedida
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Erro ao acessar a API externa.");
            }

            // Lê a resposta como string
            var responseData = await response.Content.ReadAsStringAsync();

            // Deserializa a resposta (supondo que seja um JSON)
            var midiasExternas = JsonConvert.DeserializeObject<List<Categoria>>(responseData);

            // Retorna os resultados encontrados na API externa
            return Ok(midiasExternas);
        }
        
        return Ok(query);
    }
}