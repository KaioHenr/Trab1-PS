using Trab1_PS.dto;
using Trab1_PS.Models;
using Trab1_PS.Repository.Interfaces;
using Trab1_PS.Services;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Trab1_PS.Services
{
    public class DoramaService : IDoramaService
    {
        private readonly IDoramaRepository _doramaRepository;
        private readonly IGeneroRepository _generoRepository;

        public DoramaService(IDoramaRepository doramaRepository, IGeneroRepository generoRepository)
        {
            _doramaRepository = doramaRepository;
            _generoRepository = generoRepository;
        }

        public async Task<(bool, string)> CadastrarDorama(DoramaDTO doramaDTO)
        {
            // Verifica se o título já existe
            if (await _doramaRepository.ExistsByTituloAsync(doramaDTO.Titulo))
                return (false, "Dorama já cadastrado!");

            // Verifica se há gêneros informados
            if (doramaDTO.Generos == null || !doramaDTO.Generos.Any())
                return (false, "Gêneros não informados ou inválidos!");

            // Busca os gêneros existentes no banco pelo ID
            var generos = await _generoRepository.ListarGenerosAsync(doramaDTO.Generos);

            // Verifica se todos os gêneros fornecidos foram encontrados
            if (generos.Count() != doramaDTO.Generos.Count)
                return (false, "Um ou mais gêneros não foram encontrados!");

            // Cria o objeto Dorama
            var dorama = new Dorama
            {
                Titulo = doramaDTO.Titulo,
                Descricao = doramaDTO.Descricao,
                QtdEpisodios = doramaDTO.QtdEpisodios,
                DataLancamento = doramaDTO.DataLancamento,
                GenerosIds = doramaDTO.Generos
            };

            // Adiciona ao banco de dados
            await _doramaRepository.AddAsync(dorama);

            return (true, $"Dorama registrado com sucesso! ID: {dorama.Id}");
        }

        public async Task<(bool, string)> EditarDorama(int id, DoramaDTO doramaDTO)
        {
            var existingDorama = await _doramaRepository.GetByIdAsync(id);
            if (existingDorama == null)
                return (false, "Dorama não encontrado");

            // Atualiza o título e a descrição apenas se o usuário fornecer valores novos
            if (!string.IsNullOrEmpty(doramaDTO.Titulo))
                existingDorama.Titulo = doramaDTO.Titulo;

            if (!string.IsNullOrEmpty(doramaDTO.Descricao))
                existingDorama.Descricao = doramaDTO.Descricao;

            // Atualiza os gêneros com os novos IDs fornecidos, se existirem
            if (doramaDTO.Generos != null && doramaDTO.Generos.Any())
            {
                // Verifica se os gêneros fornecidos são válidos
                var generos = await _generoRepository.ListarGenerosAsync(doramaDTO.Generos);

                if (generos.Count() != doramaDTO.Generos.Count)
                    return (false, "Um ou mais gêneros não foram encontrados!");

                // Associa os IDs dos gêneros ao dorama
                existingDorama.GenerosIds = doramaDTO.Generos;
            }

            // Atualiza o dorama no banco de dados
            await _doramaRepository.UpdateAsync(existingDorama);
            return (true, "Dorama editado com sucesso!");
        }

        public async Task<(bool, string)> DeletarDorama(int id)
        {
            var existingDorama = await _doramaRepository.GetByIdAsync(id);
            if (existingDorama == null)
                return (false, "Dorama não encontrado");

            await _doramaRepository.DeleteAsync(id);
            return (true, "Dorama excluído com sucesso!");
        }

        public async Task<IEnumerable<Dorama>> PesquisarDorama(string titulo)
        {
            return await _doramaRepository.SearchByTituloAsync(titulo);
        }

        public async Task<Dorama> ObterDoramaPorId(int id)
        {
            return await _doramaRepository.GetByIdAsync(id);
        }
    }
}
