namespace Trab1_PS.Models;


public class Serie : Categoria
{
    private ICollection<int> Episodios { get; set; }
    public Serie(int id,string titulo, string descricao,int ano, int mes, int dia, ICollection<Avaliacao> avaliacoes, List<int> episodios):base(id,titulo,descricao,ano,mes,dia,avaliacoes)
    {
        Episodios = episodios;
    }
}