namespace EventBus.Shared.Messages
{
    public class PaymentMessage
    {
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string CVV { get; set; }
        public double TotalPrice { get; set; }
    }
}
