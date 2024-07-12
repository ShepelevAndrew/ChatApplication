using ChatApplication.DAL.Domain;
using FluentResults;

namespace ChatApplication.BLL.Services.Abstraction;

public interface IChatService
{
    Task<IEnumerable<Chat>> GetChats(string? search);

    Task<Result<Chat>> GetChatById(Guid chatId);

    Task<IEnumerable<User>> GetUsersByChat(Guid chatId);

    Task<Result<Chat>> CreateChat(Guid userId, string name);

    Task<Result> JoinChat(Guid userId, Guid chatId);

    Task<Result> SendMessage(Guid userId, Guid chatId, string message);

    Task<Result> UpdateChat(Guid userId, Guid chatId, string name);

    Task<Result> DeleteChat(Guid userId, Guid chatId);
}