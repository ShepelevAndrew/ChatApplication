namespace ChatApplication.Controllers.Chat.Request;

public record CreateChatRequest(
    Guid UserId,
    string Name);