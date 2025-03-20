namespace Dal.Models;

public class AdditionMenuItem
{
    public int ID { get; set; }
    public int AdditionsID { get; set; }
    public Addition Addition { get; set; }
    public int MenuItemID { get; set; }
    public MenuItem MenuItem { get; set; }
}