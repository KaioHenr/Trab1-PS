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

        // Criar um novo gênero no banco de dados
        public async Task<GeneroDTO> CriarGeneroAsync(GeneroDTO generoDto)
        {
            // Convertendo o DTO para a entidade do modelo e atribuindo o ID
            var genero = new Genero
            {
                Id = generoDto.Id, // Respeita o ID fornecido pelo usuário
                Nome = generoDto.Nome
            };

            // Adicionando o gênero no contexto do banco de dados
            await _context.Generos.AddAsync(genero);
            await _context.SaveChangesAsync(); // Salvando as alterações no banco

            return generoDto; // Retorna o DTO sem alterar o ID fornecido
        }

        // Obter um gênero pelo ID
        public async Task<GeneroDTO> ObterGeneroPorIdAsync(int id)
        {
            var genero = await _context.Generos
                .Where(g => g.Id == id)
                .FirstOrDefaultAsync();

            // Retornando o DTO com as informações do gênero encontrado
            if (genero == null) return null;

            return new GeneroDTO
            {
                Id = genero.Id,
                Nome = genero.Nome
            };
        }

        // Listar todos os gêneros
        public async Task<IEnumerable<GeneroDTO>> ListarGenerosAsync(List<int> doramaGenerosIds)
        {
            var generos = await _context.Generos
                .Where(g => doramaGenerosIds.Contains(g.Id)) // Filtra os gêneros pelos IDs fornecidos
                .ToListAsync();

            return generos.Select(g => new GeneroDTO
            {
                Id = g.Id,
                Nome = g.Nome
            }).ToList();
        }




        public async Task<GeneroDTO> ObterGeneroPorNomeAsync(string nome)
        {
            var genero = await _context.Generos
                .FirstOrDefaultAsync(g =>
                    g.Nome.ToLower() == nome.ToLower()); // Compara nomes ignorando maiúsculas/minúsculas

            if (genero == null) return null;

            return new GeneroDTO

            {
                Id = genero.Id,
                Nome = genero.Nome
            };

        }
        
        public async Task<IEnumerable<Genero>> ListarGenerosAsync(IEnumerable<int> generoIds)
        {
            return await _context.Generos
                .Where(g => generoIds.Contains(g.Id))
                .ToListAsync();
        }
    }
}