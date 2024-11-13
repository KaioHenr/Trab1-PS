namespace Trab1_PS.Models;


public class Filme : Categoria
{
    private TimeSpan Duracao { get; set; }
    public Filme(int id,string titulo, string descricao,int ano, int mes, int dia, ICollection<Avaliacao> avaliacoes, int duracao):base(id,titulo,descricao,ano,mes,dia,avaliacoes)
    {
        Duracao= TimeSpan.FromMinutes(duracao);
    }
}