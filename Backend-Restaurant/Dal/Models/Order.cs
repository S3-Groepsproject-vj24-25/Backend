namespace Dal.Models;

public class Order
{
    public int ID { get; set; }
    public int TableID { get; set; }
    public bool Paid { get; set; }
    public decimal TotalCost { get; set; }
    public DateTime Timestamp { get; set; }
    public Table Table { get; set; }
    public List<OrderItem> Items { get; set; }

    public ICollection<OrderMenuItem> OrderMenuItems { get; set; }
}