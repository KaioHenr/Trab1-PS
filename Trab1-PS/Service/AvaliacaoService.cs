using Trab1_PS.dto;
using Trab1_PS.Models;
using Trab1_PS.Repository.Interfaces;
using Trab1_PS.Services;
using System.Threading.Tasks;

namespace Trab1_PS.Services
{
    public class AvaliacaoService : IAvaliacaoService
    {
        private readonly IAvaliacaoRepository _avaliacaoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IDoramaRepository _doramaRepository;

        public AvaliacaoService(IAvaliacaoRepository avaliacaoRepository, IUsuarioRepository usuarioRepository, IDoramaRepository doramaRepository)
        {
            _avaliacaoRepository = avaliacaoRepository;
            _usuarioRepository = usuarioRepository;
            _doramaRepository = doramaRepository;
        }

        public async Task<(bool, string)> CadastrarAvaliacao(AvaliacaoDTO avaliacaoDTO)
        {
            // Verifica se o Usuário existe
            var usuario = await _usuarioRepository.GetByIdAsync(avaliacaoDTO.UsuarioId);
            if (usuario == null)
                return (false, $"Usuário com ID {avaliacaoDTO.UsuarioId} não encontrado.");

            // Verifica se o Dorama existe
            var dorama = await _doramaRepository.GetByIdAsync(avaliacaoDTO.DoramaId);
            if (dorama == null)
                return (false, $"Dorama com ID {avaliacaoDTO.DoramaId} não encontrado.");

            // Cria a entidade Avaliacao
            var avaliacao = new Avaliacao
            {
                UsuarioId = avaliacaoDTO.UsuarioId,
                DoramaId = avaliacaoDTO.DoramaId,
                Nota = avaliacaoDTO.Nota,
                Comentario = avaliacaoDTO.Comentario,
                DataAvaliacao = avaliacaoDTO.DataAvaliacao
            };

            await _avaliacaoRepository.AddAsync(avaliacao);
            return (true, $"Avaliação cadastrada com sucesso! ID: {avaliacao.Id}");
        }

        public async Task<(bool, string)> EditarAvaliacao(int id, AvaliacaoDTO avaliacaoDTO)
        {
            var avaliacao = await _avaliacaoRepository.GetByIdAsync(id);
            if (avaliacao == null)
                return (false, "Avaliação não encontrada.");

            var usuario = await _usuarioRepository.GetByIdAsync(avaliacaoDTO.UsuarioId);
            if (usuario == null)
                return (false, $"Usuário com ID {avaliacaoDTO.UsuarioId} não encontrado.");

            var dorama = await _doramaRepository.GetByIdAsync(avaliacaoDTO.DoramaId);
            if (dorama == null)
                return (false, $"Dorama com ID {avaliacaoDTO.DoramaId} não encontrado.");

            // Atualiza os campos da avaliação
            avaliacao.Nota = avaliacaoDTO.Nota;
            avaliacao.Comentario = avaliacaoDTO.Comentario;
            avaliacao.DataAvaliacao = avaliacaoDTO.DataAvaliacao;

            await _avaliacaoRepository.UpdateAsync(avaliacao);
            return (true, "Avaliação editada com sucesso!");
        }

        public async Task<(bool, string)> DeletarAvaliacao(int id)
        {
            var avaliacao = await _avaliacaoRepository.GetByIdAsync(id);
            if (avaliacao == null)
                return (false, "Avaliação não encontrada.");

            await _avaliacaoRepository.DeleteAsync(id);
            return (true, "Avaliação deletada com sucesso!");
        }

        public async Task<Avaliacao> ObterAvaliacaoPorId(int id)
        {
            return await _avaliacaoRepository.GetByIdAsync(id);
        }
    }
}
