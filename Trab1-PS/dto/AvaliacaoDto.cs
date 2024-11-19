namespace Trab1_PS.dto;

public class AvaliacaoDto
{
    public int Usuario { get; set; }
    
    public int Categoria { get; set; }
    public int Nota { get; set; } // Ex: 1 a 5
    public string Comentario { get; set; }
    
    public DateTime DataAvaliacao { get; set; }


    public AvaliacaoDto(int usuario, int categoria, int nota, string comentario, int ano, int mes, int dia)
    {
        Usuario = usuario;
        Categoria = categoria;
        Nota = nota;
        Comentario = comentario;
        DataAvaliacao = new DateTime(ano, mes, dia);
    }
}

