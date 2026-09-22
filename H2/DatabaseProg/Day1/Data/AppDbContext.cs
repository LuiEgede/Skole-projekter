using Day1.Models;
using Microsoft.EntityFrameworkCore;

namespace Day1.Data;

public class AppDbContext : DbContext
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Car> Cars => Set<Car>();
    public DbSet<WorkOrder> WorkOrders => Set<WorkOrder>();
    public DbSet<Part> Parts => Set<Part>();
    public DbSet<WorkOrderPart> WorkOrderParts => Set<WorkOrderPart>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=workshop.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId);
            entity.Property(e => e.CustomerName).IsRequired();
            entity.Property(e => e.Phone).IsRequired();
            entity.HasIndex(e => e.Email).IsUnique();
        });

        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.CarId);
            entity.Property(e => e.LicensePlate).IsRequired();
            entity.Property(e => e.Brand).IsRequired();
            entity.Property(e => e.Model).IsRequired();
            entity.Property(e => e.ManufactureYear).IsRequired();
            entity.HasIndex(e => e.LicensePlate).IsUnique();

            entity.HasOne(e => e.Customer)
                .WithMany(e => e.Cars)
                .HasForeignKey(e => e.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<WorkOrder>(entity =>
        {
            entity.HasKey(e => e.WorkOrderId);
            entity.Property(e => e.WorkDescription).IsRequired();
            entity.Property(e => e.WorkStatus).IsRequired();

            entity.HasOne(e => e.Car)
                .WithMany(e => e.WorkOrders)
                .HasForeignKey(e => e.CarId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Part>(entity =>
        {
            entity.HasKey(e => e.PartId);
            entity.Property(e => e.PartName).IsRequired();
            entity.Property(e => e.Price).IsRequired();
        });

        modelBuilder.Entity<WorkOrderPart>(entity =>
        {
            entity.HasKey(e => new { e.WorkOrderId, e.PartId });
            entity.Property(e => e.Quantity).IsRequired();

            entity.HasOne(e => e.WorkOrder)
                .WithMany(e => e.WorkOrderParts)
                .HasForeignKey(e => e.WorkOrderId);

            entity.HasOne(e => e.Part)
                .WithMany(e => e.WorkOrderParts)
                .HasForeignKey(e => e.PartId);
        });
    }
}