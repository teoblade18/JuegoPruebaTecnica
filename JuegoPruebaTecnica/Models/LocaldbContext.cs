using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace JuegoPruebaTecnica.Models;

public partial class LocaldbContext : DbContext
{
    public LocaldbContext()
    {
    }

    public LocaldbContext(DbContextOptions<LocaldbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Jugador> Jugadores { get; set; }

    public virtual DbSet<Movimiento> Movimientos { get; set; }

    public virtual DbSet<Partida> Partidas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\localserver; DataBase=localdb;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Jugador>(entity =>
        {
            entity.HasKey(e => e.IdJugador).HasName("PK__Jugador__99E32016D4D44748");

            entity.ToTable("Jugador");

            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Movimiento>(entity =>
        {
            entity.HasKey(e => e.IdMovimiento).HasName("PK__Movimien__881A6AE081A980D3");

            entity.ToTable("Movimiento");

            entity.Property(e => e.Descripcion).HasMaxLength(255);

            entity.HasOne(d => d.IdJugadorNavigation).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.IdJugador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Movimient__IdJug__4E88ABD4");

            entity.HasOne(d => d.IdPartidaNavigation).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.IdPartida)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Movimient__IdPar__4F7CD00D");
        });

        modelBuilder.Entity<Partida>(entity =>
        {
            entity.HasKey(e => e.IdPartida).HasName("PK__Partida__6ED660C70B4423B7");

            entity.HasOne(d => d.IdGanadorNavigation).WithMany(p => p.Partidas)
                .HasForeignKey(d => d.IdGanador)
                .HasConstraintName("FK__Partida__IdGanad__4BAC3F29");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
