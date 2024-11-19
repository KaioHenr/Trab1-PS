// using Microsoft.AspNetCore.Mvc;
// using Trab1_PS.Models;
//
// namespace Trab1_PS.Controllers;
//
// public class UsuarioController : ControllerBase
// {
//     [ApiController]
//     [Route("usuarios")]
//     public class UsuariosController : ControllerBase
//     {
//         [HttpPost]
//         public IActionResult CreateUsuario([FromBody] Usuario usuario)
//         {
//             // Implementação aqui
//             return Ok();
//         }
//     }
//
//     
// }

using Microsoft.AspNetCore.Mvc;
using Trab1_PS.Models;  // Certifique-se de usar o namespace correto

namespace Trab1_PS.Controllers
{
    [ApiController]
    [Route("usuarios")]
    public class UsuarioController : ControllerBase
    {
        // Para armazenar os usuários em memória
        private static List<Usuario> usuarios = new List<Usuario>();

        // POST para criar um novo usuário
        [HttpPost]
        public IActionResult CreateUsuario([FromBody] Usuario usuario)
        {
            usuarios.Add(usuario);  // Adiciona o usuário à lista
            return Ok(usuario);     // Retorna o usuário que foi adicionado
        }

        // GET para listar todos os usuários
        [HttpGet]
        public IActionResult GetUsuarios()
        {
            // Retorna a lista de usuários
            return Ok(usuarios);  // Aqui você retorna a lista que está armazenada na variável 'usuarios'
        }
    }
}
