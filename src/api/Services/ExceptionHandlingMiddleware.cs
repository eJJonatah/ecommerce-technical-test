namespace TEcomerc.Api.Services;

using System.Net;
using TEcomerc.Domain.Exceptions;

sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (InvalidOperationException ex)
        {
            await WriteBadRequest(context, ex.Message);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            await WriteBadRequest(context, ex.Message);
        }
        catch (InvalidDomainParameterException ex)
        {
            await WriteBadRequest(context, ex.Message);
        }
    }

    private static async Task WriteBadRequest(
        HttpContext context,
        string message)
    {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsJsonAsync(new
        {
            error = message
        });
    }
}