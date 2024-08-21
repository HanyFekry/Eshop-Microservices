namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand(OrderDto OrderDto) : ICommand<CreateOrderResult>;
    public record CreateOrderResult(Guid Id);

    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.OrderDto.CustomerId).NotEmpty().WithMessage("{PropertyName} is required.!");
            RuleFor(x => x.OrderDto.OrderName).NotEmpty().WithMessage("{PropertyName} is required.!");
            RuleFor(x => x.OrderDto.OrderItems).NotEmpty().WithMessage("{PropertyName} is required.!");
        }

    }
}
