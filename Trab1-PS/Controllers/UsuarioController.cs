using Microsoft.AspNetCore.Mvc;
using Trab1_PS.Models;

namespace Trab1_PS.Controllers;

public class UsuarioController : ControllerBase
{
    [ApiController]
    [Route("usuarios")]
    public class UsuariosController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateUsuario([FromBody] Usuario usuario)
        {
            // Implementação aqui
            return Ok();
        }
    }

    
}
