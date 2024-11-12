namespace Trab1_PS.Models;

public class Comentario
{
    private Usuario comentarista{get;set;}
    private string comentario{get;set;}
    private Video video {get;set;}

    public Comentario(Usuario comentarista, string comentario, Video video)
    {
        this.comentarista = comentarista;
        this.comentario = comentario;
        
    }
}