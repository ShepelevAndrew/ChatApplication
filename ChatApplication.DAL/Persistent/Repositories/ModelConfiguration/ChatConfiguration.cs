using ChatApplication.DAL.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApplication.DAL.Persistent.Repositories.ModelConfiguration;

public class ChatConfiguration : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.ToTable("Chats");

        builder.HasKey(chat => chat.ChatId);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(chat => chat.OwnerId);

        builder.HasMany(chat => chat.UsersInChat)
            .WithMany(user => user.ConnectedChats);
    }
}