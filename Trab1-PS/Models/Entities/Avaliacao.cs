namespace Trab1_PS.Models;

public class Avaliacao
{
    public int Id { get; set; }
    public int IdUsuario { get; set; }
    public int IdCategoria { get; set; } 
    public int Nota { get; set; } 
    public string Comentario { get; set; }
    public DateTime DataAvaliacao { get; set; }
    
    public Avaliacao() { }
    public Avaliacao(int id, int usuario, int categoria, int nota, string comentario, int ano, int mes, int dia)
    {
        Id = id;
        IdUsuario = usuario;
        IdCategoria = categoria;
        Nota = nota;
        Comentario = comentario;
        DataAvaliacao = new DateTime(ano,mes, dia);
    }
}
