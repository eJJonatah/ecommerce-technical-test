#pragma warning disable CA2254 // Template should be a static expression

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace TEcomerc.Api.Services;

// essa classe foi implementada precariamente (com óbvias perdas de performance) somente para cumprir o
// requisito de logging

public sealed class SerilogLoggerProxy<TCategory> : ILogger<TCategory>, Application.Observability.ILogger<TCategory>
{
    readonly ILogger<TCategory>? logger;

#pragma warning disable S6672 // Generic logger injection should match enclosing type
    public SerilogLoggerProxy(ILogger<TCategory>? logger = null)
#pragma warning restore S6672 // Generic logger injection should match enclosing type
    {
        this.logger = logger;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return logger?.BeginScope(state);
    }

    public void Critical([ConstantExpected] string msg) { logger?.LogCritical(msg); }

    public void Critical(DefaultInterpolatedStringHandler msg) { logger?.LogCritical(msg.ToStringAndClear()); }

    public void Debug([ConstantExpected] string msg) { logger?.LogDebug(msg); }

    public void Debug(DefaultInterpolatedStringHandler msg) { logger?.LogDebug(msg.ToStringAndClear()); }

    public void Info([ConstantExpected] string msg) { logger?.LogInformation(msg) ; }

    public void Info(DefaultInterpolatedStringHandler msg) { logger?.LogInformation(msg.ToStringAndClear()); }

    public bool IsEnabled(LogLevel logLevel) { return logger?.IsEnabled(logLevel) ?? false; }

    public void LError([ConstantExpected] string msg) { logger?.LogError(msg); }

    public void LError(DefaultInterpolatedStringHandler msg) { logger?.LogError(msg.ToStringAndClear()); }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        logger?.Log(logLevel, eventId, state, exception, formatter);
    }

    public void Trace([ConstantExpected] string msg) { logger?.LogTrace(msg); }

    public void Trace(DefaultInterpolatedStringHandler msg) { logger?.LogTrace(msg.ToStringAndClear()); }

    public void Warn([ConstantExpected] string msg) { logger?.LogWarning(msg);}

    public void Warn(DefaultInterpolatedStringHandler msg) { logger?.LogWarning(msg.ToStringAndClear());}
}

