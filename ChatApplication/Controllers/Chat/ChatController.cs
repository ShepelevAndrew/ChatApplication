using ChatApplication.BLL.Services.Abstraction;
using ChatApplication.Controllers.Chat.Request;
using ChatApplication.Controllers.Chat.Response;
using Microsoft.AspNetCore.Mvc;

namespace ChatApplication.Controllers.Chat;

[ApiController]
[Route("/api/chats")]
public class ChatController : ApiController
{
    private readonly IChatService _chatService;

    public ChatController(IChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpGet]
    public async Task<IActionResult> GetChats(string? search)
    {
        var chats = await _chatService.GetChats(search);
        return Ok(chats);
    }

    [HttpGet("{chatId:guid}")]
    public async Task<IActionResult> GetChatById(Guid chatId)
    {
        var getResult = await _chatService.GetChatById(chatId);
        if (getResult.IsSuccess)
        {
            return Ok(getResult.Value);
        }

        return Problem(getResult.Errors);
    }
    
    [HttpGet("{chatId:guid}/users")]
    public async Task<IActionResult> GetUsersFromChat(Guid chatId)
    {
        var users = await _chatService.GetUsersByChat(chatId);
        return Ok(users);
    }

    [HttpPost]
    public async Task<IActionResult> CreateChat(CreateChatRequest request)
    {
        var createResult = await _chatService.CreateChat(request.UserId, request.Name);
        if (!createResult.IsSuccess)
        {
            return Problem(createResult.Errors);
        }

        var response = ChatResponse.ToResponse(createResult.Value);
        return CreatedAtAction(
            nameof(GetChatById),
            new { chatId = response.ChatId },
            response);
    }

    [HttpPost("{chatId:guid}/message")]
    public async Task<IActionResult> SendMessage(SendMessageRequest request, Guid chatId)
    {
        var sendMessageResult = await _chatService.SendMessage(request.UserId, chatId, request.Message);
        if (sendMessageResult.IsSuccess)
        {
            return Ok();
        }

        return Problem(sendMessageResult.Errors);
    }

    [HttpPut("{chatId:guid}")]
    public async Task<IActionResult> UpdateChat(CreateChatRequest request, Guid chatId)
    {
        var updateResult = await _chatService.UpdateChat(request.UserId, chatId, request.Name);
        if (updateResult.IsSuccess)
        {
            return Ok();
        }

        return Problem(updateResult.Errors);
    }

    [HttpDelete("{chatId:guid}")]
    public async Task<IActionResult> DeleteChat(ChatRequest request, Guid chatId)
    {
        var deleteResult = await _chatService.DeleteChat(request.UserId, chatId);
        if (deleteResult.IsSuccess)
        {
            return NoContent();
        }

        return Problem(deleteResult.Errors);
    }
}