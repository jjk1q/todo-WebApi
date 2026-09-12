using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,Exception exception,CancellationToken cancellationToken)
    {
        var problemDetails = exception switch
        {
            ArgumentException => new ProblemDetails
            {
                Status = 400,
                Title = "Невалидные данные",
                Detail = exception.Message
            },
            _ => new ProblemDetails
            {
                Status = 500,
                Title = "Ошибка сервера",
                Detail = "Что-то пошло не так, попробуйте позже"
            }
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        Console.WriteLine(exception);
        return true;
        
    }
}