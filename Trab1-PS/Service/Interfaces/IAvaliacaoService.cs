using Trab1_PS.dto;
using Trab1_PS.Models;
using System.Threading.Tasks;

namespace Trab1_PS.Services
{
    public interface IAvaliacaoService
    {
        Task<(bool, string)> CadastrarAvaliacao(AvaliacaoDTO avaliacaoDTO);
        Task<(bool, string)> EditarAvaliacao(int id, AvaliacaoDTO avaliacaoDTO);
        Task<(bool, string)> DeletarAvaliacao(int id);
        Task<Avaliacao> ObterAvaliacaoPorId(int id);
    }
}