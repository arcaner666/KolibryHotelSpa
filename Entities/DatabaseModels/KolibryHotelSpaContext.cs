using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Entities.DatabaseModels
{
    public partial class KolibryHotelSpaContext : DbContext
    {
        public KolibryHotelSpaContext()
        {
        }

        public KolibryHotelSpaContext(DbContextOptions<KolibryHotelSpaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Claim> Claims { get; set; } = null!;
        public virtual DbSet<Currency> Currencies { get; set; } = null!;
        public virtual DbSet<Invoice> Invoices { get; set; } = null!;
        public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; } = null!;
        public virtual DbSet<InvoiceType> InvoiceTypes { get; set; } = null!;
        public virtual DbSet<PaymentType> PaymentTypes { get; set; } = null!;
        public virtual DbSet<Person> People { get; set; } = null!;
        public virtual DbSet<PersonClaim> PersonClaims { get; set; } = null!;
        public virtual DbSet<Reservation> Reservations { get; set; } = null!;
        public virtual DbSet<Suite> Suites { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=work\\sqlserver;Database=KolibryHotelSpa;User Id=sa;Password=Candiltos96.;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Claim>(entity =>
            {
                entity.ToTable("Claim");

                entity.Property(e => e.Title).HasMaxLength(500);
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("Currency");

                entity.Property(e => e.CurrencyId).ValueGeneratedOnAdd();

                entity.Property(e => e.CurrencySymbol).HasMaxLength(5);

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("Invoice");

                entity.Property(e => e.BuyerEmail).HasMaxLength(50);

                entity.Property(e => e.BuyerNameSurname).HasMaxLength(100);

                entity.Property(e => e.BuyerPhone).HasMaxLength(50);

                entity.Property(e => e.NetPrice).HasColumnType("smallmoney");

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TotalPrice).HasColumnType("smallmoney");

                entity.Property(e => e.TotalVat).HasColumnType("smallmoney");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Invoice__Currenc__35BCFE0A");

                entity.HasOne(d => d.InvoiceType)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.InvoiceTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Invoice__Invoice__33D4B598");

                entity.HasOne(d => d.PaymentType)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.PaymentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Invoice__Payment__34C8D9D1");
            });

            modelBuilder.Entity<InvoiceDetail>(entity =>
            {
                entity.ToTable("InvoiceDetail");

                entity.Property(e => e.Price).HasColumnType("smallmoney");

                entity.Property(e => e.TotalPrice).HasColumnType("smallmoney");

                entity.Property(e => e.TotalVat).HasColumnType("smallmoney");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.InvoiceDetails)
                    .HasForeignKey(d => d.InvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__InvoiceDe__Invoi__38996AB5");

                entity.HasOne(d => d.Suite)
                    .WithMany(p => p.InvoiceDetails)
                    .HasForeignKey(d => d.SuiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__InvoiceDe__Suite__398D8EEE");
            });

            modelBuilder.Entity<InvoiceType>(entity =>
            {
                entity.ToTable("InvoiceType");

                entity.Property(e => e.InvoiceTypeId).ValueGeneratedOnAdd();

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<PaymentType>(entity =>
            {
                entity.ToTable("PaymentType");

                entity.Property(e => e.PaymentTypeId).ValueGeneratedOnAdd();

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.PasswordHash).HasMaxLength(500);

                entity.Property(e => e.PasswordSalt).HasMaxLength(500);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.RefreshToken).HasMaxLength(500);

                entity.Property(e => e.Role).HasMaxLength(50);
            });

            modelBuilder.Entity<PersonClaim>(entity =>
            {
                entity.ToTable("PersonClaim");

                entity.HasOne(d => d.Claim)
                    .WithMany(p => p.PersonClaims)
                    .HasForeignKey(d => d.ClaimId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PersonCla__Claim__2D27B809");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonClaims)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PersonCla__Perso__2C3393D0");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.ToTable("Reservation");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.InvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Reservati__Invoi__3C69FB99");
            });

            modelBuilder.Entity<Suite>(entity =>
            {
                entity.ToTable("Suite");

                entity.Property(e => e.Price).HasColumnType("smallmoney");

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TotalPrice).HasColumnType("smallmoney");

                entity.Property(e => e.TotalVat).HasColumnType("smallmoney");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
