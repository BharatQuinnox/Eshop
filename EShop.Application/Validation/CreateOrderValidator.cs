using EShop.Application.Dto;
using FluentValidation;

namespace EShop.Application.Validation
{
    public class CreateOrderCommandValidator : AbstractValidator<EShop.Application.Dto.Order>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(order => order.CustomerId)
                .NotEmpty().WithMessage("Customer Id is required.")
                .GreaterThan(0).WithMessage("Customer Id must be greater than 0");

        }
    }
}
