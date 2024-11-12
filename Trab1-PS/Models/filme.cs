namespace Trab1_PS.Models;


public class Filme : Video
{
    private TimeSpan Duracao { get; set; }
    public Filme(string titulo, string descricao,string genero,DateTime dataLancamento, ICollection<Avaliacao> avaliacoes, TimeSpan duracao):base(titulo,descricao,genero,dataLancamento,avaliacoes)
    {
        Duracao=duracao;
    }
}