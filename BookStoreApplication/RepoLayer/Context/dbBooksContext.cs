using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RepoLayer.Context.Models;

namespace RepoLayer.Context
{
    public partial class dbBooksContext : DbContext 
    {
        public dbBooksContext()
        {
        }

        public dbBooksContext(DbContextOptions<dbBooksContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CartTable> CartTable { get; set; }
        public virtual DbSet<CustomerDetailsTable> CustomerDetailsTable { get; set; }
        public virtual DbSet<OrderTable> OrderTable { get; set; }
        public virtual DbSet<ProductTable> ProductTable { get; set; }
        public virtual DbSet<RegistrationTable> RegistrationTable { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=BookStoreConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartTable>(entity =>
            {
                entity.HasKey(e => e.CartId)
                    .HasName("PK__CartTabl__51BCD7B7ABC24635");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CartTable)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__CartTable__Produ__4222D4EF");

                entity.HasOne(d => d.Register)
                    .WithMany(p => p.CartTable)
                    .HasForeignKey(d => d.RegisterId)
                    .HasConstraintName("FK__CartTable__Regis__412EB0B6");
            });

            modelBuilder.Entity<CustomerDetailsTable>(entity =>
            {
                entity.HasKey(e => e.CustomerDetailId)
                    .HasName("PK__Customer__D04B369E8BECDACB");

                entity.Property(e => e.AddressType)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AreaBuilding)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PinCode)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Register)
                    .WithMany(p => p.CustomerDetailsTable)
                    .HasForeignKey(d => d.RegisterId)
                    .HasConstraintName("FK__CustomerD__Regis__3D5E1FD2");
            });

            modelBuilder.Entity<OrderTable>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__OrderTab__C3905BCFFB9B9C53");

                entity.Property(e => e.OrderTime).HasColumnType("datetime");

                entity.HasOne(d => d.CustomerDetail)
                    .WithMany(p => p.OrderTable)
                    .HasForeignKey(d => d.CustomerDetailId)
                    .HasConstraintName("FK__OrderTabl__Custo__44FF419A");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderTable)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__OrderTabl__Produ__45F365D3");
            });

            modelBuilder.Entity<ProductTable>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK__ProductT__B40CC6CD418606B1");

                entity.Property(e => e.Author).IsUnicode(false);

                entity.Property(e => e.BookName).IsUnicode(false);

                entity.Property(e => e.Descrption).IsUnicode(false);

                entity.Property(e => e.Discountprice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Image).IsUnicode(false);

                entity.Property(e => e.Language)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Register)
                    .WithMany(p => p.ProductTable)
                    .HasForeignKey(d => d.RegisterId)
                    .HasConstraintName("FK__ProductTa__Regis__3A81B327");
            });

            modelBuilder.Entity<RegistrationTable>(entity =>
            {
                entity.HasKey(e => e.RegisterId)
                    .HasName("PK__Registra__B91FAB792D5792BF");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Registra__A9D10534F20640A9")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TypeofRegister)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
