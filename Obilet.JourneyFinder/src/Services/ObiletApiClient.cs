using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Models;
using Models.Configuration;
using Models.Exceptions;
using Models.Requests;
using Models.Responses;
using Services.Interfaces;

namespace Services;

public class ObiletApiClient : IObiletApiClient
{
    private readonly HttpClient _client;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<ObiletApiClient> _logger;

    public ObiletApiClient(
        HttpClient client,
        IHttpContextAccessor httpContextAccessor,
        ILogger<ObiletApiClient> logger,
        ObiletApiSettings settings)
    {
        _client = client;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
        _client.BaseAddress = new Uri(settings.BaseUrl);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", settings.ApiToken
        );
    }

    public async Task<SessionData> GetSessionAsync()
    {
        try
        {
            var ip = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "127.0.0.1";

            var request = new SessionRequest
            {
                Type = 1,
                Connection = new Connection { IpAddress = ip, Port = "5117" },
                Browser = new Browser { Name = "Chrome", Version = "47.0.0.12" }
            };

            var response = await _client.PostAsJsonAsync("api/client/getsession", request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadFromJsonAsync<SessionResponse>();

            if (content?.Data == null)
            {
                throw new ObiletApiException(
                    "Failed to get session data",
                    content?.Status ?? "Unknown",
                    content?.UserMessage ?? "Session data is null",
                    content?.ApiRequestId ?? "Unknown");
            }

            return new SessionData
            {
                SessionId = content.Data.SessionId,
                DeviceId = content.Data.DeviceId,
                CreatedAt = DateTime.UtcNow
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting session from Obilet API");
            throw;
        }
    }

    public async Task<T> CallObiletEndpoint<T>(string url, object body)
    {
        try
        {
            var response = await _client.PostAsJsonAsync(url, body);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (result is null)
            {
                throw new InvalidOperationException($"Deserialization failed. JSON: {json}");
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calling Obilet API endpoint: {Url}", url);
            throw;
        }
    }
}