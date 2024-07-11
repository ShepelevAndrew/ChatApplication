using ChatApplication.BLL.Services.Abstraction;
using ChatApplication.DAL.Domain;
using ChatApplication.DAL.Domain.Errors;
using ChatApplication.DAL.Persistent.Repositories.Abstraction;
using FluentResults;

namespace ChatApplication.BLL.Services.Implementation;

public class ChatService : IChatService
{
    private readonly IUserRepository _userRepository;
    private readonly IChatRepository _chatRepository;

    public ChatService(
        IUserRepository userRepository,
        IChatRepository chatRepository)
    {
        _userRepository = userRepository;
        _chatRepository = chatRepository;
    }

    public async Task<IEnumerable<Chat>> GetChats(string? search)
    {
        var chats = await _chatRepository.Get(search);
        return chats;
    }

    public async Task<Result<Chat>> GetChatById(Guid chatId)
    {
        var chat = await _chatRepository.GetById(chatId);
        if (chat is null)
        {
            return Result.Fail(ChatError.NotFoundError);
        }

        return chat;
    }

    public async Task<IEnumerable<User>> GetUsersByChat(Guid chatId)
    {
        var users = await _userRepository.GetUsersByChat(chatId);
        return users;
    }

    public async Task<Result<Chat>> CreateChat(Guid userId, string name)
    {
        var user = await _userRepository.GetUser(userId);
        if (user is null)
        {
            return Result.Fail(UserError.NotFoundError);
        }

        var chat = new Chat(name, user.UserId);
        if (await _chatRepository.IsAlreadyExist(chat))
        {
            return Result.Fail(ChatError.DuplicateChatError);
        }

        await _chatRepository.CreateChat(chat);
        await _chatRepository.AddUserInChat(chat, user);

        return chat;
    }

    public async Task<Result> JoinChat(Guid userId, Guid chatId)
    {
        var user = await _userRepository.GetUser(userId);
        if (user is null)
        {
            return Result.Fail(UserError.NotFoundError);
        }

        var chat = await _chatRepository.GetById(chatId);
        if (chat is null)
        {
            return Result.Fail(ChatError.NotFoundError);
        }

        if (await _userRepository.IsAlreadyInChat(user.UserId, chat.ChatId))
        {
            return Result.Fail(ChatError.DuplicateUserInChatError);
        }

        await _chatRepository.AddUserInChat(chat, user);
        return Result.Ok();
    }

    public async Task<Result> DeleteChat(Guid userId, Guid chatId)
    {
        var user = await _userRepository.GetUser(userId);
        if (user is null)
        {
            return Result.Fail(UserError.NotFoundError);
        }

        var chat = await _chatRepository.GetById(chatId);
        if (chat is null)
        {
            return Result.Fail(ChatError.NotFoundError);
        }

        var isNotOwnerDeleteChat = !(chat.OwnerId == user.UserId);
        if (isNotOwnerDeleteChat)
        {
            return Result.Fail(ChatError.NoPermissionError);
        }

        await _chatRepository.Delete(chat);
        return Result.Ok();
    }
}