namespace Dal.Models;

public class MenuItem
{
    public int ID { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
    public int Amount { get; set; }


    public ICollection<OrderMenuItem> OrderMenuItems { get; set; }
    public ICollection<ModificationMenuItem> ModificationMenuItems { get; set; }
    public ICollection<AdditionMenuItem> AdditionMenuItems { get; set; }
}