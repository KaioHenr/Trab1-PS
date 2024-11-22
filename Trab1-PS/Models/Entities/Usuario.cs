﻿namespace Trab1_PS.Models;
public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public ICollection<Avaliacao> Avaliacoes { get; set; }
    
    public Usuario() { }
    public Usuario(int id, string nome, string email, string senha)
    {
        Id = id;
        Nome = nome;
        Email = email;
        Senha = senha;
        Avaliacoes = new List<Avaliacao>();
    }
}