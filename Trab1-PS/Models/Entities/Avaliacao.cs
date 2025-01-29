using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trab1_PS.Models;

public class Avaliacao
{
    [Key] // Este atributo é opcional, pois o EF já considera a propriedade 'Id' como chave primária por convenção
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public int DoramaId { get; set; }
    public int Nota { get; set; }
    public string Comentario { get; set; }
    public DateTime DataAvaliacao { get; set; }
}
