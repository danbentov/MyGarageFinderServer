using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyGarageFinderServer.Models;

[Table("UserStatus")]
public partial class UserStatus
{
    [Key]
    [Column("StatusID")]
    public int StatusId { get; set; }

    [StringLength(50)]
    public string? StatusName { get; set; }

    [InverseProperty("UserStatus")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
