namespace ChatApplication.DAL.Domain.Errors;

public static class ChatError
{
    public static NotFoundError NotFoundError => new("Chat");

    public static DuplicateError DuplicateChatError => new("Chat is already exist.");

    public static DuplicateError DuplicateUserInChatError => new("User is already in chat.");

    public static NoPermissionError NoPermissionError => new();

    public static NoPermissionError IsNotInChatError => new("User didn't join in chat");
}