using Trab1_PS.Data;
using Trab1_PS.Models;
using Microsoft.EntityFrameworkCore;
using Trab1_PS.Repository.Interfaces;

namespace Trab1_PS.Repository;

public class AvaliacaoRepository : IAvaliacaoRepository
{
    private readonly AppDbContext _context;

    public AvaliacaoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Avaliacao> AddAsync(Avaliacao avaliacao)
    {
        _context.Avaliacoes.Add(avaliacao);
        await _context.SaveChangesAsync();
        return avaliacao;
    }

    public async Task<Avaliacao> GetByIdAsync(int id)
    {
        return await _context.Avaliacoes.FindAsync(id);
    }

    public async Task UpdateAsync(Avaliacao avaliacao)
    {
        _context.Entry(avaliacao).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {   
        var avaliacao = await _context.Avaliacoes.FindAsync(id);
        if (avaliacao != null)
        {
            _context.Avaliacoes.Remove(avaliacao);
            await _context.SaveChangesAsync();
        }
    }
}