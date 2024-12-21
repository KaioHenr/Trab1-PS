namespace Trab1_PS.Models;

public class Avaliacao
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
    public int DoramaId { get; set; }
    public Dorama Dorama { get; set; }
    public int Nota { get; set; }
    public string Comentario { get; set; }
    public DateTime DataAvaliacao { get; set; }
}