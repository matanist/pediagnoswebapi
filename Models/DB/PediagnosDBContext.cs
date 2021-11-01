using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace pediagnoswebapi.Models.DB
{
    public partial class PediagnosDBContext : DbContext
    {
        public PediagnosDBContext()
        {
        }

        public PediagnosDBContext(DbContextOptions<PediagnosDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Pet> Pets { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-T2R99UQ\\SQLEXPRESS;Database=PediagnosDB;User Id=sa;Password=Password1");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Pet>(entity =>
            {
                entity.ToTable("Pet");

                entity.Property(e => e.Ad).HasMaxLength(255);

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Pets)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("FK_Pet_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Ad).HasMaxLength(100);

                entity.Property(e => e.KullaniciAdi).HasMaxLength(100);

                entity.Property(e => e.Rol).HasMaxLength(100);

                entity.Property(e => e.Sifre).HasMaxLength(100);

                entity.Property(e => e.Soyad).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
