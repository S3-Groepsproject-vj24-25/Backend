using Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace Dal;

public class RestaurantDbContext : DbContext
{
    //Here you have to change the connection string.
    //The default if you set up a local db and named it Backend_Restaurant is: "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Backend_Restaurant".
    //Change if required.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(@"Server=localhost\SQLEXPRESS;Initial Catalog=Backend_Restaurant;Integrated Security=True;TrustServerCertificate=True;");



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
}