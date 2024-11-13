namespace Trab1_PS.Models;

public class Avaliacao
{
    public int Id { get; set; }
    public Usuario Usuario { get; set; }
    public Categoria Categoria { get; set; } 
    public int Nota { get; set; } 
    public string Comentario { get; set; }
    public DateTime DataAvaliacao { get; set; }
    public Avaliacao(int id, Usuario usuario, Categoria categoria, int nota, string comentario, DateTime dataAvaliacao)
    {
        Id = id;
        Usuario = usuario;
        Categoria = categoria;
        Nota = nota;
        Comentario = comentario;
        DataAvaliacao = dataAvaliacao;
    }
}
