using Trab1_PS.Models;

namespace Trab1_PS.Repository.Interfaces;

public interface IDoramaRepository
{
    Task<bool> ExistsByTituloAsync(string titulo);
    Task<Dorama> AddAsync(Dorama dorama);
    Task<Dorama> GetByIdAsync(int id);
    Task<IEnumerable<Dorama>> GetAllAsync();
    Task<IEnumerable<Dorama>> SearchByTituloAsync(string titulo);
    Task UpdateAsync(Dorama dorama);
    Task DeleteAsync(int id);
}