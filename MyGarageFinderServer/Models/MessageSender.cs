using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyGarageFinderServer.Models;

[Table("MessageSender")]
public partial class MessageSender
{
    [Key]
    [Column("MessageSenderID")]
    public int MessageSenderId { get; set; }

    [StringLength(50)]
    public string? MessageSenderType { get; set; }

    [InverseProperty("MessageSender")]
    public virtual ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();
}
