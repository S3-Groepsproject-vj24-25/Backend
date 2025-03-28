namespace Dal.Models;

public class ModificationMenuItem
{
    public int ID { get; set; }
    public int ModificationID { get; set; }
    public Modification Modification { get; set; }
    public int MenuItemID { get; set; }
    public MenuItem MenuItem { get; set; }
    public int Amount { get; set; }
}