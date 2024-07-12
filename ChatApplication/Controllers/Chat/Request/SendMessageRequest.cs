namespace ChatApplication.Controllers.Chat.Request;

public record SendMessageRequest(
    Guid UserId,
    string Message);