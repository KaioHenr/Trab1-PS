namespace Trab1_PS.Models;

public class Dorama
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public int QtdEpisodios { get; set; }
    public DateTime DataLancamento { get; set; }
    public List<Genero> Generos { get; set; } = new List<Genero>();
    public List<Avaliacao> Avaliacoes { get; set; } = new List<Avaliacao>();
}