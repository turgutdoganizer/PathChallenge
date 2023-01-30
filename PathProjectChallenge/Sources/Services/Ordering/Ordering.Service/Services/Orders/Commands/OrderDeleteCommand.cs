using MediatR;
using Ordering.Core.Utilities.Results.Abstract;
using Ordering.Dto.Orders;

namespace Ordering.Service.Services.Orders.Commands
{
    public class OrderDeleteCommand : IRequest<IResult>
    {
        public OrderDeleteDto OrderDeleteDto { get; set; }
    }
}
