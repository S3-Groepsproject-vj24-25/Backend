namespace Dal.Models;

public class Modification
{
    public int ID { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public ICollection<ModificationMenuItem> ModificationMenuItems { get; set; }
}