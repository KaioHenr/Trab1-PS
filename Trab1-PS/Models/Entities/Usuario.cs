using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trab1_PS.Models;

public class Usuario
{
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Gera automaticamente o Id
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public List<Avaliacao> Avaliacoes { get; set; } = new List<Avaliacao>();
} 