using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace TEcomerc.Application.Observability;


#pragma warning disable S2326 // Essa é uma interface de proxy


public interface ILogger
{
    void Trace([ConstantExpected] string msg);
    void Trace(DefaultInterpolatedStringHandler msg);

    void Debug([ConstantExpected] string msg);
    void Debug(DefaultInterpolatedStringHandler msg);

    void Info([ConstantExpected] string msg);
    void Info(DefaultInterpolatedStringHandler msg);

    void Warn([ConstantExpected] string msg);
    void Warn(DefaultInterpolatedStringHandler msg);

    void LError([ConstantExpected] string msg);
    void LError(DefaultInterpolatedStringHandler msg);

    void Critical([ConstantExpected] string msg);
    void Critical(DefaultInterpolatedStringHandler msg);
}

public interface ILogger<TContext> : ILogger;