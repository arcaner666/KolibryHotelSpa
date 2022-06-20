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

        public virtual DbSet<Claim> Claims { get; set; }
        public virtual DbSet<ContactForm> ContactForms { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<PersonClaim> PersonClaims { get; set; }
        public virtual DbSet<Suite> Suites { get; set; }

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

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<ContactForm>(entity =>
            {
                entity.ToTable("ContactForm");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.NameSurname)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("Currency");

                entity.Property(e => e.CurrencyId).ValueGeneratedOnAdd();

                entity.Property(e => e.CurrencySymbol)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.ExchangeRate).HasColumnType("smallmoney");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("Invoice");

                entity.Property(e => e.BuyerEmail)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.BuyerNameSurname)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.BuyerPhone)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NetPrice).HasColumnType("smallmoney");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TotalPrice).HasColumnType("smallmoney");

                entity.Property(e => e.TotalVat).HasColumnType("smallmoney");

                entity.Property(e => e.Vat).HasColumnType("smallmoney");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Invoice__Currenc__300424B4");
            });

            modelBuilder.Entity<InvoiceDetail>(entity =>
            {
                entity.ToTable("InvoiceDetail");

                entity.Property(e => e.Price).HasColumnType("smallmoney");

                entity.Property(e => e.TotalPrice).HasColumnType("smallmoney");

                entity.Property(e => e.TotalVat).HasColumnType("smallmoney");

                entity.Property(e => e.Vat).HasColumnType("smallmoney");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.InvoiceDetails)
                    .HasForeignKey(d => d.InvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__InvoiceDe__Invoi__32E0915F");

                entity.HasOne(d => d.Suite)
                    .WithMany(p => p.InvoiceDetails)
                    .HasForeignKey(d => d.SuiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__InvoiceDe__Suite__33D4B598");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RefreshToken)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PersonClaim>(entity =>
            {
                entity.ToTable("PersonClaim");

                entity.HasOne(d => d.Claim)
                    .WithMany(p => p.PersonClaims)
                    .HasForeignKey(d => d.ClaimId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PersonCla__Claim__29572725");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonClaims)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PersonCla__Perso__286302EC");
            });

            modelBuilder.Entity<Suite>(entity =>
            {
                entity.ToTable("Suite");

                entity.Property(e => e.Price).HasColumnType("smallmoney");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TotalPrice).HasColumnType("smallmoney");

                entity.Property(e => e.TotalVat).HasColumnType("smallmoney");

                entity.Property(e => e.Vat).HasColumnType("smallmoney");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
