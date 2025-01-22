namespace Trab1_PS.Models
{
    public class DoramaDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int QtdEpisodios { get; set; }
        public DateTime DataLancamento { get; set; }
        public List<string> Generos { get; set; } = new List<string>();
        public List<AvaliacaoDTO> Avaliacoes { get; set; } = new List<AvaliacaoDTO>();

        public DoramaDTO(int id, string titulo, string descricao, int qtdEpisodios, DateTime dataLancamento, List<string> generos, List<AvaliacaoDTO> avaliacoes)
        {
            Id = id;
            Titulo = titulo;
            Descricao = descricao;
            QtdEpisodios = qtdEpisodios;
            DataLancamento = dataLancamento;
            Generos = generos;
            Avaliacoes = avaliacoes;
        }
    }
}