namespace Trab1_PS.Models;

public abstract class Video
{
    private int id { get; set; }
    private string Titulo { get; set; }
    private string Descricao { get; set; }
    private string Genero { get; set; }
    private DateTime DataLancamento { get; set; }
    private ICollection<Avaliacao> Avaliacoes { get; set; }

    protected Video(string titulo, string descricao,string genero,DateTime dataLancamento, ICollection<Avaliacao> avaliacoes)
    {
        
    }

}