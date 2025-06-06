namespace API_Access.DTOs
{
    public class FrontendOrderItemDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public decimal unitPrice { get; set; }
        public List<FrontendModificationDto> modifications { get; set; }
        public string instructions { get; set; }
        public decimal itemTotal { get; set; }
    }
}
