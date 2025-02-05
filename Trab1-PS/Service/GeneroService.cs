using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trab1_PS.Models;
using Trab1_PS.Models.DTOs;
using Trab1_PS.Repository.Interfaces;

namespace Trab1_PS.Services
{
    public class GeneroService : IGeneroService
    {
        private readonly IGeneroRepository _generoRepository;

        public GeneroService(IGeneroRepository generoRepository)
        {
            _generoRepository = generoRepository;
        }

        public async Task<(bool, string)> CriarGenero(GeneroDTO generoDto)
        {
            if (generoDto == null || string.IsNullOrWhiteSpace(generoDto.Nome))
            {
                return (false, "Nome do gênero é obrigatório.");
            }

            var generoExistente = await _generoRepository.ObterGeneroPorNomeAsync(generoDto.Nome);
            if (generoExistente != null)
            {
                return (false, $"O gênero '{generoDto.Nome}' já existe.");
            }

            var generoCriado = await _generoRepository.CriarGeneroAsync(generoDto);
            return (true, "Gênero cadastrado com sucesso.");
        }

        public async Task<GeneroDTO> ObterGeneroPorNome(string nome)
        {
            var genero = await _generoRepository.ObterGeneroPorNomeAsync(nome);
            if (genero == null)
            {
                throw new KeyNotFoundException($"Gênero '{nome}' não encontrado.");
            }

            return new GeneroDTO { Nome = genero.Nome };
        }

        public async Task<GeneroDTO> ObterGeneroPorId(int id)
        {
            var genero = await _generoRepository.ObterGeneroPorIdAsync(id);
            if (genero == null)
            {
                throw new KeyNotFoundException($"Gênero com id {id} não encontrado.");
            }

            return new GeneroDTO { Nome = genero.Nome };
        }

        public async Task<IEnumerable<GeneroDTO>> ListarGeneros(List<int> generoIds)
        {
            if (generoIds == null || !generoIds.Any())
            {
                throw new ArgumentException("Lista de IDs de gêneros não fornecida ou vazia.");
            }

            var generos = await _generoRepository.ListarGenerosAsync(generoIds);
            if (!generos.Any())
            {
                throw new KeyNotFoundException("Nenhum gênero encontrado para os IDs fornecidos.");
            }

            return generos.Select(g => new GeneroDTO { Nome = g.Nome }).ToList();
        }
    }
}