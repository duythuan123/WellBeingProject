﻿using System;
using System.Collections.Generic;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataAccessLayer.Context
{
    public partial class WellMeetDbContext : DbContext
    {
        public WellMeetDbContext()
        {
        }

        public WellMeetDbContext(DbContextOptions<WellMeetDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<Psychiatrist> Psychiatrists { get; set; } = null!;
        public virtual DbSet<TimeSlot> TimeSlots { get; set; } = null!;
        public virtual DbSet<Token> Tokens { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server =localhost; database =WellMeetDb;uid=sa;pwd=12345;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>(entity =>
            {
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
                    .HasConstraintName("FK__TimeSlots__Psych__4222D4EF");
            });

            modelBuilder.Entity<Token>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Role).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
