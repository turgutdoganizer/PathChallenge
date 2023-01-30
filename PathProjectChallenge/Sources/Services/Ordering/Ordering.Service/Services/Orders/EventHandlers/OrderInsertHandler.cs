using EventBus.Shared.Events;
using EventBus.Shared.Events.Abstracts;
using EventBusRabbitMQ;
using MassTransit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Core.EventSourcing.Events.Orders;
using Ordering.Core.EventSourcing.Streams;
using Ordering.Core.Utilities.Results;
using Ordering.Core.Utilities.Results.Abstract;
using Ordering.Service.Mappers;
using Ordering.Service.Services.Orders.Commands;
using System.Net;

namespace Ordering.Service.Services.Orders.EventHandlers
{
    public class OrderInsertHandler : IRequestHandler<OrderInsertCommand, IResult>
    {

        private readonly IOrderStream _orderStream;
        //private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public OrderInsertHandler(IOrderStream orderStream, IServiceScopeFactory serviceScopeFactory)
        {
            _orderStream = orderStream;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task<IResult> Handle(OrderInsertCommand request, CancellationToken cancellationToken)
        {
            var orderInsertedEvent = OrderMapper.Mapper.Map<OrderInsertedEvent>(request.OrderInsertDto);
            _orderStream.Inserted(orderInsertedEvent);
            var eventId = Guid.NewGuid();
            var result = await _orderStream.InsertAsync(eventId);
            using var a = _serviceScopeFactory.CreateScope();
            var b = a.ServiceProvider.GetRequiredService<ISendEndpointProvider>();
            var sendEndPoint = await b.GetSendEndpoint(new Uri($"queue:{RabbitMQSettingsConst.OrderSaga}"));
            await sendEndPoint.Send<IOrderCreatedRequestEvent>(
                new OrderCreatedRequestEvent()
                {
                    UserId = request.OrderInsertDto.UserId,
                    CurrencyId = request.OrderInsertDto.CurrencyId,
                    OrderId = eventId,
                    TotalAmount = request.OrderInsertDto.TotalAmount,
                    TotalPrice = request.OrderInsertDto.TotalPrice,
                    AccountId = Guid.NewGuid(),
                    UnitPrice = request.OrderInsertDto.UnitPrice
                });
            return result ? new Result(HttpStatusCode.Created, "Order is created successfully") : new Result(HttpStatusCode.BadRequest, "Bad Request");
        }
    }
}
