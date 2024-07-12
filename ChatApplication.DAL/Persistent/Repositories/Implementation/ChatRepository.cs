using ChatApplication.DAL.Domain;
using ChatApplication.DAL.Persistent.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.DAL.Persistent.Repositories.Implementation;

public class ChatRepository : IChatRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ChatRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Chat>> Get(string? search)
    {
        IQueryable<Chat> chats = _dbContext.Chats;
        if (!string.IsNullOrWhiteSpace(search))
        {
            chats = chats.Where(chat => chat.Name.Contains(search));
        }

        return await chats.ToListAsync();
    }

    public async Task<Chat?> GetByName(string name)
    {
        var chat = await _dbContext.Chats.FirstOrDefaultAsync(chat => chat.Name == name);
        return chat;
    }

    public async Task<Chat?> GetById(Guid chatId)
    {
        var chat = await _dbContext.Chats.FirstOrDefaultAsync(chat => chat.ChatId == chatId);
        return chat;
    }

    public async Task CreateChat(Chat chat)
    {
        await _dbContext.Chats.AddAsync(chat);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddUserInChat(Chat chat, User user)
    {
        chat.AddUser(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Chat chat)
    {
        _dbContext.Chats.Update(chat);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Chat chat)
    {
        _dbContext.Chats.Remove(chat);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> IsAlreadyExist(Chat chat)
    {
        var isExist = await _dbContext.Chats.AnyAsync(c => c.Name == chat.Name);
        return isExist;
    }
}