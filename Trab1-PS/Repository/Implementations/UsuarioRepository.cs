using Trab1_PS.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Trab1_PS.Models;
using Trab1_PS.Data;


namespace Trab1_PS.Repository;



public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _context.Usuarios.AnyAsync(u => u.Email == email);
    }

    public async Task<Usuario> AddAsync(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }

    public async Task<Usuario> GetByIdAsync(int id)
    {
        return await _context.Usuarios.FindAsync(id);
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync()
    {
        return await _context.Usuarios.ToListAsync();
    }

    public async Task<Usuario> AuthenticateAsync(string email, string senha)
    {
        return await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == email && u.Senha == senha);
    }

    public async Task UpdateAsync(Usuario usuario)
    {
        _context.Entry(usuario).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario != null)
        {
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }
    }
}