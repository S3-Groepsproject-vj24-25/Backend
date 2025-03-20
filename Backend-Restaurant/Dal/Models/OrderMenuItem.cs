namespace Dal.Models;

public class OrderMenuItem
{
    public int ID { get; set; }
    public int OrderID { get; set; }
    public Order Order { get; set; }
    public int MenuItemID { get; set; }
    public MenuItem MenuItem { get; set; }
    public int Amount { get; set; }
    public string Notes { get; set; }
}