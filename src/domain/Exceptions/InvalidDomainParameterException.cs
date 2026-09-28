#pragma warning disable S3925 // não é mais padrao aceito
#pragma warning disable CS1591 // documentações redundantes
#pragma warning disable CA1030 // não encaixa como evento

using System.Runtime.CompilerServices;
namespace TEcomerc.Domain.Exceptions;

/// <summary>Excessão simples levantada quando parâmetros intrinsecamente inválidos são fornecidos</summary>
[Serializable] public class InvalidDomainParameterException : Exception
{
    public required string ParameterExpression { get; init; }
    public required string Violation { get; init; }

    public InvalidDomainParameterException() { }
    public InvalidDomainParameterException(string message) : base(message) { }
    public InvalidDomainParameterException(string message, Exception inner) : base(message, inner) { }

    #pragma warning disable IDE0060, RCS1163

    public static void RaiseAt<T>( T _0, Predicate<T> IF, string? msg = null,
        [CallerArgumentExpression(nameof(_0))] string? parameterExpression = null)
    {
#pragma warning disable CA1062 // Validate arguments of public methods
        if (!IF(_0)) { return; }
#pragma warning restore CA1062

        throw new InvalidDomainParameterException()
        {
            ParameterExpression = parameterExpression!,
            Violation = msg!
        };
    }
}