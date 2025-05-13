using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Models;
using Models.Requests;
using Models.Responses;
using Services.Interfaces;

namespace Services;

public class ObiletApiClient : IObiletApiClient
{
    private readonly HttpClient _client;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly string _port = "5117";

    public ObiletApiClient(HttpClient client, IHttpContextAccessor httpContextAccessor)
    {
        _client = client;
        _httpContextAccessor = httpContextAccessor;
        // TODO :Get Url from Options
        _client.BaseAddress = new Uri("https://v2-api.obilet.com/");
        // TODO :Get Token value from Options
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Basic", "JEcYcEMyantZV095WVc3G2JtVjNZbWx1");
    }

    public async Task<SessionData> GetSessionAsync()
    {
        var ip = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "127.0.0.1";

        var request = new SessionRequest
        {
            Type = 1,
            Connection = new Connection { IpAddress = ip, Port = _port },
            Browser = new Browser { Name = "Chrome", Version = "47.0.0.12" }
        };

        var response = await _client.PostAsJsonAsync("api/client/getsession", request);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadFromJsonAsync<SessionResponse>();

        if (content is null || content.Data is null)
            throw new Exception("Session alınamadı.");

        return new SessionData
        {
            SessionId = content.Data.SessionId,
            DeviceId = content.Data.DeviceId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public async Task<T> CallObiletEndpoint<T>(string url, object body)
    {
        var response = await _client.PostAsJsonAsync(url, body);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (result is null)
            throw new InvalidOperationException($"Deserialization failed. JSON: {json}");

        return result;
    }
}