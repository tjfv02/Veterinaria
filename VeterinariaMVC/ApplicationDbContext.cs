using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VeterinariaMVC.Models;

namespace VeterinariaMVC
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public virtual DbSet<Cita> Cita { get; set; }

        public virtual DbSet<Mascota> Mascota { get; set; }

        public virtual DbSet<Medicamento> Medicamentos { get; set; }

        public virtual DbSet<RecetaMedica> RecetaMedicas { get; set; }

        public virtual DbSet<RecetaMedicina> RecetaMedicinas { get; set; }

        public virtual DbSet<Usuario> Usuarios { get; set; }

        public virtual DbSet<Veterinario> Veterinarios { get; set; }

        public virtual DbSet<Veterinaria> Veterinaria { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cita>(entity =>
            {
                entity.HasKey(e => e.CitaId).HasName("PK_CITA");

                entity.Property(e => e.CitaId)
                    .ValueGeneratedNever()
                    .HasColumnName("CitaID");
                entity.Property(e => e.Fecha).HasColumnType("datetime");
                entity.Property(e => e.MascotaId).HasColumnName("MascotaID");
                entity.Property(e => e.VeterinariaId).HasColumnName("VeterinariaID");
                entity.Property(e => e.VeterinarioId).HasColumnName("VeterinarioID");

                entity.HasOne(d => d.Mascota).WithMany(p => p.Cita)
                    .HasForeignKey(d => d.MascotaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Cita_fk0");

                //entity.HasOne(d => d.Veterinaria).WithMany(p => p.Cita)
                //    .HasForeignKey(d => d.VeterinariaId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("Cita_fk1");

                //entity.HasOne(d => d.Veterinario).WithMany(p => p.Cita)
                //    .HasForeignKey(d => d.VeterinarioId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("Cita_fk2");
            });

            modelBuilder.Entity<Mascota>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_MASCOTA");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("MascotaID");
                entity.Property(e => e.Nombre)
                    .HasMaxLength(40)
                    .IsUnicode(false);
                entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

                entity.HasOne(d => d.Usuario).WithMany(p => p.Mascota)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Mascota_fk0");
            });

            modelBuilder.Entity<Medicamento>(entity =>
            {
                entity.HasKey(e => e.MedicamentoId).HasName("PK_MEDICAMENTO");

                entity.ToTable("Medicamento");

                entity.Property(e => e.MedicamentoId)
                    .ValueGeneratedNever()
                    .HasColumnName("MedicamentoID");
                entity.Property(e => e.Farmaceutica)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RecetaMedica>(entity =>
            {
                entity.HasKey(e => e.RecetaMedicaId).HasName("PK_RECETAMEDICA");

                entity.ToTable("RecetaMedica");

                entity.Property(e => e.RecetaMedicaId)
                    .ValueGeneratedNever()
                    .HasColumnName("RecetaMedicaID");
                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Fecha).HasColumnType("datetime");
                entity.Property(e => e.MascotaId).HasColumnName("MascotaID");
                entity.Property(e => e.Medicina)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.VeterinarioId).HasColumnName("VeterinarioID");
            });

            modelBuilder.Entity<RecetaMedicina>(entity =>
            {
                entity.HasKey(e => e.RecetaMedicinaId).HasName("PK_RECETAMEDICINA");

                entity.ToTable("RecetaMedicina");

                entity.Property(e => e.RecetaMedicinaId)
                    .ValueGeneratedNever()
                    .HasColumnName("RecetaMedicinaID");
                entity.Property(e => e.MedicamentoId).HasColumnName("MedicamentoID");
                entity.Property(e => e.RecetaMedicaId).HasColumnName("RecetaMedicaID");

                entity.HasOne(d => d.Medicamento).WithMany(p => p.RecetaMedicinas)
                    .HasForeignKey(d => d.MedicamentoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RecetaMedicina_fk1");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.UsuarioId).HasName("PK_USUARIO");

                entity.ToTable("Usuario");

                entity.Property(e => e.UsuarioId)
                    .ValueGeneratedNever()
                    .HasColumnName("UsuarioID");
                entity.Property(e => e.Apellido)
                    .HasMaxLength(40)
                    .IsUnicode(false);
                entity.Property(e => e.Contraseña)
                    .HasMaxLength(40)
                    .IsUnicode(false);
                entity.Property(e => e.Email)
                    .HasMaxLength(40)
                    .IsUnicode(false);
                entity.Property(e => e.Nombre)
                    .HasMaxLength(40)
                    .IsUnicode(false);
                entity.Property(e => e.Telefono)
                    .HasMaxLength(8)
                    .IsUnicode(false);
                entity.Property(e => e.Usuario1)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("Usuario");
            });

            modelBuilder.Entity<Veterinario>(entity =>
            {
                entity.HasKey(e => e.VeterinarioId).HasName("PK_VETERINARIO");

                entity.ToTable("Veterinario");

                entity.Property(e => e.VeterinarioId)
                    .ValueGeneratedNever()
                    .HasColumnName("VeterinarioID");
                entity.Property(e => e.Apellido)
                    .HasMaxLength(40)
                    .IsUnicode(false);
                entity.Property(e => e.Nombre)
                    .HasMaxLength(40)
                    .IsUnicode(false);
                entity.Property(e => e.VeterinariaId).HasColumnName("VeterinariaID");
            });

            modelBuilder.Entity<Veterinaria>(entity =>
            {
                entity.HasKey(e => e.VeterinariaId).HasName("PK_VETERINARIA");

                entity.Property(e => e.VeterinariaId)
                    .ValueGeneratedNever()
                    .HasColumnName("VeterinariaID");
                entity.Property(e => e.Email)
                    .HasMaxLength(40)
                    .IsUnicode(false);
                entity.Property(e => e.Telefono)
                    .HasMaxLength(8)
                    .IsUnicode(false);
                entity.Property(e => e.Ubicacion)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
