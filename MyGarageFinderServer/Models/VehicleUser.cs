using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyGarageFinderServer.Models;

[Table("VehicleUser")]
public partial class VehicleUser
{
    [Key]
    [Column("VehicleUserID")]
    public int VehicleUserId { get; set; }

    [Column("VehicleID")]
    [StringLength(50)]
    public string? VehicleId { get; set; }

    [Column("UserID")]
    public int? UserId { get; set; }

    [InverseProperty("VehicleUser")]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    [ForeignKey("UserId")]
    [InverseProperty("VehicleUsers")]
    public virtual User? User { get; set; }

    [ForeignKey("VehicleId")]
    [InverseProperty("VehicleUsers")]
    public virtual Vehicle? Vehicle { get; set; }
}
