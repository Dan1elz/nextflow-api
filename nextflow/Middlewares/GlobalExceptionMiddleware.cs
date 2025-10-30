using System.Net;
using System.Text.Json;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Exceptions;

namespace Nextflow.Middlewares;

public class GlobalExceptionMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }
    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var response = context.Response;
        response.ContentType = "application/json";

        int statusCode;
        string message;
        object? errors = null;

        switch (ex)
        {
            case BadRequestException:
                statusCode = (int)HttpStatusCode.BadRequest; // 400
                message = ex.Message;
                break;
            case NotAuthorizedException:
                statusCode = (int)HttpStatusCode.Unauthorized; // 401
                message = ex.Message;
                break;
            case NotFoundException:
                statusCode = (int)HttpStatusCode.NotFound; // 404
                message = ex.Message;
                break;
            case NextflowValidationException validationEx:
                statusCode = (int)HttpStatusCode.UnprocessableEntity; // 422
                message = "Erros de validação.";
                errors = validationEx.Errors;
                break;
            default:
                statusCode = (int)HttpStatusCode.InternalServerError; // 500
                message = "Erro interno do servidor. Contate o suporte.";
                Console.WriteLine(ex.Message);
                break;
        }
        response.StatusCode = statusCode;
        var result = JsonSerializer.Serialize(new ApiResponseMessage
        {
            Status = statusCode,
            Message = message,
            Errors = errors as List<string>
        });

        return response.WriteAsync(result);
    }
}

