using AutoMapper;
using EventBus.Shared.Events;
using Ordering.Core.Entities;
using Ordering.Core.EventSourcing.Events.Orders;
using Ordering.Dto.Orders;

namespace Ordering.Service.Services.Orders.Mappers
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            // For Writing to Event Store
            CreateMap<OrderInsertDto, OrderInsertedEvent>().ReverseMap();
            CreateMap<OrderUpdateDto, OrderUpdatedEvent>().ReverseMap();
            CreateMap<OrderDeleteDto, OrderDeletedEvent>().ReverseMap();


            //  For Writing to Sql Server
            CreateMap<OrderInsertedEvent, Order>().ReverseMap();
            CreateMap<OrderUpdatedEvent, Order>().ReverseMap();
            CreateMap<OrderDeletedEvent, Order>().ReverseMap();


            // For Reading from Sql Server
            CreateMap<Order, OrderDto>().ReverseMap();




            // For Sending Event to EventBus
            CreateMap<OrderInsertedEvent, OrderCreatedRequestEvent>().ReverseMap();
        }
    }
}
