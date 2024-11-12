namespace Trab1_PS.Models;


public class Serie : Video
{
    private List<int> Episodios { get; set; }

    public Serie(string titulo, string descricao,string genero,DateTime dataLancamento, ICollection<Avaliacao> avaliacoes, List<int> episodios):base(titulo,descricao,genero,dataLancamento,avaliacoes)
    {
        Episodios=episodios;
    }
}