using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using Trab1_PS.Models;

namespace Trab1_PS.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CategoriaController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly HttpClient _httpClient;

    public CategoriaController(AppDbContext context, HttpClient httpClient)
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

        var query = await _context.Categorias
                                        .AsQueryable()
                                        .Where(tipoMidia => tipoMidia.Titulo.Contains(nomeMidia) && tipoMidia.DataLancamento.Year.Equals(anoLancamento))
                                        .ToListAsync();
        if (!query.Any())
        {


            var options = new RestClientOptions("https://api.themoviedb.org/3/search/movie?key=53ff025f998be338910dbff474bd51df&include_adult=false&language=en-US&page=1");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI1M2ZmMDI1Zjk5OGJlMzM4OTEwZGJmZjQ3NGJkNTFkZiIsIm5iZiI6MTczMTk4MjU2OS4zNTgyMDc3LCJzdWIiOiI2NzM4ZDZhMjExOTkxN2JjMWY1ZTc3YzYiLCJzY29wZXMiOlsiYXBpX3JlYWQiXSwidmVyc2lvbiI6MX0.Vae5cpQDnyO4gQIRxmwtAH3Dt6xOqifnaOrUMEuU6Mo");
            var test = await client.GetAsync(request);

            Console.WriteLine("{0}", test.Content);

            var response = await _httpClient.GetAsync("https://api.themoviedb.org/3/search/movie");
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Erro ao acessar a API externa.");
            }
            var responseData = await response.Content.ReadAsStringAsync();
            
        }
        return Ok(query);
    }
}