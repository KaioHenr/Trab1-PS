namespace Trab1_PS.Models;

public class Usuario
{
    public int idUsuario { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    
    public ICollection<Avaliacao> Avaliacoes { get; set; }
}