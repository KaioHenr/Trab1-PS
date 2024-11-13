namespace Trab1_PS.Models;
public class Usuario
{
    private int Id { get; set; }
    private string Nome { get; set; }
    private string Email { get; set; }
    private string Senha { get; set; }
    private ICollection<Avaliacao> Avaliacoes { get; set; }
    public Usuario(int id, string nome, string email, string senha, ICollection<Avaliacao> avaliacoes)
    {
        Id = id;
        Nome = nome;
        Email = email;
        Senha = senha;
        Avaliacoes = new List<Avaliacao>();
    }
}