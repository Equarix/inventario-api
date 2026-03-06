using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using invetario_api.Modules.store.entity;
using invetario_api.Modules.users.entity;

namespace invetario_api.Modules.chat.entity;

[Table("chats")]
public class ChatMessage
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int chatId { get; set; }

    public string message { get; set; }

    public int userId { get; set; }

    [ForeignKey(nameof(userId))]
    public User user { get; set; }

    public DateTime createdAt { get; set; } = DateTime.UtcNow;

    public int storeId { get; set; }

    [ForeignKey(nameof(storeId))]
    public Store store { get; set; }
}
