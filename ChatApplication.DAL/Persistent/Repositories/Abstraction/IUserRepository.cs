using ChatApplication.DAL.Domain;

namespace ChatApplication.DAL.Persistent.Repositories.Abstraction;

public interface IUserRepository
{
    Task CreateUser(User user);

    Task<User?> GetUser(Guid userId);

    Task<IEnumerable<User>> GetUsersByChat(Guid chatId);

    Task<bool> IsAlreadyInChat(Guid userId, Guid chatId);
}