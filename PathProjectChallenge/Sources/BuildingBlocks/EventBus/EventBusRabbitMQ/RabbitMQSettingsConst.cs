namespace EventBusRabbitMQ
{
    public class RabbitMQSettingsConst
    {
        public const string OrderSaga = "order-saga-queue";

        public const string AccountBalanceIsSufficientEvent = "account-balance-is-sufficient-queue";
        public const string AccountBalanceIsInsufficientEvent = "account-balance-is-insufficient-queue";
        public const string AccountOrderCreatedEvent = "account-order-created-queue";
        public const string AccountBalanceIsSufficientPaymentRequestEvent = "account-balance-is-sufficient-payment-request-queue";

        public const string AccountBalanceRoleBackMessage = "account-balance-role-back-message-queue";

        public const string PaymentIsCompletedEvent = "payment-is-completed-queue";
        public const string PaymentIsNotCompletedEvent = "payment-is-not-completed-queue";
        public const string PaymentAccountBalanceIsSufficientEvent = "payment-account-balance-is-sufficient-event-queue";


        public const string OrderPaymentCompletedRequestEvent = "order-payment-completed-request-event-queue";
        public const string OrderPaymentFailedRequestEvent = "order-payment-failed-request-event-queue";
        public const string OrderAccountBalanceIsInsufficientRequestEvent = "order-account-balance-is-insufficient-request-event-queue";



    }
}
