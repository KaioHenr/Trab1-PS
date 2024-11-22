using Trab1_PS.Models;

namespace Trab1_PS.dto;

public class AvaliacaoDTO
{
    public int Id { get; set; }
    public int IdUsuario { get; set; }
    
    public int IdDorama { get; set; }
    public int Nota { get; set; } // Ex: 1 a 5
    public string Comentario { get; set; }
    
    public DateTime DataAvaliacao { get; set; }


    public AvaliacaoDTO(int id, int usuario, int idDorama, int nota, string comentario, int ano, int mes, int dia)
    {
        Id = id;
        IdUsuario = usuario;
        IdDorama = idDorama;
        Nota = nota;
        Comentario = comentario;
        DataAvaliacao = new DateTime(ano, mes, dia);
    }
}
