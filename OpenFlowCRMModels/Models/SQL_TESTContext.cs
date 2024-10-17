﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace OpenFlowCRMModels.Models
{
    public partial class SQL_TESTContext : DbContext
    {


        public SQL_TESTContext()
        {

        }


        public SQL_TESTContext(DbContextOptions<SQL_TESTContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Clienti> Clienti { get; set; }
        public virtual DbSet<ComponenteMerceCatalogoModelli> ComponenteMerceCatalogoModelli { get; set; }
        public virtual DbSet<ComponentiMerce> ComponentiMerce { get; set; }
        public virtual DbSet<CatalogoModelli> CatalogoModelli { get; set; }

        public virtual DbSet<Ordini> Ordini { get; set; }
        public virtual DbSet<Partite> Partite { get; set; }
        public virtual DbSet<VwPianoRiordino> VwPianoRiordino { get; set; }
        public virtual DbSet<Lotti> Lotti { get; set; }
        public virtual DbSet<Macchine> Macchine { get; set; }

       
        public virtual DbSet<Utenti> Utenti { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            string? connection_string = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION_STRING");
            if (connection_string== null) { throw new Exception("Set the DEFAULT_CONNECTION_STRING environment variable"); }

            optionsBuilder.UseSqlServer(connection_string);

            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lotti>(entity =>
            {
                entity.HasKey(e => e.LottoId);

                entity.Property(e => e.LottoId).HasColumnName("LottoID");

                entity.Property(e => e.DataFineEffettiva).HasColumnType("datetime");

                entity.Property(e => e.DataFinePrevista).HasColumnType("datetime");

                entity.Property(e => e.DataInizioEffettiva).HasColumnType("datetime");

                entity.Property(e => e.DataInizioPrevista).HasColumnType("datetime");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TipoMateriale).HasMaxLength(50);
            });

            modelBuilder.Entity<Macchine>(entity =>
            {
                entity.Property(e => e.MacchineId)
                    .ValueGeneratedNever()
                    .HasColumnName("MacchineID");

                entity.Property(e => e.Ip)
                    .HasMaxLength(50)
                    .HasColumnName("IP");

                entity.Property(e => e.Name).HasMaxLength(50);

            });

           
            modelBuilder.Entity<Utenti>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.PasswordHash).IsRequired();

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Clienti>(entity =>
            {
                entity.HasKey(e => e.Idcliente);

                entity.Property(e => e.Idcliente).HasColumnName("IDCliente");

                entity.Property(e => e.Indirizzo).HasMaxLength(150);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Note).HasMaxLength(50);
            });

            modelBuilder.Entity<ComponenteMerceCatalogoModelli>(entity =>
            {
                entity.HasKey(e => new { e.ComponenteMerceId, e.CatalogoModelliId });

                entity.Property(e => e.Quantita).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.ComponenteMerce)
                    .WithMany(p => p.ComponenteMerceCatalogoModelli)
                    .HasForeignKey(d => d.ComponenteMerceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ComponenteMerceCatalogoModelli_ComponentiMerce");

                entity.HasOne(d => d.CatalogoModelli)
                    .WithMany(p => p.ComponenteMerceCatalogoModelli)
                    .HasForeignKey(d => d.CatalogoModelliId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ComponenteMerceCatalogoModelli_CatalogoModelli");
            });

            modelBuilder.Entity<ComponentiMerce>(entity =>
            {
                entity.HasKey(e => e.IdcomponenteMerce);

                entity.Property(e => e.IdcomponenteMerce).HasColumnName("IDComponenteMerce");

                entity.Property(e => e.MaterialeComponente)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Note).HasMaxLength(140);

                entity.Property(e => e.TipoComponente)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<CatalogoModelli>(entity =>
            {
                entity.Property(e => e.Descrizione)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Ordini>(entity =>
            {
                entity.Property(e => e.OrdiniId).HasColumnName("OrdiniID");

                entity.HasOne(d => d.ClienteNavigation)
                    .WithMany(p => p.Ordini)
                    .HasForeignKey(d => d.Cliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ordini_Clienti");
            });

            modelBuilder.Entity<Partite>(entity =>
            {
                entity.Property(e => e.PartiteId).HasColumnName("PartiteID");

                entity.Property(e => e.DataConsegna).HasColumnType("datetime");

                entity.Property(e => e.Descrizione)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Ncarichi).HasColumnName("NCarichi");

                entity.HasOne(d => d.ModelloNavigation)
                    .WithMany(p => p.Partite)
                    .HasForeignKey(d => d.Modello)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Partite_Modelli");

                entity.HasOne(d => d.OrdineNavigation)
                    .WithMany(p => p.Partite)
                    .HasForeignKey(d => d.Ordine)
                    .HasConstraintName("FK_Ordini_Partite");
            });

            modelBuilder.Entity<VwPianoRiordino>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_PianoRiordino");

                entity.Property(e => e.DataConsegnaPartita).HasColumnType("datetime");

                entity.Property(e => e.MaterialeComponente)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TipoComponente)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}