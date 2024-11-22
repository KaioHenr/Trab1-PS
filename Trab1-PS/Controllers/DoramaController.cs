using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestSharp;
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
    
    [HttpGet]
    [Route("Pesquisar/{id}")]
    public async Task<IActionResult> GetAllUsuarios()
    {
        var usuarios = await _context.Doramas.ToListAsync();
        //_dbContext.Usuarios acessa a tabela usuarios no banco
        //ToList consulta pra buscar todos registros
        //await espera a resposta sem parar execução
        return Ok(usuarios);
        //volta a lista de objetos Usuario em json
    }
}