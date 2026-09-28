using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace TEcomerc.Application.Commands;

[SuppressMessage("CA", "CA1815", Justification = "Somente um command build")]
public readonly struct RepositoryCommandBuilderUpdate<TEntity>
{
    public RepositoryCommandBuilderUpdate<TEntity> Change<T>(Func<TEntity, T> _0, T to, [CallerArgumentExpression(nameof(_0))] string? fieldName = null)
    {
        ArgumentNullException.ThrowIfNull(fieldName);
        var lastExpressionDot = fieldName.LastIndexOf('.');
        ArgumentOutOfRangeException.ThrowIfLessThan(lastExpressionDot, 1);

        fieldName = fieldName[(lastExpressionDot+1)..];
        updatedEntries.Add(fieldName, to);

        return this;
    }

    public RepositoryCommandUpdate<TEntity> AsChanges() { return new(updatedEntries); }

    public RepositoryCommandBuilderUpdate() { updatedEntries = []; }
    readonly Dictionary<string, object?> updatedEntries;
}