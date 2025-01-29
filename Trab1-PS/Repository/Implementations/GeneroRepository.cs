using Trab1_PS.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Trab1_PS.Models;
using Trab1_PS.Data;
using Trab1_PS.Models.DTOs;

namespace Trab1_PS.Repository
{
    public class GeneroRepository : IGeneroRepository
    {
        private readonly AppDbContext _context;

        public GeneroRepository(AppDbContext context)
        {
            _context = context;
        }

        // Método para criar um novo gênero no banco de dados
        public async Task<GeneroDTO> CriarGeneroAsync(GeneroDTO generoDto)
        {
            // Cria a entidade Genero a partir do DTO, mas sem o ID
            var genero = new Genero
            {
                Nome = generoDto.Nome // O Id será gerado automaticamente
            };

            // Adiciona o gênero no banco de dados
            await _context.Generos.AddAsync(genero);

            // Salva as mudanças no banco de dados (o Id será gerado automaticamente)
            await _context.SaveChangesAsync();

            // Retorna o DTO com o Id gerado automaticamente
            return new GeneroDTO
            {
                Nome = genero.Nome // O Id gerado é apenas retornado, não deve ser preenchido no DTO de entrada
            };
        }

        // Método para obter um gênero pelo ID
        public async Task<GeneroDTO> ObterGeneroPorIdAsync(int id)
        {
            var genero = await _context.Generos
                .Where(g => g.Id == id)
                .FirstOrDefaultAsync();

            if (genero == null) return null;

            return new GeneroDTO
            {
                Nome = genero.Nome // Não retornamos o Id aqui
            };
        }

        // Método para obter um gênero pelo nome
        public async Task<GeneroDTO> ObterGeneroPorNomeAsync(string nome)
        {
            var genero = await _context.Generos
                .FirstOrDefaultAsync(g => g.Nome.ToLower() == nome.ToLower());

            if (genero == null) return null;

            return new GeneroDTO
            {
                Nome = genero.Nome // Não retornamos o Id na busca pelo nome
            };
        }

        // Método para listar os gêneros pelos IDs fornecidos
        public async Task<IEnumerable<GeneroDTO>> ListarGenerosAsync(List<int> doramaGenerosIds)
        {
            var generos = await _context.Generos
                .Where(g => doramaGenerosIds.Contains(g.Id))
                .ToListAsync();

            return generos.Select(g => new GeneroDTO
            {
                Nome = g.Nome // Não retornamos o Id no DTO
            }).ToList();
        }
    }
}
