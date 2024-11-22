﻿using IOCompletionCallback = System.Threading.IOCompletionCallback;
namespace Trab1_PS.Models;
public abstract class Dorama
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public List<string>  Genero { get; set; }
    public DateTime DataLancamento { get; set; }
    public ICollection<Avaliacao> Avaliacoes { get; set; }
    public ICollection<int> Episodios { get; set; }
    protected Dorama() 
    {
        Avaliacoes = new List<Avaliacao>();
        Genero = new List<string>();
    }
    protected Dorama(int id,string titulo, string descricao,int ano, int mes, int dia, ICollection<Avaliacao> avaliacoes,ICollection<int> episodios)
    {
        Id = id;
        Titulo = titulo;
        Descricao = descricao;
        Episodios = episodios;
        Avaliacoes = new List<Avaliacao>();
        DataLancamento = new DateTime(ano, mes, dia);
        Genero = new List<string>();
    }
}