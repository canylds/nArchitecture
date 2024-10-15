using System.Net.Mime;
using System.Text.Json;
using Core.CrossCuttingConcerns.Exception.Handlers;
using Core.CrossCuttingConcerns.Logging;
using Microsoft.AspNetCore.Http;

namespace Core.CrossCuttingConcerns.Exception;

public class ExceptionMiddleware
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly HttpExceptionHandler _httpExceptionHandler;
    private readonly ILogger _loggerService;
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next, IHttpContextAccessor contextAccessor, ILogger loggerService)
    {
        _next = next;
        _contextAccessor = contextAccessor;
        _loggerService = loggerService;
        _httpExceptionHandler = new HttpExceptionHandler();
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (System.Exception exception)
        {
            await LogException(context, exception);
            await HandleExceptionAsync(context.Response, exception);
        }
    }

    protected virtual Task HandleExceptionAsync(HttpResponse response, dynamic exception)
    {
        response.ContentType = MediaTypeNames.Application.Json;

        _httpExceptionHandler.Response = response;

        return _httpExceptionHandler.HandleExceptionAsync(exception);
    }

    protected virtual Task LogException(HttpContext context, System.Exception exception)
    {
        List<LogParameter> logParameters = [new LogParameter { Type = context.GetType().Name, Value = exception.ToString() }];

        LogDetail logDetail = new()
        {
            MethodName = _next.Method.Name,
            Parameters = logParameters,
            User = _contextAccessor.HttpContext.User.Identity?.Name ?? "?"
        };

        _loggerService.Error(JsonSerializer.Serialize(logDetail));

        return Task.CompletedTask;
    }
}
