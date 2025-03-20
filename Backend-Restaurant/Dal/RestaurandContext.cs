using Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace Dal;

public class RestaurandContext : DbContext
{
    public DbSet<Table> Tables { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<Addition> Additions { get; set; }
    public DbSet<OrderMenuItem> OrderMenuItems { get; set; }
    public DbSet<AdditionMenuItem> AdditionMenuItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("YourConnectionStringHere");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderMenuItem>()
            .HasOne(o => o.Order)
            .WithMany()
            .HasForeignKey(o => o.OrderID);

        modelBuilder.Entity<OrderMenuItem>()
            .HasOne(m => m.MenuItem)
            .WithMany()
            .HasForeignKey(m => m.MenuItemID);

        modelBuilder.Entity<AdditionMenuItem>()
            .HasOne(a => a.Addition)
            .WithMany()
            .HasForeignKey(a => a.AdditionsID);

        modelBuilder.Entity<AdditionMenuItem>()
            .HasOne(m => m.MenuItem)
            .WithMany()
            .HasForeignKey(m => m.MenuItemID);
    }