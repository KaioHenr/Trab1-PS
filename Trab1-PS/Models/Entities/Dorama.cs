using IOCompletionCallback = System.Threading.IOCompletionCallback;
namespace Trab1_PS.Models;
public class Dorama
{
    
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public List<string>  Genero { get; set; }
    public DateTime DataLancamento { get; set; }
    public int Episodios { get; set; }
    public ICollection<Avaliacao> Avaliacoes { get; set; }
    
    public Dorama() 
    {
        Avaliacoes = new List<Avaliacao>();
        Genero = new List<string>();
    }
    public Dorama(int id,string titulo, string descricao,int ano, int mes, int dia, int episodios)
    {
        Id = id;
        Titulo = titulo;
        Descricao = descricao;
        Episodios = episodios;
        DataLancamento = new DateTime(ano, mes, dia);
        Genero = new List<string>();
        Avaliacoes = new List<Avaliacao>();
    }
}