using Microsoft.EntityFrameworkCore;
using OrderLookup.API.Models;

namespace OrderLookup.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<StatusHistory> StatusHistories => Set<StatusHistory>();
    public DbSet<ShippingInfo> ShippingInfos => Set<ShippingInfo>();
    public DbSet<PaymentDetail> PaymentDetails => Set<PaymentDetail>();
    public DbSet<CustomerServiceNote> CustomerServiceNotes => Set<CustomerServiceNote>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Order>()
            .HasIndex(o => o.OrderNumber)
            .IsUnique();

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId);
        
        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.Items)
            .HasForeignKey(oi => oi.OrderId);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Product)
            .WithMany()
            .HasForeignKey(oi => oi.ProductId);
        
        modelBuilder.Entity<StatusHistory>()
            .HasOne(sh => sh.Order)
            .WithMany(o => o.StatusHistory)
            .HasForeignKey(sh => sh.OrderId);
        
        modelBuilder.Entity<ShippingInfo>()
            .HasOne(si => si.Order)
            .WithOne(o => o.ShippingInfo)
            .HasForeignKey<ShippingInfo>(si => si.OrderId);
        
        modelBuilder.Entity<PaymentDetail>()
            .HasOne(pd => pd.Order)
            .WithOne(o => o.PaymentDetail)
            .HasForeignKey<PaymentDetail>(pd => pd.OrderId);
        
        modelBuilder.Entity<CustomerServiceNote>()
            .HasOne(csn => csn.Order)
            .WithMany(o => o.ServiceNotes)
            .HasForeignKey(csn => csn.OrderId);
        
        modelBuilder.Entity<Address>()
            .HasOne(a => a.Customer)
            .WithMany(c => c.Addresses)
            .HasForeignKey(a => a.CustomerId);
        
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();
        
        modelBuilder.Entity<Order>()
            .Property(o => o.Subtotal).HasPrecision(18, 2);
        modelBuilder.Entity<Order>()
            .Property(o => o.TaxAmount).HasPrecision(18, 2);
        modelBuilder.Entity<Order>()
            .Property(o => o.ShippingCost).HasPrecision(18, 2);
        modelBuilder.Entity<Order>()
            .Property(o => o.DiscountAmount).HasPrecision(18, 2);
        modelBuilder.Entity<Order>()
            .Property(o => o.TotalAmount).HasPrecision(18, 2);

        modelBuilder.Entity<OrderItem>()
            .Property(oi => oi.UnitPrice).HasPrecision(18, 2);
        modelBuilder.Entity<OrderItem>()
            .Property(oi => oi.DiscountPercent).HasPrecision(5, 2);
        modelBuilder.Entity<OrderItem>()
            .Property(oi => oi.LineTotal).HasPrecision(18, 2);

        modelBuilder.Entity<Product>()
            .Property(p => p.Price).HasPrecision(18, 2);
        modelBuilder.Entity<Product>()
            .Property(p => p.Weight).HasPrecision(10, 2);

        modelBuilder.Entity<ShippingInfo>()
            .Property(si => si.ShippingWeight).HasPrecision(10, 2);

        modelBuilder.Entity<PaymentDetail>()
            .Property(pd => pd.AmountPaid).HasPrecision(18, 2);
        modelBuilder.Entity<PaymentDetail>()
            .Property(pd => pd.RefundedAmount).HasPrecision(18, 2);
    }
}
