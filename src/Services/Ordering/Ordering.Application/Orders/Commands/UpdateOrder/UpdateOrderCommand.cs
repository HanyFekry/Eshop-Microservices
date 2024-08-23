
namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public record UpdateOrderCommand(OrderDto Order) : ICommand<UpdateOrderResult>;

    public record UpdateOrderResult(bool Success);

    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(x => x.Order.Id).NotEmpty().WithMessage("{PropertyName is required.");
            RuleFor(x => x.Order.CustomerId).NotEmpty().WithMessage("{PropertyName is required.");
            RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("{PropertyName is required.");
            RuleFor(x => x.Order.OrderItems).NotEmpty().WithMessage("{PropertyName is required.");
        }
    }
}
