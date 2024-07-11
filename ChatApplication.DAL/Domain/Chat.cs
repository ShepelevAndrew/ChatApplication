namespace ChatApplication.DAL.Domain;

public class Chat
{
    private readonly List<User> _usersInChat = new();

    public Chat(string name, Guid ownerId)
    {
        ChatId = Guid.NewGuid();
        Name = name;
        OwnerId = ownerId;
    }

    public Guid ChatId { get; private set; }

    public string Name { get; private set; }

    public Guid OwnerId { get; private set; }

    public IReadOnlyList<User> UsersInChat => _usersInChat.AsReadOnly();

    public void AddUser(User user)
    {
        _usersInChat.Add(user);
    }
}