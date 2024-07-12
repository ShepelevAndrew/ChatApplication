using ChatApplication.BLL.Services.Abstraction;
using ChatApplication.Controllers.User.Response;
using ChatApplication.Hubs.Abstraction;
using ChatApplication.Hubs.Response;
using Microsoft.AspNetCore.SignalR;

namespace ChatApplication.Hubs;

public class ChatHub : Hub<IChatClient>
{
    private readonly IChatService _chatService;
    private readonly IUserService _userService;

    public ChatHub(
        IChatService chatService,
        IUserService userService)
    {
        _chatService = chatService;
        _userService = userService;
    }

    public async Task JoinChat(Guid userId, Guid chatId)
    {
        var userResult = await _userService.GetUser(userId);

        if (userResult.IsSuccess)
        {
            await _userService.UpdateConnectionId(userResult.Value.UserId, Context.ConnectionId);
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());

            var joinResult = await _chatService.JoinChat(userId, chatId);
            if (joinResult.IsSuccess)
            {
                await Clients
                    .Group(chatId.ToString())
                    .ReceiveMessage(new MessageResponse(UserResponse.ToUserResponse(userResult.Value), "Join in the chat."));
            }
        }
    }
}