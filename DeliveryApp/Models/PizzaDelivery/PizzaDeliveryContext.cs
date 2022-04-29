using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DeliveryApp.Models.PizzaDelivery
{
    public partial class PizzaDeliveryContext : DbContext
    {
        public PizzaDeliveryContext()
        {
        }

        public PizzaDeliveryContext(DbContextOptions<PizzaDeliveryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Pizza> Pizzas { get; set; }
        public virtual DbSet<PizzaOrder> PizzaOrders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(LocalDB)\\MSSQLLocalDB;Database=PizzaDelivery;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.CustomerAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("customer_address");

                entity.Property(e => e.CustomerEmail)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("customer_email");

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
                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("order_date");

                entity.Property(e => e.OrderTotal)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("order_total");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Orders__customer__25869641");
            });

            modelBuilder.Entity<Pizza>(entity =>
            {
                entity.ToTable("Pizza");

                entity.Property(e => e.PizzaId).HasColumnName("pizza_id");

                entity.Property(e => e.PizzaAmount)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("pizza_amount");

                entity.Property(e => e.PizzaName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("pizza_name");
            });

            modelBuilder.Entity<PizzaOrder>(entity =>
            {
                entity.HasKey(e => e.PizzaOrdersId)
                    .HasName("PK__PizzaOrd__9620AFE0C81F56FB");

                entity.Property(e => e.PizzaOrdersId).HasColumnName("pizza_orders_id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.PizzaId).HasColumnName("pizza_id");

                entity.Property(e => e.PizzaOrdersCount).HasColumnName("pizza_orders_count");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.PizzaOrders)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__PizzaOrde__order__2A4B4B5E");

                entity.HasOne(d => d.Pizza)
                    .WithMany(p => p.PizzaOrders)
                    .HasForeignKey(d => d.PizzaId)
                    .HasConstraintName("FK__PizzaOrde__pizza__2B3F6F97");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
