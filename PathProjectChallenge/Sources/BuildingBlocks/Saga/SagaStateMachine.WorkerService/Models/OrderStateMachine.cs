using Automatonymous;
using EventBus.Shared.Events;
using EventBus.Shared.Events.Absracts;
using EventBus.Shared.Messages;
using EventBusRabbitMQ;
using MassTransit;

namespace SagaStateMachine.WorkerService.Models
{
    public class OrderStateMachine : MassTransitStateMachine<OrderStateInstance>
    {
        public Event<IOrderCreatedRequestEvent> OrderCreatedRequestEvent { get; set; }

        public Event<IAccountBalanceIsSufficientEvent> AccountBalanceIsSufficientEvent { get; set; }
        public Event<IAccountBalanceIsInsufficientEvent> AccountBalanceIsInsufficientEvent { get; set; }
        public Event<IPaymentCompletedEvent> PaymentCompletedEvent { get; set; }
        public Event<IPaymentFailedEvent> PaymentFailedEvent { get; set; }
        public State OrderCreated { get; private set; }

        public State AccountIsSufficient { get; private set; }

        public State AccountIsInsufficient { get; private set; }
        public State PaymentCompleted { get; private set; }
        public State PaymentFailed { get; private set; }
        public OrderStateMachine()
        {
            // Step 1
            InstanceState(x => x.CurrentState);

            // Step 2
            Event(() => OrderCreatedRequestEvent, y => y.CorrelateBy<Guid>(x => x.OrderId, z => z.Message.OrderId).SelectId(context => Guid.NewGuid()));

            Event(() => AccountBalanceIsSufficientEvent, x => x.CorrelateById(y => y.Message.CorrelationId));

            Event(() => AccountBalanceIsInsufficientEvent, x => x.CorrelateById(y => y.Message.CorrelationId));

            Event(() => PaymentCompletedEvent, x => x.CorrelateById(y => y.Message.CorrelationId));

            Event(() => PaymentFailedEvent, x => x.CorrelateById(y => y.Message.CorrelationId));

            // Step 3

            //.Publish(context =>
            //                         new OrderCreatedEvent(context.Instance.CorrelationId)
            //                         {
            //                             AccountId = context.Data.AccountId,
            //                             CurrencyId = context.Data.CurrencyId,
            //                             TotalAmount = context.Data.TotalAmount,
            //                             TotalPrice = context.Data.TotalPrice,
            //                             UnitPrice = context.Data.UnitPrice,
            //                             UserId = context.Data.UserId,
            //                         })
            Initially(
                When(OrderCreatedRequestEvent).
                ThenAsync(async context =>
                {
                    context.Instance.UserId = context.Data.UserId;
                    context.Instance.OrderId = context.Data.OrderId;
                    context.Instance.CreatedDate = DateTime.UtcNow;
                    context.Instance.AccountId = context.Data.AccountId;
                    context.Instance.CurrencyId = context.Data.CurrencyId;
                    context.Instance.UnitPrice = context.Data.UnitPrice;
                    context.Instance.TotalPrice = context.Data.TotalPrice;
                    context.Instance.TotalAmount = context.Data.TotalAmount;
                })
                .Then(context => { Console.WriteLine($"OrderCreatedRequestEvent Before : {context.Instance}"); })
                .PublishAsync(context => context.Init<IOrderCreatedEvent>(new OrderCreatedEvent(context.Instance.CorrelationId)
                {
                    AccountId = context.Data.AccountId,
                    CurrencyId = context.Data.CurrencyId,
                    TotalAmount = context.Data.TotalAmount,
                    TotalPrice = context.Data.TotalPrice,
                    UnitPrice = context.Data.UnitPrice,
                    UserId = context.Data.UserId,
                }))
                .TransitionTo(OrderCreated)
                .ThenAsync(async context => { Console.WriteLine($"OrderCreatedRequestEvent After : {context.Instance}"); }));

            During(OrderCreated,
                When(AccountBalanceIsSufficientEvent)
                .TransitionTo(AccountIsSufficient)
                .ThenAsync(async context => { Console.WriteLine($"AccountBalanceIsSufficientPaymentEvent Before : {context.Instance}"); })
                .SendAsync(new Uri($"queue:{RabbitMQSettingsConst.AccountBalanceIsSufficientPaymentRequestEvent}"), context => context.Init<IAccountBalanceIsSufficientPaymentRequestEvent>(
                    new AccountBalanceIsSufficientPaymentRequestEvent(context.Instance.CorrelationId)
                    {
                        AccountId = context.Data.AccountId,
                        TotalPrice = context.Data.TotalPrice,
                        UserId = context.Data.UserId,
                        CurrencyId = context.Data.CurrencyId
                    })
                   ).ThenAsync(async context => { Console.WriteLine($"AccountBalanceIsSufficientPaymentEvent After : {context.Instance}"); }),
                When(AccountBalanceIsInsufficientEvent)
                .TransitionTo(AccountIsInsufficient)
                .PublishAsync(context =>context.Init<IOrderAccountBalanceIsInsufficientRequestEvent>(new OrderAccountBalanceIsInsufficientRequestEvent() { OrderId = context.Instance.OrderId, Reason = context.Data.Reason }))
                .ThenAsync(async context => { Console.WriteLine($"OrderAccountBalanceIsInsufficientRequestEvent Before : {context.Instance}"); })
                );


            During(AccountIsSufficient,
                When(PaymentCompletedEvent)
                .TransitionTo(PaymentCompleted)
                .ThenAsync( async context => { Console.WriteLine($"OrderPaymentCompletedRequestEvent Before : {context.Instance}"); })
                .PublishAsync(context => context.Init<IOrderPaymentCompletedRequestEvent>( new OrderPaymentCompletedRequestEvent() { OrderId = context.Instance.OrderId }))
                .ThenAsync(async context => { Console.WriteLine($"OrderPaymentCompletedRequestEvent After : {context.Instance}"); })
                .Finalize(),
                When(PaymentFailedEvent)
                .ThenAsync(async context => { Console.WriteLine($"AccountBalanceRoleBackMessage Before : {context.Instance}"); })
                .PublishAsync(context =>context.Init<IOrderPaymentFailedRequestEvent>(new OrderPaymentFailedRequestEvent() { OrderId = context.Instance.OrderId, Reason = context.Data.Reason }))
                .SendAsync(new Uri($"queue:{RabbitMQSettingsConst.AccountBalanceRoleBackMessage}"), context => context.Init<IAccountBalanceRoleBackMessage>(
                 new AccountBalanceRoleBackMessage()
                 {
                     AccountId = context.Data.AccountId,
                     Balance = context.Data.Balance
                 }))
                .TransitionTo(PaymentFailed)
                   .ThenAsync(async context => { Console.WriteLine($"AccountBalanceRoleBackMessage After : {context.Instance}"); })
                );

            //SetCompletedWhenFinalized();


        }
    }
}
