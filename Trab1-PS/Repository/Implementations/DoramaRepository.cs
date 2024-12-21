using Trab1_PS.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Trab1_PS.Models;
using Trab1_PS.Data;


namespace Trab1_PS.Repository;


public class DoramaRepository : IDoramaRepository
{
    private readonly AppDbContext _context;

    public DoramaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsByTituloAsync(string titulo)
    {
        return await _context.Doramas.AnyAsync(d => d.Titulo == titulo);
    }

    public async Task<Dorama> AddAsync(Dorama dorama)
    {
        _context.Doramas.Add(dorama);
        await _context.SaveChangesAsync();
        return dorama;
    }

    public async Task<Dorama> GetByIdAsync(int id)
    {
        return await _context.Doramas.FindAsync(id);
    }

    public async Task<IEnumerable<Dorama>> GetAllAsync()
    {
        return await _context.Doramas.ToListAsync();
    }

    public async Task<IEnumerable<Dorama>> SearchByTituloAsync(string titulo)
    {
        return await _context.Doramas
            .Where(d => EF.Functions.Like(d.Titulo, $"%{titulo}%"))
            .ToListAsync();
    }

    public async Task UpdateAsync(Dorama dorama)
    {
        _context.Entry(dorama).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var dorama = await _context.Doramas.FindAsync(id);
        if (dorama != null)
        {
            _context.Doramas.Remove(dorama);
            await _context.SaveChangesAsync();
        }
    }
}