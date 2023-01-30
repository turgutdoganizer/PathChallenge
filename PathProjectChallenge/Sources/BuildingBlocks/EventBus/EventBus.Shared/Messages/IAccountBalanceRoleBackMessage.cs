namespace EventBus.Shared.Messages
{
    public interface IAccountBalanceRoleBackMessage
    {
        public Guid AccountId { get; set; }

        public double Balance { get; set; }
    }
}
