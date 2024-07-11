namespace ChatApplication.Controllers.Chat.Response;

public record ChatResponse(
    Guid ChatId,
    string Name)
{
    public static ChatResponse ToResponse(DAL.Domain.Chat chat)
    {
        return new ChatResponse(chat.ChatId, chat.Name);
    }
};