using Microsoft.EntityFrameworkCore.ChangeTracking;
using TEcomerc.Application.Commands;

namespace TEcomerc.Infraestructure.Sqlite;

static class PropertyEntityExtensions
{
    public static void Apply<TEntity, T>(this PropertyEntry<TEntity, T> prop, UpdateValue<T>? possibleChanges) where TEntity : class
    {
        if (possibleChanges is not {} changes) { return; }
        prop.CurrentValue = changes.NewValue;
    }
}