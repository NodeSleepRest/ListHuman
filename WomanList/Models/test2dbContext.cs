using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WomanList.Models
{
    public partial class test2dbContext : DbContext
    {
        public test2dbContext()
        {
        }

        public test2dbContext(DbContextOptions<test2dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dating> Dating { get; set; }
        public virtual DbSet<Method> Method { get; set; }
        public virtual DbSet<Prefecture> Prefecture { get; set; }
        public virtual DbSet<Woman> Woman { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=test2db;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dating>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.女性id).HasColumnName("女性ID");

                entity.HasOne(d => d.女性)
                    .WithMany(p => p.Dating)
                    .HasForeignKey(d => d.女性id)
                    .HasConstraintName("FK_Dating_Woman");
            });

            modelBuilder.Entity<Method>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<Prefecture>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.名称).HasMaxLength(50);
            });

            modelBuilder.Entity<Woman>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.仮名).HasMaxLength(50);

                entity.Property(e => e.出会った日).HasColumnType("date");

                entity.Property(e => e.本名).HasMaxLength(50);

                entity.HasOne(d => d.出身地Navigation)
                    .WithMany(p => p.Woman出身地Navigation)
                    .HasForeignKey(d => d.出身地)
                    .HasConstraintName("FK_Woman_Prefecture1");

                entity.HasOne(d => d.居住地Navigation)
                    .WithMany(p => p.Woman居住地Navigation)
                    .HasForeignKey(d => d.居住地)
                    .HasConstraintName("FK_Woman_Prefecture");

                entity.HasOne(d => d.知り合った方法Navigation)
                    .WithMany(p => p.Woman)
                    .HasForeignKey(d => d.知り合った方法)
                    .HasConstraintName("FK_Woman_Method");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
