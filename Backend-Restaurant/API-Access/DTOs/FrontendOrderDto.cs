namespace API_Access.DTOs
{
    public class FrontendOrderDto
    {
        public string orderId { get; set; }
        public string timestamp { get; set; }
        public string tableNumber { get; set; }
        public List<FrontendOrderItemDto> items { get; set; }
        public FrontendOrderSummaryDto summary { get; set; }
    }
}
