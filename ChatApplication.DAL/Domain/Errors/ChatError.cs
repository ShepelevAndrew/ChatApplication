namespace ChatApplication.DAL.Domain.Errors;

public static class ChatError
{
    public static NotFoundError NotFoundError => new("Chat");

    public static DuplicateError DuplicateChatError => new("Chat is already exist.");

    public static DuplicateError DuplicateUserInChatError => new("User is already in chat.");

    public static NoPermissionError NoPermissionError => new();
}