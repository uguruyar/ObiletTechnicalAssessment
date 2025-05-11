using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Repository.Interfaces;

namespace Middlewares;

public class ObiletSessionMiddleware
{
    private readonly RequestDelegate _next;

    public ObiletSessionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ISessionRepository sessionRepository, IObiletApiClient client)
    {
        if (sessionRepository.Get() == null)
        {
            var session = await client.GetSessionAsync();
            sessionRepository.Save(session);
        }

        await _next(context);
    }
}