using Microsoft.EntityFrameworkCore;
using Trab1_PS.Models;

public class AvaliacaoDb : DbContext //Classe AvaliacaoDb herda de DbContext para comunicação com o banco de dados
{
    public AvaliacaoDb(DbContextOptions<AvaliacaoDb> options)
        : base(options) { }
    
    //Definindo as tabelas no banco
    public DbSet<Avaliacao> Avaliacoes { get; set; }
    public DbSet<Filme> Filmes { get; set; }
    public DbSet<Serie> Series { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    
    
    
}