namespace ChatApplication.DAL.Domain;

public class User
{
    private readonly List<Chat> _connectedChats = new();

    public User(string name)
    {
        UserId = Guid.NewGuid();
        Name = name;
    }

    public Guid UserId { get; private set; }

    public string Name { get; private set; }

    public IReadOnlyList<Chat> ConnectedChats => _connectedChats.AsReadOnly();
}