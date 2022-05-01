using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DeliveryApp.Models
{
    public partial class DeliveryDBContext : DbContext
    {
        public DeliveryDBContext()
        {
        }

        public DeliveryDBContext(DbContextOptions<DeliveryDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Pizza> Pizzas { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerEmail)
                    .HasName("PK__Customer__CE486A0B3B778F1C");

                entity.Property(e => e.CustomerEmail)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("customer_email");

                entity.Property(e => e.CustomerAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("customer_address");

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("customer_name");

                entity.Property(e => e.CustomerPhoneNumber)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("customer_phone_number");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId).HasColumnName("order_id").ValueGeneratedOnAdd();

                entity.Property(e => e.CustomerEmail)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("customer_email");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("order_date");

                entity.Property(e => e.OrderTotal)
                    .HasColumnType("decimal(8, 2)")
                    .HasColumnName("order_total");

                entity.HasOne(d => d.CustomerEmailNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerEmail)
                    .HasConstraintName("FK__Orders__customer__25869641");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("OrderItem");

                entity.Property(e => e.OrderItemId).HasColumnName("order_item_id").ValueGeneratedOnAdd();

                entity.Property(e => e.ItemCount).HasColumnName("item_count");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.OrderItemAmount)
                    .HasColumnType("decimal(8, 2)")
                    .HasColumnName("Order_item_amount");

                entity.Property(e => e.PizzaId).HasColumnName("pizza_id");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__OrderItem__order__2A4B4B5E");

                entity.HasOne(d => d.Pizza)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.PizzaId)
                    .HasConstraintName("FK__OrderItem__pizza__2B3F6F97");
            });

            modelBuilder.Entity<Pizza>(entity =>
            {
                entity.ToTable("Pizza");

                entity.Property(e => e.PizzaId).HasColumnName("pizza_id").ValueGeneratedOnAdd();

                entity.Property(e => e.PizzaAmount)
                    .HasColumnType("decimal(4, 2)")
                    .HasColumnName("pizza_amount");

                entity.Property(e => e.PizzaName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("pizza_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
