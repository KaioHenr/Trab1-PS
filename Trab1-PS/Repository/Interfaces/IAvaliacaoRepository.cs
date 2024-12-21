using Trab1_PS.Models;

namespace Trab1_PS.Repository.Interfaces;

public interface IAvaliacaoRepository
{
    Task<Avaliacao> AddAsync(Avaliacao avaliacao);
    Task<Avaliacao> GetByIdAsync(int id);
    Task UpdateAsync(Avaliacao avaliacao);
    Task DeleteAsync(int id);
}