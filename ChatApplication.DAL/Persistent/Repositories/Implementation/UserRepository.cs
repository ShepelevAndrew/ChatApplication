using ChatApplication.DAL.Domain;
using ChatApplication.DAL.Persistent.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.DAL.Persistent.Repositories.Implementation;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateUser(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<User?> GetUser(Guid userId)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        return user;
    }

    public async Task UpdateUser(User user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>> GetUsersByChat(Guid chatId)
    {
        var users = await _dbContext.Chats
            .Where(chat => chat.ChatId == chatId)
            .SelectMany(chat => chat.UsersInChat)
            .ToListAsync();

        return users;
    }

    public async Task<bool> IsAlreadyInChat(Guid userId, Guid chatId)
    {
        var isAlreadyInChat = await _dbContext.Chats
            .Where(chat => chat.ChatId == chatId)
            .SelectMany(chat => chat.UsersInChat)
            .AnyAsync(user => user.UserId == userId);

        return isAlreadyInChat;
    }
}