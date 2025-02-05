using Trab1_PS.dto;
using Trab1_PS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Trab1_PS.Services
{
    public interface IDoramaService
    {
        Task<(bool, string)> CadastrarDorama(DoramaDTO doramaDTO);
        Task<(bool, string)> EditarDorama(int id, DoramaDTO doramaDTO);
        Task<(bool, string)> DeletarDorama(int id);
        Task<IEnumerable<Dorama>> PesquisarDorama(string titulo);
        Task<Dorama> ObterDoramaPorId(int id);
    }
}