namespace Models.Exceptions;

public class ObiletApiException(string message, string status, string userMessage, string apiRequestId)
    : Exception(message)
{
    public string Status { get; } = status;
    public string UserMessage { get; } = userMessage;
    public string ApiRequestId { get; } = apiRequestId;
} 