using Trab1_PS.Models;
using Trab1_PS.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Trab1_PS.Services
{
    public interface IGeneroService
    {
        Task<(bool, string)> CriarGenero(GeneroDTO generoDto);
        Task<GeneroDTO> ObterGeneroPorNome(string nome);
        Task<GeneroDTO> ObterGeneroPorId(int id);
        Task<IEnumerable<GeneroDTO>> ListarGeneros(List<int> generoIds);
    }
}