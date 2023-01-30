namespace EventBus.Shared.Messages
{
    public class OrderItemMessage
    {
        public Guid CurrencyId { get; set; }  
        public decimal Count { get; set; }   
    }
}
