using Trab1_PS.Models;

namespace Trab1_PS.Repository.Interfaces;


public interface IUsuarioRepository
{
    Task<bool> ExistsByEmailAsync(string email);
    Task<Usuario> AddAsync(Usuario usuario);
    Task<Usuario> GetByIdAsync(int id);
    Task<IEnumerable<Usuario>> GetAllAsync();
    Task<Usuario> AuthenticateAsync(string email, string senha);
    Task UpdateAsync(Usuario usuario);
    Task DeleteAsync(int id);
}