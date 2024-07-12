using ChatApplication.DAL.Domain;

namespace ChatApplication.BLL.Services.Abstraction;

public interface INotificationService
{
    Task SendMessage(User user, Chat chat, string message);

    Task RemoveFromChat(User user, Chat chat);
}