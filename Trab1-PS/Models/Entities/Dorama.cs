using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trab1_PS.Models.DTOs;

namespace Trab1_PS.Models;

public class Dorama
{
    [Key] // Define como chave primária
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Faz o EF gerar o ID automaticamente
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public int QtdEpisodios { get; set; }
    public DateTime DataLancamento { get; set; }
    public List<Avaliacao> Avaliacoes { get; set; } = new List<Avaliacao>();
    
    public List<int> GenerosIds { get; set; } = new List<int>();

    public List<Genero> Generos { get; set; } = new List<Genero>();

}