using ChatApplication.BLL.Services.Abstraction;
using ChatApplication.BLL.Services.Implementation;
using ChatApplication.DAL.Domain;
using ChatApplication.DAL.Domain.Errors;
using ChatApplication.DAL.Persistent.Repositories.Abstraction;
using Moq;
using Xunit;

namespace ChatApplication.Tests.Unit.ChatApplication.BLL.Services;

public class ChatServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IChatRepository> _chatRepositoryMock;
    private readonly ChatService _chatService;

    public ChatServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _chatRepositoryMock = new Mock<IChatRepository>();
        Mock<INotificationService> notificationServiceMock = new();

        _chatService = new ChatService(
            _userRepositoryMock.Object,
            _chatRepositoryMock.Object,
            notificationServiceMock.Object);
    }

    [Fact]
    public async Task CreateChat_UserNotFound_ReturnsNotFoundError()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _userRepositoryMock.Setup(repo => repo.GetUser(userId))
            .ReturnsAsync((User)null!);

        // Act
        var result = await _chatService.CreateChat(userId, "Test Chat");

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(UserError.NotFoundError.Message, result.Errors[0].Message);
    }

    [Fact]
    public async Task CreateChat_ChatAlreadyExists_ReturnsDuplicateChatError()
    {
        // Arrange
        var user = new User("Alex");
        
        _userRepositoryMock.Setup(repo => repo.GetUser(user.UserId))
            .ReturnsAsync(user);
        _chatRepositoryMock.Setup(repo => repo.IsAlreadyExist(It.IsAny<Chat>()))
            .ReturnsAsync(true);

        // Act
        var result = await _chatService.CreateChat(user.UserId, "Test Chat");

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(ChatError.DuplicateChatError.Message, result.Errors[0].Message);
    }

    [Fact]
    public async Task CreateChat_ValidInput_CreatesChatSuccessfully()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User("Alex");
        var chat = new Chat("Test Chat", userId);

        _userRepositoryMock.Setup(repo => repo.GetUser(userId))
            .ReturnsAsync(user);
        _chatRepositoryMock.Setup(repo => repo.IsAlreadyExist(chat))
            .ReturnsAsync(false);
        _chatRepositoryMock.Setup(repo => repo.CreateChat(chat))
            .Returns(Task.CompletedTask);
        _chatRepositoryMock.Setup(repo => repo.AddUserInChat(chat, user))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _chatService.CreateChat(userId, "Test Chat");

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(chat.Name, result.Value.Name);
        _chatRepositoryMock.Verify(repo => repo.CreateChat(It.IsAny<Chat>()), Times.Once);
    }
}