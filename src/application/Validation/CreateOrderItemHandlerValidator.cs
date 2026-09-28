using FluentValidation;
using TEcomerc.Domain.Entities;

namespace TEcomerc.Application.Validation;


public sealed class CreateOrderItemHandlerValidator : AbstractValidator<OrderItem>
{
    public CreateOrderItemHandlerValidator()
    {
        _= RuleFor(o => o.Id).NotEmpty().WithMessage("Não pode ser um valor nulo ou zero");
        _= RuleFor(o => o.OrderId).NotEmpty().WithMessage("Não pode ser um valor nulo ou zero");
        _= RuleFor(o => o.Quantity).GreaterThan(0).WithMessage("A quantitade não pode ser zero ou negativa");
        _= RuleFor(o => o.UnitPrice).GreaterThan(0).WithMessage("A quantitade não pode ser zero ou negativa");

        _= RuleFor(o => o.ProductName)
            .NotEmpty().WithMessage("O nome do produto não pode ser nulo ou vazio")
            .MaximumLength(128).WithMessage("O nome do produto não pode conter mais que 128 caracteres");
    }
}