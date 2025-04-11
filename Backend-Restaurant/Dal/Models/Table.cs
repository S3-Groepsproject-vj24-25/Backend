namespace Dal.Models;

public class Table
{
    public int ID { get; set; }
    public int TableNumber { get; set; }
    public DateTime Date { get; set; }
    public decimal TotalCost { get; set; }
    public bool Active { get; set; }
}