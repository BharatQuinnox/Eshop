using EShop.Application.Command;
using EShop.Application.Dto;
using EShop.Core.Entities;
using FluentValidation;

namespace EShop.Application.Validation
{
    public class CreateCartRequestCommandValidator : AbstractValidator<Dto.Cart>
    {
        public CreateCartRequestCommandValidator()
        {
            RuleFor(request => request.CustomerId)
                .GreaterThan(0).WithMessage("Customer Id must be greater than 0");

            RuleFor(request => request.Items)
                .NotNull().WithMessage("Items cannot be null")
                .NotEmpty().WithMessage("Items cannot be empty");

            RuleForEach(request => request.Items)
                .SetValidator(new CartItemDtoValidator());
        }

        public class CartItemDtoValidator : AbstractValidator<Dto.CartItem>
        {
            public CartItemDtoValidator()
            {
                RuleFor(item => item.Type)
                     .InclusiveBetween(1,2).WithMessage("Should be 1 or 2");

                RuleFor(item => item.Quantity)
                    .GreaterThan(0).WithMessage("Quantity must be greater than 0");

                RuleFor(item => item.ProductId)
                    .GreaterThan(0).WithMessage("ProductId must be greater than 0");

               
            }
        }
    }
}
