using Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace Dal;

public class RestaurantDbContext : DbContext
{
    public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options)
            : base(options)
    {
    }


    public DbSet<Table> Tables { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<Addition> Additions { get; set; }
    public DbSet<OrderMenuItem> OrderMenuItems { get; set; }
    public DbSet<AdditionMenuItem> AdditionMenuItems { get; set; }
    public DbSet<Modification> Modifications { get; set; }
    public DbSet<ModificationMenuItem> ModificationMenuItems { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Order <--> OrderMenuItems
        modelBuilder.Entity<OrderMenuItem>()
            .HasOne(omi => omi.Order)
            .WithMany(o => o.OrderMenuItems)
            .HasForeignKey(omi => omi.OrderID);

        // OrderMenuItem <--> MenuItem
        modelBuilder.Entity<OrderMenuItem>()
            .HasOne(omi => omi.MenuItem)
            .WithMany(m => m.OrderMenuItems)
            .HasForeignKey(omi => omi.MenuItemID);

        // AdditionMenuItem <--> Addition
        modelBuilder.Entity<AdditionMenuItem>()
            .HasOne(ami => ami.Addition)
            .WithMany() // No navigation from Addition
            .HasForeignKey(ami => ami.AdditionsID);

        // AdditionMenuItem <--> MenuItem
        modelBuilder.Entity<AdditionMenuItem>()
            .HasOne(ami => ami.MenuItem)
            .WithMany() // No navigation from MenuItem to AdditionMenuItems
            .HasForeignKey(ami => ami.MenuItemID);

        // ModificationMenuItem <--> Modification
        modelBuilder.Entity<ModificationMenuItem>()
            .HasOne(mmi => mmi.Modification)
            .WithMany(m => m.ModificationMenuItems)
            .HasForeignKey(mmi => mmi.ModificationID);

        // ModificationMenuItem <--> MenuItem
        modelBuilder.Entity<ModificationMenuItem>()
            .HasOne(mmi => mmi.MenuItem)
            .WithMany(m => m.ModificationMenuItems)
            .HasForeignKey(mmi => mmi.MenuItemID);

        // IGNORE unwanted relation if EF still infers one
        modelBuilder.Entity<ModificationMenuItem>()
            .Ignore("OrderMenuItem"); // This line helps prevent EF from trying to use OrderMenuItemID if it still sees the nav somehow
    }


}