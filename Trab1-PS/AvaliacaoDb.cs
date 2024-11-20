using Microsoft.EntityFrameworkCore;
using Trab1_PS.Models;

public class AvaliacaoDb : DbContext
{
    public AvaliacaoDb(DbContextOptions<AvaliacaoDb> options)
        : base(options) { }

    // Definindo as tabelas no banco
    public DbSet<Avaliacao> Avaliacoes { get; set; }
    public DbSet<Filme> Filmes { get; set; }
    public DbSet<Serie> Series { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Categoria> Categorias { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuração para a herança
        modelBuilder.Entity<Categoria>()
            .HasDiscriminator<string>("CategoriaType") // Coluna para discriminar entre Filme, Série etc.
            .HasValue<Categoria>("BaseCategoria")
            .HasValue<Filme>("Filme");

        // Configurações para Filme
        modelBuilder.Entity<Filme>()
            .Property(f => f.Duracao)
            .HasConversion(
                v => v.TotalMinutes,         // Converter TimeSpan para o banco (em minutos)
                v => TimeSpan.FromMinutes(v) // Converter minutos para TimeSpan
            );

        // Configurações para as propriedades de Categoria
        modelBuilder.Entity<Categoria>()
            .Property(c => c.Titulo)
            .IsRequired(); // Exemplo: Título é obrigatório

        modelBuilder.Entity<Categoria>()
            .Property(c => c.DataLancamento)
            .IsRequired();

        base.OnModelCreating(modelBuilder);
    }
}