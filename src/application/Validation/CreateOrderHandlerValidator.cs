using FluentValidation;
using TEcomerc.Domain.Entities;

namespace TEcomerc.Application.Validation;


public sealed class CreateOrderHandlerValidator : AbstractValidator<Order<OrderItem>>
{
    public CreateOrderHandlerValidator()
    {
        _= RuleFor(o => o.Id).NotEmpty().WithMessage("Não pode ser um valor nulo ou zero");
        _= RuleFor(o => o.CustomerId).NotEmpty().WithMessage("Não pode ser um valor nulo ou zero");
        _= RuleFor(o => o.CreatedAt).NotEmpty().WithMessage("Não pode ser um valor nulo ou zero");

        // por que a validação dos items.count não está aqui? Pois não queremos
        // que o mediator de validação automática cause consultas no banco ou
        // force uma enumeração que não temos controle/certeza a respeito ainda


    }
}