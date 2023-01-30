using FluentValidation;
using Ordering.Service.Services.Orders.Commands;

namespace Ordering.Service.Services.Orders.Validators
{
    public class OrderDeleteValidator : AbstractValidator<OrderDeleteCommand>
    {
        public OrderDeleteValidator()
        {
            RuleFor(x => x.OrderDeleteDto.Id).NotEmpty().NotNull();
        }
    }
}
