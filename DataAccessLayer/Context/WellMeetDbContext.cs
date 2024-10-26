using System;
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

        public virtual DbSet<Appointment> Appointments { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Psychiatrist> Psychiatrists { get; set; } = null!;
        public virtual DbSet<TimeSlot> TimeSlots { get; set; } = null!;
        public virtual DbSet<Token> Tokens { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=localhost;database=WellMeetDb;uid=sa;pwd=12345;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("Appointment");

                entity.Property(e => e.Reason).HasMaxLength(255);

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.PaymentId)
                    .HasConstraintName("FK_Appointment_Payment");

                entity.HasOne(d => d.Psychiatrist)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.PsychiatristId)
                    .HasConstraintName("FK_Booking_Psychiatrist_PsychiatristId");

                entity.HasOne(d => d.TimeSlot)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.TimeSlotId)
                    .HasConstraintName("FK_Appointment_TimeSlot");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Booking_Users_UserId");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentStatus).HasMaxLength(50);

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.AppointmentId)
                    .HasConstraintName("FK_Payment_Appointment");
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
                entity.Property(e => e.SlotDate).HasColumnType("date");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.Psychiatrist)
                    .WithMany(p => p.TimeSlots)
                    .HasForeignKey(d => d.PsychiatristId)
                    .HasConstraintName("FK__TimeSlots__Psych__59063A47");
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
