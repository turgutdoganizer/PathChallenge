using MediatR;
using Ordering.Core.Utilities.Results.Abstract;
using Ordering.Dto.Orders;

namespace Ordering.Service.Services.Orders.Commands
{
    public class OrderUpdateCommand : IRequest<IResult>
    {
        public OrderUpdateDto OrderUpdateDto { get; set; }
    }
}
