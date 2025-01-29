using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trab1_PS.Models;

public class Genero
{
    [Key] // Define como chave primária
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Faz o EF gerar o ID automaticamente
    public int Id { get; set; }
    public string Nome { get; set; }

}