using ChatApplication.BLL.Services.Abstraction;
using ChatApplication.Controllers.User.Response;
using ChatApplication.DAL.Domain;
using ChatApplication.Hubs.Response;
using Microsoft.AspNetCore.SignalR;

namespace ChatApplication.Hubs.Services;

public class NotificationService : INotificationService
{
    private readonly IHubContext<ChatHub> _hubContext;

    public NotificationService(IHubContext<ChatHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendMessage(User user, Chat chat, string message)
    {
        await _hubContext.Clients
            .Group(chat.ChatId.ToString())
            .SendAsync("ReceiveMessage", new MessageResponse(UserResponse.ToUserResponse(user), message));
    }

    public async Task RemoveFromChat(User user, Chat chat)
    {
        await SendMessage(user, chat, $"{user.Name} left the chat");
        await _hubContext.Groups.RemoveFromGroupAsync(user.ConnectionId, chat.ChatId.ToString());
    }
}