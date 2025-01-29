

namespace Trab1_PS.Models
{
    public class DoramaDTO
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int QtdEpisodios { get; set; }
        public DateTime DataLancamento { get; set; }
        public List<AvaliacaoDTO> Avaliacoes { get; set; } = new List<AvaliacaoDTO>();
        public List<int> Generos { get; set; } = new List<int>();  






      
    }
}