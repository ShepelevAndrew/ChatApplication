using ChatApplication.DAL.Domain;

namespace ChatApplication.DAL.Persistent.Repositories.Abstraction;

public interface IChatRepository
{
    Task<IEnumerable<Chat>> Get(string? search);

    Task<Chat?> GetByName(string name);

    Task<Chat?> GetById(Guid chatId);

    Task CreateChat(Chat chat);

    Task AddUserInChat(Chat chat, User user);

    Task Update(Chat chat);

    Task Delete(Chat chat);

    Task<bool> IsAlreadyExist(Chat chat);
}