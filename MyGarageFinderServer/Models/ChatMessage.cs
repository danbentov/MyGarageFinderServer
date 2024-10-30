using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyGarageFinderServer.Models;

[Table("ChatMessage")]
public partial class ChatMessage
{
    [Key]
    [Column("MessageID")]
    public int MessageId { get; set; }

    [Column(TypeName = "text")]
    public string MessageText { get; set; } = null!;

    [Column("UserID")]
    public int? UserId { get; set; }

    [Column("GarageID")]
    public int? GarageId { get; set; }

    [Column("MessageSenderID")]
    public int? MessageSenderId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? MessageTimestamp { get; set; }

    [ForeignKey("GarageId")]
    [InverseProperty("ChatMessages")]
    public virtual Garage? Garage { get; set; }

    [ForeignKey("MessageSenderId")]
    [InverseProperty("ChatMessages")]
    public virtual MessageSender? MessageSender { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("ChatMessages")]
    public virtual User? User { get; set; }
}
