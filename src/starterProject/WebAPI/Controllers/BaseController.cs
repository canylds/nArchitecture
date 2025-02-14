using Core.Security.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public class BaseController : ControllerBase
{
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()
        ?? throw new InvalidOperationException("IMediator cannot be retrieved from request services.");

    private IMediator? _mediator;

    protected string GetIpAddress()
    {
        string ipAddress = Request.Headers.ContainsKey("X-Forwarded-For")
            ? Request.Headers["X-Forwarded-For"].ToString()
            : HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString()
            ?? throw new InvalidOperationException("IP address cannot be retrieved from request.");

        return ipAddress;
    }

    protected int GetUserIdFromRequest()
    {
        int userId = Convert.ToInt32(HttpContext.User.GetIdClaim());

        return userId;
    }
}
