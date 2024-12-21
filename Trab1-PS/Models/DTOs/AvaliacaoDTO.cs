public class AvaliacaoDTO
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }  // ID do Usuário
    public int DoramaId { get; set; }   // ID do Dorama
    public int Nota { get; set; }
    public string Comentario { get; set; }
    public DateTime DataAvaliacao { get; set; }

    public AvaliacaoDTO(int id, int usuarioId, int doramaId, int nota, string comentario, DateTime dataAvaliacao)
    {
        Id = id;
        UsuarioId = usuarioId;
        DoramaId = doramaId;
        Nota = nota;
        Comentario = comentario;
        DataAvaliacao = dataAvaliacao;
    }
}