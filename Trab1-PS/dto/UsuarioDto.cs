namespace Trab1_PS.dto;

public class UsuarioDto
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public int Id { get; set; }


    public UsuarioDto(string nome, string email, string senha, int id)
    {
        Nome = nome;
        Email = email;
        Senha = senha;
        Id = id;
    }

}

