using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyGarageFinderServer.Models;

public partial class User
{
    [Key]
    [Column("UserID")]
    public int UserId { get; set; }

    [StringLength(20)]
    public string? FirstName { get; set; }

    [StringLength(20)]
    public string? LastName { get; set; }

    [StringLength(50)]
    public string? Email { get; set; }

    public string? LicenseNumber { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    [StringLength(15)]
    public string? UserPassword { get; set; }

    [Column("UserStatusID")]
    public int UserStatusId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RegistrationDate { get; set; }

    [StringLength(15)]
    public string? Phone { get; set; }

    public int GarageLicense { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();

    [InverseProperty("User")]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    [ForeignKey("UserStatusId")]
    [InverseProperty("Users")]
    public virtual UserStatus? UserStatus { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<VehicleUser> VehicleUsers { get; set; } = new List<VehicleUser>();
}
