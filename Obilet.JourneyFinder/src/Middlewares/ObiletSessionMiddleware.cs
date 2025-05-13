using Microsoft.AspNetCore.Http;
using Models;
using Models.Base;
using Repository.Interfaces;
using Services.Interfaces;

namespace Middlewares;

public class ObiletSessionMiddleware
{
    private readonly RequestDelegate _next;

    public ObiletSessionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IMemoryCacheProvider memoryCacheProvider,
        IObiletApiClient client)
    {
        if (memoryCacheProvider.Get<SessionData>(Constants.ObiletSessionKey) == null)
        {
            var session = await client.GetSessionAsync();
            memoryCacheProvider.Save(Constants.ObiletSessionKey, session);
        }

        await _next(context);
    }
}