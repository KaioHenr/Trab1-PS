﻿using Microsoft.EntityFrameworkCore;
using Trab1_PS.Models;

namespace Trab1_PS.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Dorama> Doramas { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }
        public DbSet<Genero> Generos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired();
                entity.Property(e => e.Email).IsRequired();

                entity.HasMany(e => e.Avaliacoes)
                    .WithOne(a => a.Usuario)
                    .HasForeignKey(a => a.UsuarioId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Dorama>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Titulo).IsRequired();
                entity.Property(e => e.Descricao).IsRequired();
                entity.Property(e => e.DataLancamento).IsRequired();

                entity.HasMany(e => e.Avaliacoes)
                    .WithOne(a => a.Dorama)
                    .HasForeignKey(a => a.DoramaId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Avaliacao>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Comentario).IsRequired();
                entity.Property(e => e.Nota).IsRequired();

                entity.HasOne(e => e.Usuario)
                    .WithMany(u => u.Avaliacoes)
                    .HasForeignKey(e => e.UsuarioId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Dorama)
                    .WithMany(d => d.Avaliacoes)
                    .HasForeignKey(e => e.DoramaId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Genero>(entity =>
            {
                entity.HasKey(g => g.Id);
                entity.Property(g => g.Nome).IsRequired();
            });
        }
    }
}
