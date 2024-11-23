using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trab1_PS.Models;

public class Avaliacao
{
    [Key] 
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
    //Para gerar o id automaticamente 
    public int Id { get; set; }
    public String EmailUsuario { get; set; }
    
    public String NomeDorama { get; set; } 
    public int Nota { get; set; } 
    public string Comentario { get; set; }
    public DateTime DataAvaliacao { get; set; }
    
    public Avaliacao() { }
    public Avaliacao(int id, String email, String nome, int nota, string comentario, int ano, int mes, int dia)
    {
        Id = id;
        EmailUsuario = email;
        NomeDorama = nome;
        Nota = nota;
        Comentario = comentario;
        DataAvaliacao = new DateTime(ano,mes, dia);
    }
}
