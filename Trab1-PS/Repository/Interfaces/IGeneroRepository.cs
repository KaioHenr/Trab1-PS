using Trab1_PS.Models;
using Trab1_PS.Models.DTOs;

namespace Trab1_PS.Repository.Interfaces;

public interface IGeneroRepository
{
    Task<GeneroDTO> CriarGeneroAsync(GeneroDTO generoDto);
    Task<GeneroDTO> ObterGeneroPorIdAsync(int id);
    Task<GeneroDTO> ObterGeneroPorNomeAsync(string nome);
    Task<IEnumerable<GeneroDTO>> ListarGenerosAsync(List<int> id);
}
