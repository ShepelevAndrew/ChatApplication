using ChatApplication.Hubs.Response;

namespace ChatApplication.Hubs.Abstraction;

public interface IChatClient
{
    public Task ReceiveMessage(MessageResponse message);
}