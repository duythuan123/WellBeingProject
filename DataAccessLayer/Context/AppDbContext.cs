using System;
using System.Collections.Generic;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.Context
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<Psychiatrist> Psychiatrists { get; set; } = null!;
        public virtual DbSet<TimeSlot> TimeSlots { get; set; } = null!;
        public virtual DbSet<Token> Tokens { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
          => optionsBuilder.UseSqlServer(GetConnectionString());

        string GetConnectionString()
        {
            IConfiguration builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            return builder["ConnectionStrings:DefaultConnection"];
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasIndex(e => e.PsychiatristId, "IX_Booking_PsychiatristId");

                entity.HasIndex(e => e.UserId, "IX_Booking_UserId");

                entity.HasOne(d => d.Psychiatrist)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.PsychiatristId)
                    .HasConstraintName("FK_Booking_Psychiatrist_PsychiatristId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Booking_Users_UserId");
            });

            modelBuilder.Entity<Psychiatrist>(entity =>
            {
                entity.Property(e => e.ConsultationFee).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Location).IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Psychiatrists)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<TimeSlot>(entity =>
            {
                entity.Property(e => e.DateOfWeek).HasMaxLength(50);

                entity.HasOne(d => d.Psychiatrist)
                    .WithMany(p => p.TimeSlots)
                    .HasForeignKey(d => d.PsychiatristId)
                    .HasConstraintName("FK__TimeSlot__Psychi__0B91BA14");
            });

            modelBuilder.Entity<Token>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_Tokens_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tokens)
                    .HasForeignKey(d => d.UserId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
