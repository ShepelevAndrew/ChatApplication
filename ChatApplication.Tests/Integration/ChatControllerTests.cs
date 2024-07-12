using System.Net.Http.Json;
using ChatApplication.Controllers.Chat.Request;
using ChatApplication.Controllers.Chat.Response;
using Xunit;

namespace ChatApplication.Tests.Integration;

public class ChatControllerTests
{
    [Fact]
    public async Task CreateChatRequest_CreateChat()
    {
        // Arrange
        var application = new WebApplicationFactory();
        var request = new CreateChatRequest(DefaultUtil.User.UserId, "Chat");

        var client = application.CreateClient();

        // Act
        var response = await client.PostAsJsonAsync("/api/chats", request);

        // Assert
        response.EnsureSuccessStatusCode();

        var chatResponse = await response.Content.ReadFromJsonAsync<ChatResponse>();
        Assert.NotNull(chatResponse);
        Assert.Equal(request.Name, chatResponse.Name);
    }
}