using IOCompletionCallback = System.Threading.IOCompletionCallback;
namespace Trab1_PS.Models;
public abstract class Categoria
{
    private int id { get; set; }
    private string Titulo { get; set; }
    private string Descricao { get; set; }
    private List<string>  Genero { get; set; }
    private DateTime DataLancamento { get; set; }
    private ICollection<Avaliacao> Avaliacoes { get; set; }
    protected Categoria(int id,string titulo, string descricao,int ano, int mes, int dia, ICollection<Avaliacao> avaliacoes)
    {
        Titulo = titulo;
        Descricao = descricao;
        Avaliacoes = new List<Avaliacao>();
        DataLancamento = new DateTime(ano, mes, dia);
        Genero = new List<string>();
    }
}