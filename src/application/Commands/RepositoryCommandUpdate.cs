using System.Diagnostics;
using System.Runtime.CompilerServices;
using TEcomerc.Domain.Exceptions;

namespace TEcomerc.Application.Commands;

public readonly record struct UpdateValue<T>(T NewValue);

public sealed class RepositoryCommandUpdate<TEntity>
{
    public UpdateValue<T>? GetUpdateValorFor<T>(Func<TEntity, T> _0, [CallerArgumentExpression(nameof(_0))] string? fieldName = null)
    {
        ArgumentNullException.ThrowIfNull(fieldName);
        var lastExpressionDot = fieldName.LastIndexOf('.');
        ArgumentOutOfRangeException.ThrowIfLessThan(lastExpressionDot, 1);

        fieldName = fieldName[(lastExpressionDot+1)..];

        if (!updatedFieldsVsValues.TryGetValue(fieldName, out object? boxedValue)) { return null; }
        if (boxedValue is not T unboxed)
        {
            InvalidDomainParameterException.RaiseAt(fieldName, _=>true, "o valor fornecido atualizado não é do mesmo tipo do campo");
            throw new UnreachableException();
        }

        return new(unboxed);
    }

    public RepositoryCommandUpdate(IReadOnlyDictionary<string, object?> updatedFieldsVsValues)
    {
        this.updatedFieldsVsValues = updatedFieldsVsValues;
    }

    readonly IReadOnlyDictionary<string, object?> updatedFieldsVsValues;
}