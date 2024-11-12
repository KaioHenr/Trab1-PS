namespace Trab1_PS.Models;

public class Avaliacao
{
    public int Id { get; set; }
    public Usuario Usuario { get; set; }
    public Video Video { get; set; }  // Agora Video é genérico para Filme ou Serie
    public int Nota { get; set; }  // Ex: 1 a 5 estrelas
    public string Comentario { get; set; }
    public DateTime DataAvaliacao { get; set; }
}
