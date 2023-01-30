using MediatR;
using Ordering.Core.Utilities.Results.Abstract;

namespace Ordering.Service.Services.Orders.Queries
{
    public class OrderListQuery : IRequest<IResult>
    {
    }
}
