namespace Dal.Models;

public class MenuItem
{
    public int ID { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
    public int Amount { get; set; }
}