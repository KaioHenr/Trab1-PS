using Microsoft.EntityFrameworkCore;
using Trab1_PS.Models;
//Para interagir com o banco
public class AppDbContext : DbContext //AppDbContext herda de DbCcontext (classe que faz comunicação com banco)
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }
    // Definindo as tabelas no banco
    public DbSet<Avaliacao> Avaliacoes { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Dorama> Doramas { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurações para as propriedades de Categoria
        modelBuilder.Entity<Dorama>()
            .Property(c => c.Titulo)
            .IsRequired(); // Exemplo: Título é obrigatório

        modelBuilder.Entity<Dorama>()
            .Property(c => c.DataLancamento)
            .IsRequired();
        base.OnModelCreating(modelBuilder);
    }
}