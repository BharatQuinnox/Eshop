using System;
using System.Collections.Generic;
using System.Linq;
using EShop.Application.Command;
using EShop.Application.Dto;
using FluentValidation;

namespace EShop.Application.Validation
{
    public class CreateShippingCommandValidator : AbstractValidator<Shipping>
    {
        public CreateShippingCommandValidator()
        {
            RuleFor(command => command.CustomerId)
                .GreaterThan(0).WithMessage("CustomerId must be greater than 0");

            RuleFor(command => command.AddressLine1)
                .NotEmpty().WithMessage("AddressLine1 is required")
                .MaximumLength(100).WithMessage("AddressLine1 cannot exceed 100 characters");

            RuleFor(command => command.AddressLine2)
                .MaximumLength(100).WithMessage("AddressLine2 cannot exceed 100 characters");

            RuleFor(command => command.City)
                .NotEmpty().WithMessage("City is required")
                .MaximumLength(50).WithMessage("City cannot exceed 50 characters");

            RuleFor(command => command.Country)
                .NotEmpty().WithMessage("Country is required")
                .MaximumLength(50).WithMessage("Country cannot exceed 50 characters");

            RuleFor(command => command.ZipCode)
                .NotEmpty().WithMessage("ZipCode is required")
                .MaximumLength(20).WithMessage("ZipCode cannot exceed 20 characters");

            RuleFor(command => command.IsActive)
                .NotNull().WithMessage("IsActive is required");
        }
    }

}
